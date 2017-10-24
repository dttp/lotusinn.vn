using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusInn.Model
{
    public class RoomType
    {
        public string Id { get; set; }
        public string HouseId { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }   
        public List<string> Features { get; set; }               
        public Article Article { get; set; }
        public List<ImageItem> Images { get; set; } 
    }

    public class RoomTypeList
    {
        public List<RoomType> RoomTypes { get; set; }
    }
}
