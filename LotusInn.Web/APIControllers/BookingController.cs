using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using LotusInn.Business;
using LotusInn.Core;
using LotusInn.Model;

namespace LotusInn.Web.APIControllers
{
    public class BookingController : BaseApiController
    {
        [AcceptVerbs("POST")]
        public HttpResponseMessage Reserve(WebBooking bookingInfo)
        {
            var houseList = GetData<HouseList>("houses.json");
            var roomTypeList = GetData<RoomTypeList>(bookingInfo.HouseId + "-roomtypes.json");

            var mailInfo = bookingInfo.CreateMail(LoadTemplate("reservation.html"), houseList, roomTypeList);
            EmailService.SendMail(mailInfo);
            return Request.CreateResponse(HttpStatusCode.OK);
        }        
    }
}