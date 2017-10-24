using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using LotusInn.Model;
using LotusInn.Web.Controllers;

namespace LotusInn.Web.APIControllers
{

    public class ArticleController : BaseApiController
    {
        [AcceptVerbs("GET")]
        public Article GetContactPage()
        {
            return GetData<Article>("contact.json");
        }

        [AcceptVerbs("GET")]
        public Article GetAboutPage()
        {
            return GetData<Article>("about.json");
        }

        [AcceptVerbs("GET")]
        public Article GetArticle(string page)
        {
            var file = $"{page}.json";
            return GetData<Article>(file);
        }

        [AcceptVerbs("POST")]
        public HttpResponseMessage SaveArticle([FromUri]string page, [FromBody] Article article)
        {           
            SaveData($"{page}.json", article);
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
