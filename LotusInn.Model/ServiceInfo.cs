using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusInn.Model
{
    public class ServiceInfo
    {
        public List<FacilityItem> Facilities { get; set; } 
        public Article Article { get; set; }
    }

    public class FacilityItem
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
    }
}
