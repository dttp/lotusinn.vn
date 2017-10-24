using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using LotusInn.Business;
using LotusInn.Model;
using TP.Shared;

namespace LotusInn.Web.APIControllers
{
    public class FeedbackController : BaseApiController
    {
        [AcceptVerbs("POST")]
        public HttpResponseMessage Post(FeedbackForm feedback)
        {
            var mailInfo = WebBookingHelper.CreateMail(feedback, LoadTemplate("feedback.html"));
            EmailService.SendMail(mailInfo);
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}