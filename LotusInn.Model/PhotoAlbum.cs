using System.Collections.Generic;
using Newtonsoft.Json;

namespace LotusInn.Model
{
    public class PhotoAlbum
    {
        public string Id { get; set; }
        public string ThumbnailImage { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<ImageItem> Items { get; set; }
        public string HouseId { get; set; }
    }
}