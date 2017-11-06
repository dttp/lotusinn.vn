using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Lotusinn.Data.DataAdapter;
using LotusInn.Model;

namespace Lotusinn.Service
{
    public class AlbumService
    {
        public PhotoAlbum Insert(PhotoAlbum album)
        {
            var albumAdapter = new AlbumAdapter();
            return albumAdapter.Insert(album);
        }

        public void Update(PhotoAlbum album)
        {
            var albumAdapter = new AlbumAdapter();
            albumAdapter.Update(album);
        }

        public PhotoAlbum GetById(string id)
        {
            var albumAdapter = new AlbumAdapter();
            return albumAdapter.GetById(id);
        }

        public List<PhotoAlbum> GetByHouseId(string houseId)
        {
            var albumAdapter = new AlbumAdapter();
            return albumAdapter.GetByHouseId(houseId);
        }

        public void Delete(string id)
        {
            var album = GetById(id);
            foreach (var item in album.Items)
            {
                var repoItem = new RepositoryItem(album, item);
                Repository.Instance.Remove(repoItem);
            }

            var albumAdapter = new AlbumAdapter();
            albumAdapter.Delete(id);
        }

        public void SetThumbnail(string albumId, HttpPostedFile file)
        {
            var album = GetById(albumId);

            var tempfile = Path.GetTempFileName();
            file.SaveAs(tempfile);
            var bytes = File.ReadAllBytes(tempfile);
            File.Delete(tempfile);

            var repoItem = new RepositoryItem(album) { Data = bytes };
            Repository.Instance.Save(repoItem);
            album.ThumbnailImage = Repository.Instance.GetWebPath(repoItem);

            Update(album);
        }

        public void UploadImages(string albumId, HttpFileCollection files)
        {
            var albumAdapter = new AlbumAdapter();
            var album = GetById(albumId);
            for(var i = 0; i < files.Count; i ++)
            {
                var file = files[0];
                var tempfile = Path.GetTempFileName();
                file.SaveAs(tempfile);
                var bytes = File.ReadAllBytes(tempfile);
                File.Delete(tempfile);
                var image = new ImageItem
                {
                    Name = file.FileName,
                    Description = string.Empty
                };

                image = albumAdapter.InsertImage(albumId, image);

                var repoItem = new RepositoryItem(album, image) {Data = bytes};
                Repository.Instance.Save(repoItem);
                image.ImagePath = Repository.Instance.GetWebPath(repoItem);
                UpdateImage(image);
            }
        }

        public void UpdateImage(ImageItem image)
        {
            var imageAdapter = new ImageAdapter();
            imageAdapter.Update(image);
        }

        public void RemoveImage(string albumId, string imageId)
        {
            var album = GetById(albumId);
            var image = album.Items.FirstOrDefault(i => i.Id == imageId);
            var repoItem = new RepositoryItem(album, image);
            Repository.Instance.Remove(repoItem);

            var albumAdapter = new AlbumAdapter();
            albumAdapter.RemoveImage(albumId, imageId);
        }
    }
}
