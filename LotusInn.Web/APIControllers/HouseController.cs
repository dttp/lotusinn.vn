using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Lotusinn.Service;
using LotusInn.Model;

namespace LotusInn.Web.APIControllers
{
    public class HouseController : BaseApiController
    {
        [AcceptVerbs("GET")]
        public List<House> GetHouses()
        {
            var svc = new HouseService();
            return svc.GetAll();
        }

        [AcceptVerbs("GET")]
        public House GetById(string id)
        {
            var svc = new HouseService();
            return svc.GetById(id);
        }

        [AcceptVerbs("POST")]
        public House CreateHouse()
        {
            var svc = new HouseService();
            return svc.CreateHouse();
        }

        [AcceptVerbs("POST")]
        public HttpResponseMessage Update(House house)
        {
            var svc = new HouseService();
            svc.Update(house);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [AcceptVerbs("DELETE")]
        public HttpResponseMessage Delete(string id)
        {
            var svc = new HouseService();
            svc.Delete(id);
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
