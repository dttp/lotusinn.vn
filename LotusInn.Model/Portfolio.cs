using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace LotusInn.Model
{
    public class Portfolio
    {
        [JsonProperty("albums")]
        public List<PhotoAlbum> Albums { get; set; }
    }

    public class PhotoAlbum
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("thumbnail")]
        public string ThumbnailImage { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("items")]
        public List<ImageItem> Items { get; set; }

        [JsonProperty("houseId")]
        public string HouseId { get; set; }
    }

    public class ImageItem
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("imagePath")]
        public string ImagePath { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }
    }
}
