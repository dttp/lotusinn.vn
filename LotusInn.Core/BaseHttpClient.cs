using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace LotusInn.Core
{
	public class BaseHttpClient
	{
		public AuthenticationHeaderValue Authorization { get; set; }

		public T Get<T>(string url)
		{
			var request = CreateRequest(url, HttpMethod.Get);
			return SendRequest<T>(request);
		}

	    public HttpResponseMessage Get(string url)
	    {
	        var request = CreateRequest(url, HttpMethod.Get);
	        return SendRequest(request);
	    }

        public HttpResponseMessage GetWithoutCheck(string url)
        {
            var request = CreateRequest(url, HttpMethod.Get);
            return SendRequestWithoutCheck(request);
        }

        public TResult Post<TInput, TResult>(string url, TInput input)
		{
			var request = CreateRequest(url, HttpMethod.Post, input);
			return SendRequest<TResult>(request);
		}

        public TResult PostLongRunning<TInput, TResult>(string url, TInput input)
        {
            var request = CreateRequest(url, HttpMethod.Post, input);
            return SendRequest<TResult>(request,isLongRunning:true);
        }
        
        public HttpResponseMessage Post<TInput>(string url, TInput input)
		{
			var request = CreateRequest(url, HttpMethod.Post, input);
			return SendRequest(request);
		}

		public TResult Post<TResult>(string url)
		{
			var request = CreateRequest(url, HttpMethod.Post);
			return SendRequest<TResult>(request);
		}

		public HttpResponseMessage Post(string url)
		{
			var request = CreateRequest(url, HttpMethod.Post);
			return SendRequest(request);
		}

		public TResult Put<TInput, TResult>(string url, TInput input, bool ignoreError = false)
		{
			var request = CreateRequest(url, HttpMethod.Put, input);
			return SendRequest<TResult>(request, ignoreError);
		}

		public HttpResponseMessage Put<TInput>(string url, TInput input)
		{
			var request = CreateRequest(url, HttpMethod.Put, input);
			return SendRequest(request);
		}

		public HttpResponseMessage Delete(string url)
		{
			var request = CreateRequest(url, HttpMethod.Delete);
			return SendRequest(request);
		}

		protected virtual HttpResponseMessage CheckResponse(HttpResponseMessage response)
		{
			if (response.IsSuccessStatusCode)
			{
				return response;
			}

			var responseContent = response.Content.ReadAsStringAsync().Result;

            throw new Exception(responseContent);
		}
        
        #region Helper

        private HttpRequestMessage CreateRequest<TInput>(string url, HttpMethod method, TInput input)
		{
			var request = new HttpRequestMessage(method, url)
			{
				Content = input.ToJsonStringContent()
			};

			return request;
		}

		protected HttpRequestMessage CreateRequest(string url, HttpMethod method)
		{
			var request = new HttpRequestMessage(method, url);

			return request;
		}

		private HttpResponseMessage SendRequest(HttpRequestMessage request)
		{
			using (var client = CreateClient())
			{
				var response = client.SendAsync(request).Result;
				return CheckResponse(response);
			}
		}

        private HttpResponseMessage SendRequestWithoutCheck(HttpRequestMessage request)
        {
            using (var client = CreateClient())
            {
                var response = client.SendAsync(request).Result;
                return response;
            }
        }

        private TResult SendRequest<TResult>(HttpRequestMessage request, bool ignoreError = false, bool isLongRunning = false)
		{
			using (var client = CreateClient())
			{
                if (isLongRunning)
                {
                    var responseTask = client.SendAsync(request, new CancellationToken());
                    var response = responseTask.ContinueWith<HttpResponseMessage>(task => {
                        var taskResult = task.Result;
                        if (!taskResult.IsSuccessStatusCode)
                        {
                            var responseContent = taskResult.Content.ReadAsStringAsync().Result;
                            throw new Exception(responseContent);
                        }
                        return taskResult;
                    }, TaskContinuationOptions.OnlyOnRanToCompletion);

                    var result = responseTask.ContinueWith<TResult>(task => {
                        client.Dispose();
                        if (ignoreError)
                            return response.Result.Content.ReadAs<TResult>();
                        return CheckResponse(response.Result).Content.ReadAs<TResult>();
                    }, TaskContinuationOptions.ExecuteSynchronously);
                    return result.Result;
                }
                else
                {
                    var response = client.SendAsync(request).Result;
                    if (ignoreError) return response.Content.ReadAs<TResult>();
                    return CheckResponse(response).Content.ReadAs<TResult>();
                }
            }
		}
        
        private HttpClient CreateClient()
		{
			var client = new HttpClient();
			client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = Authorization;

            return client;
		}

		#endregion
	}

}
