using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LotusInn.Core;
using LotusInn.Model;

namespace Lotusinn.Data
{
    public static class DataReader
    {
        public static List<House> ReadHouses(IDataReader reader)
        {
            var list = new List<House>();
            while (reader.Read())
            {
                var item = new House
                {
                    Id = reader["Id"].ToString(),
                    Address = reader["Address"].ToString(),
                    Latitude = Convert.ToDouble(reader["Latitude"]),
                    Longitude = Convert.ToDouble(reader["Longitude"]),
                    Name = reader["Name"].ToString(),
                    Order = Convert.ToInt32(reader["Order"]),
                    Thumbnail = reader["Thumbnail"].ToString(),
                    Article = new Article
                    {
                        Id = reader["ArticleId"].ToString()
                    }
                };
                list.Add(item);
            }
            return list;
        }

        public static List<Article> ReadArticles(IDataReader reader)
        {
            var list = new List<Article>();
            while (reader.Read())
            {
                var item = new Article
                {
                    Id = reader["Id"].ToString(),
                    Name = reader["Name"].ToString(),
                    Description = reader["Description"].ToString(),
                    Title = reader["Title"].ToString(),
                    Content = reader["Content"].ToString()
                };
                list.Add(item);
            }
            return list;
        }

        public static List<RoomType> ReadRoomType(IDataReader reader)
        {
            var list = new List<RoomType>();
            while (reader.Read())
            {
                var item = new RoomType
                {
                    Id = reader["Id"].ToString(),
                    Name = reader["Name"].ToString(),
                    Price = Convert.ToDouble(reader["Price"]),
                    Article = new Article
                    {
                        Id = reader["ArticleId"].ToString(),
                    },
                    Features = reader["Features"].ToString().ParseAs<List<string>>(),
                    HouseId = reader["HouseId"].ToString(),
                    Images = new List<ImageItem>()
                };
                list.Add(item);
            }
            return list;
        }

        public static List<ImageItem> ReadImages(IDataReader reader)
        {
            var list = new List<ImageItem>();
            while (reader.Read())
            {
                var item = new ImageItem
                {
                    Id = reader["Id"].ToString(),
                    Name = reader["Name"].ToString(),
                    Description = reader["Description"].ToString(),
                    ImagePath = reader["Path"].ToString()
                };
                list.Add(item);
            }
            return list;
        }

        public static List<PhotoAlbum> ReadAlbum(IDataReader reader)
        {
            var list = new List<PhotoAlbum>();
            while (reader.Read())
            {
                var album = new PhotoAlbum
                {
                    Id = reader["Id"].ToString(),
                    HouseId = reader["HouseId"].ToString(),
                    Description = reader["Description"].ToString(),
                    Name = reader["Name"].ToString(),
                    ThumbnailImage = reader["Thumbnail"].ToString(),
                    Items = new List<ImageItem>()
                };
                list.Add(album);
            }
            return list;
        }
    }
}
