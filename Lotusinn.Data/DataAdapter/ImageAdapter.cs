using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using LotusInn.Core;
using LotusInn.Model;

namespace Lotusinn.Data.DataAdapter
{
    public class ImageAdapter : BaseAdapter
    {
        private const string SP_IMAGE_INSERT = "ImageInsert";
        private const string SP_IMAGE_UPDATE = "ImageUpdate";
        private const string SP_IMAGE_DELETE = "ImageDelete";
        private const string SP_IMAGE_GETBYROOMTYPEID = "ImageGetByRoomTypeId";
        private const string SP_IMAGE_GETBYALBUMID = "ImageGetByAlbumId";

        public ImageItem Insert(ImageItem image)
        {
            image.Id = IdHelper.Generate();
            var param = new[]
            {
                new SqlParameter("@id", image.Id),
                new SqlParameter("@description", image.Description ?? String.Empty),
                new SqlParameter("@name", image.Name ?? string.Empty),
                new SqlParameter("@path", image.ImagePath ?? string.Empty),
            };
            Call(SP_IMAGE_INSERT, param);
            return image;
        }

        public void Update(ImageItem image)
        {
            var param = new[]
            {
                new SqlParameter("@id", image.Id),
                new SqlParameter("@description", image.Description ?? String.Empty),
                new SqlParameter("@name", image.Name ?? string.Empty),
                new SqlParameter("@path", image.ImagePath ?? string.Empty),
            };
            Call(SP_IMAGE_UPDATE, param);
        }

        public void Delete(string id)
        {
            var param = new[]
            {
                new SqlParameter("@id", id),
            };
            Call(SP_IMAGE_DELETE, param);
        }

        public List<ImageItem> GetByAlbumId(string albumId)
        {
            var param = new[]
            {
                new SqlParameter("@albumId", albumId),
            };
            return Call(SP_IMAGE_GETBYALBUMID, param, DataReader.ReadImages);
        }

        public List<ImageItem> GetByRoomTypId(string roomTypeId)
        {
            var param = new[]
            {
                new SqlParameter("@roomTypeId", roomTypeId),
            };
            return Call(SP_IMAGE_GETBYROOMTYPEID, param, DataReader.ReadImages);
        }
    }
}
