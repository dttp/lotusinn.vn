using System.Net;
using System.Net.Http;
using System.Web.Http;
using Lotusinn.Service;
using LotusInn.Model;

namespace LotusInn.Web.APIControllers
{

    public class ArticleController : BaseApiController
    {
        [AcceptVerbs("GET")]
        public Article GetArticle(string name)
        {
            var svc = new ArticleService();
            return svc.GetByName(name);
        }

        [AcceptVerbs("POST")]
        public HttpResponseMessage Update(Article article)
        {
            var svc = new ArticleService();
            svc.Update(article);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [AcceptVerbs("POST")]
        public Article Create(Article article)
        {
            var svc = new ArticleService();
            return svc.Insert(article);
        }

        [AcceptVerbs("DELETE")]
        public HttpResponseMessage Delete(string id)
        {
            var svc = new ArticleService();
            svc.Delete(id);
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
