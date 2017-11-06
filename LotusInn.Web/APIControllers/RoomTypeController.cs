using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Lotusinn.Service;
using LotusInn.Core;
using LotusInn.Model;

namespace LotusInn.Web.APIControllers
{
    public class RoomTypeController : BaseApiController
    {
        [AcceptVerbs("GET")]
        public List<RoomType> GetByHouseId(string houseId)
        {
            var svc =new RoomTypeService();
            return svc.GetByHouseId(houseId);
        }

        [AcceptVerbs("GET")]
        public RoomType GetById(string id)
        {
            var svc = new RoomTypeService();
            return svc.GetById(id);
        }

        [AcceptVerbs("POST")]
        public RoomType Create(string houseId)
        {
            var svc = new RoomTypeService();
            var roomTypes = svc.GetByHouseId(houseId);
            var roomType = new RoomType
            {
                HouseId = houseId,
                Article = new Article
                {
                    Content = string.Empty,
                    Description = string.Empty,
                    Name = string.Empty,
                    Title = string.Empty
                },
                Name = $"Type {roomTypes.Count + 1}",
                Price = 500,
                Features = new List<string>(),
                Images = new List<ImageItem>()
            };
            roomType = svc.Insert(roomType);
            return roomType;
        }

        [AcceptVerbs("DELETE")]
        public HttpResponseMessage Delete(string id)
        {
            var svc = new RoomTypeService();
            svc.Delete(id);
            return Request.CreateResponse(HttpStatusCode.NoContent);
        }

        [AcceptVerbs("POST")]
        public HttpResponseMessage Update(RoomType roomType)
        {
            var svc = new RoomTypeService();
            svc.Update(roomType);

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [AcceptVerbs("POST")]
        public HttpResponseMessage UploadImages(string roomTypeId)
        {
            var svc = new RoomTypeService();
            svc.UploadImages(roomTypeId, HttpContext.Current.Request.Files);
            
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [AcceptVerbs("POST")]
        public HttpResponseMessage UpdateImage(ImageItem image)
        {
            var svc = new RoomTypeService();
            svc.UpdateImage(image);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [AcceptVerbs("DELETE")]
        public HttpResponseMessage DeleteImage(string roomTypeId, string imageId)
        {
            var svc = new RoomTypeService();
            svc.DeleteImage(roomTypeId, imageId);
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}