using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusInn.Model
{
    public class WebBooking
    {
        public string Id { get; set; }
        public string HouseId { get; set; }
        public string RoomTypeId { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public int NumberOfPersons { get; set; }
        public int NumberOfRooms { get; set; }
        public string Fullname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public bool IsMale { get; set; }
    }
}
