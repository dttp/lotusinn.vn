using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LotusInn.Core.Models;
using LotusInn.Model;

namespace LotusInn.Business
{
    public static class WebBookingHelper
    {
        public static MailInfo CreateMail(this WebBooking bookingInfo, string htmlTemplate, HouseList houseList, RoomTypeList roomTypeList)
        {
            var result = new MailInfo
            {
                Subject = "New Customer Reservation",
                IsBodyHtml = true,
                To = new[] {ConfigManager.ReservationEmail}
            };

            var house = houseList?.Houses.SingleOrDefault(h => h.Id.Equals(bookingInfo.HouseId));
            var roomType = roomTypeList?.RoomTypes.SingleOrDefault(r => r.Id.Equals(bookingInfo.RoomTypeId));

            var body = htmlTemplate.Replace("{{fullName}}", bookingInfo.Fullname);
            body = body.Replace("{{gender}}", bookingInfo.IsMale ? "Male" : "Female");
            body = body.Replace("{{email}}", bookingInfo.Email);
            body = body.Replace("{{phone}}", bookingInfo.Phone);
            body = body.Replace("{{houseName}}", house == null ? "[Not Specified]" : house.Name);
            body = body.Replace("{{roomType}}", roomType == null ? "[Not Specified]" :roomType.Name + " - " + roomType.Price + "$");
            body = body.Replace("{{dateRange}}", bookingInfo.From.ToString("d") + " - " + bookingInfo.To.ToString("d"));
            body = body.Replace("{{numPersons}}", bookingInfo.NumberOfPersons.ToString());
            body = body.Replace("{{numRooms}}", bookingInfo.NumberOfRooms.ToString());

            result.Body = body;
            return result;
        }

        public static MailInfo CreateMail(FeedbackForm feedback, string htmlTemplate)
        {
            var result = new MailInfo
            {
                Subject = "Customer feedback",
                IsBodyHtml = true,
                To = new[] {ConfigManager.ReservationEmail}
            };
            var body = htmlTemplate.Replace("{{name}}", feedback.Name);
            body = body.Replace("{{email}}", feedback.Email);
            body = body.Replace("{{message}}", feedback.Message);
            result.Body = body;
            return result;
        }
    }
}
