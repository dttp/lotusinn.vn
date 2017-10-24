using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using LotusInn.Model;

namespace LotusInn.Web.APIControllers
{
    public class ServicesController : BaseApiController
    {
        [AcceptVerbs("GET")]
        public ServiceInfo GetServices()
        {
            return GetData<ServiceInfo>("services.json");
        }

        [AcceptVerbs("POST")]
        public HttpResponseMessage Save(ServiceInfo service)
        {
            SaveData("services.json", service);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [AcceptVerbs("GET")]
        public List<SocialService> GetSocialServices()
        {
            return GetData<List<SocialService>>("social-services.json");
        }

        [AcceptVerbs("POST")]
        public HttpResponseMessage SaveSocialServices(List<SocialService> socialServices)
        {
            SaveData("social-services.json", socialServices);
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}