using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusInn.Model
{
    public class House
    {
        public string Id { get; set; }
        public int Order { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }        
        public Article Article { get; set; }       
        public string Thumbnail { get; set; }        
    }

    public class HouseList
    {
        public List<House> Houses { get; set; }
    }
}
