using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using LotusInn.Core;
using LotusInn.Model;

namespace Lotusinn.Data.DataAdapter
{
    public class RoomTypeAdapter : BaseAdapter
    {
        private const string SP_ROOMTYPE_INSERT = "RoomTypeInsert";
        private const string SP_ROOMTYPE_UPDATE = "RoomTypeUpdate";
        private const string SP_ROOMTYPE_GETBYID = "RoomTypeGetById";
        private const string SP_ROOMTYPE_DELETE = "RoomTypeDelete";
        private const string SP_ROOMTYPE_GETBYHOUSEID = "RoomTypeGetByHouseId";
        private const string SP_ROOMTYPEIMAGE_INSERT = "RoomTypeImageInsert";
        private const string SP_ROOMTYPEIMAGE_DELETE = " RoomTypeImageDeleteByImageId";        

        public RoomType Insert(RoomType roomType)
        {
            roomType.Id = IdHelper.Generate();

            var article = new Article
            {
                Name = roomType.Id,
                Description = "Article for roomTtype " + roomType.Name,
                Content = string.Empty,
                Title = "Article for roomType" + roomType.Name
            };

            var articleAdapter = new ArticleAdapter();
            article = articleAdapter.Insert(article);
            roomType.Article = article;

            var param = new[]
            {
                new SqlParameter("@id", roomType.Id),
                new SqlParameter("@houseId", roomType.HouseId),
                new SqlParameter("@name", roomType.Name),
                new SqlParameter("@price", roomType.Price),
                new SqlParameter("@features", roomType.Features.ToJson()),
                new SqlParameter("@articleId", roomType.Article.Id)
            };
            Call(SP_ROOMTYPE_INSERT, param);

            return roomType;
        }

        public void Update(RoomType roomType)
        {
            var param = new[]
            {
                new SqlParameter("@id", roomType.Id),
                new SqlParameter("@houseId", roomType.HouseId),
                new SqlParameter("@name", roomType.Name),
                new SqlParameter("@price", roomType.Price),
                new SqlParameter("@features", roomType.Features.ToJson()),
            };
            Call(SP_ROOMTYPE_UPDATE, param);

            var articleAdapter = new ArticleAdapter();
            articleAdapter.Update(roomType.Article);
        }

        public RoomType GetById(string id)
        {
            var param = new[]
            {
                new SqlParameter("@id", id),
            };
            var roomType = Call(SP_ROOMTYPE_GETBYID, param, DataReader.ReadRoomType).FirstOrDefault();
            if (roomType == null) return null;

            var articleAdapter = new ArticleAdapter();
            roomType.Article = articleAdapter.GetById(roomType.Article.Id);

            var imageAdapter = new ImageAdapter();
            roomType.Images = imageAdapter.GetByRoomTypId(id);

            return roomType;
        }

        public void Delete(string id)
        {
            var param = new[]
            {
                new SqlParameter("@id", id),
            };
            Call(SP_ROOMTYPE_DELETE, param);
        }

        public List<RoomType> GetByHouseId(string houseId)
        {
            var param = new[]
            {
                new SqlParameter("@houseId", houseId),
            };
            var roomTypes = Call(SP_ROOMTYPE_GETBYHOUSEID, param, DataReader.ReadRoomType);
            var imageAdapter = new ImageAdapter();
            foreach (var roomType in roomTypes)
            {
                roomType.Images = imageAdapter.GetByRoomTypId(roomType.Id);
            }

            return roomTypes;
        }

        public ImageItem InsertImage(string roomTypeId, ImageItem image)
        {
            var imageAdapter = new ImageAdapter();
            image = imageAdapter.Insert(image);

            var param = new[]
            {
                new SqlParameter("@imageId", image.Id),
                new SqlParameter("@roomTypeId", roomTypeId),
            };

            Call(SP_ROOMTYPEIMAGE_INSERT, param);
            return image;
        }

        public void RemoveImage(string roomTypeId, string imageId)
        {
            var param = new []
            {
                new SqlParameter("@imageId", imageId)
            };
            Call(SP_ROOMTYPEIMAGE_DELETE, param);
        }
    }
}
