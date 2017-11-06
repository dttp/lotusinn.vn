using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LotusInn.Core;
using LotusInn.Model;

namespace Lotusinn.Data.DataAdapter
{
    public class AlbumAdapter : BaseAdapter
    {
        private const string SP_PHOTO_ALBUM_INSERT = "PhotoAlbumInsert";
        private const string SP_PHOTO_ALBUM_UPDATE = "PhotoAlbumUpdate";
        private const string SP_PHOTO_ALBUM_GETBYID = "PhotoAlbumGetById";
        private const string SP_PHOTO_ALBUM_GETBYHOUSEID = "PhotoAlbumGetByHouseId";
        private const string SP_PHOTO_ALBUM_DELETE = "PhotoAlbumDelete";
        private const string SP_ALBUM_IMAGE_INSERT = "AlbumImageInsert";
        private const string SP_ALBUM_IMAGE_DELETEBYIAMGEID = "AlbumImageDeleteByImageId";

        public PhotoAlbum Insert(PhotoAlbum album)
        {
            album.Id = IdHelper.Generate();
            var param = new[]
            {
                new SqlParameter("@id", album.Id),
                new SqlParameter("@name", album.Name),
                new SqlParameter("@description", album.Description ?? string.Empty),
                new SqlParameter("@houseId", album.HouseId),
                new SqlParameter("@thumbnail", album.ThumbnailImage ?? string.Empty),
            };
            Call(SP_PHOTO_ALBUM_INSERT, param);
            return album;
        }

        public void Update(PhotoAlbum album)
        {
            var param = new[]
            {
                new SqlParameter("@id", album.Id),
                new SqlParameter("@name", album.Name),
                new SqlParameter("@description", album.Description ?? string.Empty),
                new SqlParameter("@houseId", album.HouseId),
                new SqlParameter("@thumbnail", album.ThumbnailImage ?? string.Empty),
            };
            Call(SP_PHOTO_ALBUM_UPDATE, param);
        }

        public void Delete(string id)
        {
            var param = new[]
            {
                new SqlParameter("@id", id),
            };
            Call(SP_PHOTO_ALBUM_DELETE, param);
        }

        public PhotoAlbum GetById(string id)
        {
            var param = new[]
            {
                new SqlParameter("@id", id),
            };
            var album = Call(SP_PHOTO_ALBUM_GETBYID, param, DataReader.ReadAlbum).FirstOrDefault();
            if (album != null)
            {
                var imageAdapter = new ImageAdapter();
                album.Items = imageAdapter.GetByAlbumId(album.Id);
            }
            return album;
        }

        public List<PhotoAlbum> GetByHouseId(string houseId)
        {
            var param = new[]
            {
                new SqlParameter("@houseId", houseId),
            };
            var albums = Call(SP_PHOTO_ALBUM_GETBYHOUSEID, param, DataReader.ReadAlbum);
            foreach(var album in albums)
            {
                var imageAdapter = new ImageAdapter();
                album.Items = imageAdapter.GetByAlbumId(album.Id);
            }
            return albums;
        }

        public ImageItem InsertImage(string albumId, ImageItem image)
        {
            var imageAdapter = new ImageAdapter();
            image = imageAdapter.Insert(image);
            var param = new[]
            {
                new SqlParameter("@albumId", albumId),
                new SqlParameter("@imageId", image.Id),
            };
            Call(SP_ALBUM_IMAGE_INSERT, param);
            return image;
        }

        public void RemoveImage(string albumId, string imageId)
        {
            var param = new[]
            {
                new SqlParameter("@imageId", imageId),
            };
            Call(SP_ALBUM_IMAGE_DELETEBYIAMGEID, param);
        }
    }
}
