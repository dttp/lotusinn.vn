using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lotusinn.Data.DataAdapter;
using LotusInn.Model;
using System.Web;

namespace Lotusinn.Service
{
    public class RoomTypeService
    {
        public RoomType Insert(RoomType roomType)
        {
            var adapter = new RoomTypeAdapter();
            return adapter.Insert(roomType);
        }

        public void Update(RoomType roomType)
        {
            var adapter = new RoomTypeAdapter();
            adapter.Update(roomType);
        }

        public void Delete(string id)
        {
            var roomType = GetById(id);
            foreach (var image in roomType.Images)
            {
                var repoItem = new RepositoryItem(roomType, image);
                Repository.Instance.Remove(repoItem);
            }

            var adapter = new RoomTypeAdapter();
            adapter.Delete(id);
        }

        public RoomType GetById(string id)
        {
            var adapter = new RoomTypeAdapter();
            return adapter.GetById(id);
        }

        public List<RoomType> GetByHouseId(string houseId)
        {
            var adapter = new RoomTypeAdapter();
            return adapter.GetByHouseId(houseId);
        }

        public ImageItem InsertImage(string roomTypeId, ImageItem image)
        {
            var adapter = new RoomTypeAdapter();
            return adapter.InsertImage(roomTypeId, image);
        }        

        public void DeleteImage(string roomTypeId, string imageId)
        {
            var adapter = new RoomTypeAdapter();
            adapter.RemoveImage(roomTypeId, imageId);

            var roomType = GetById(roomTypeId);
            var image = roomType.Images.FirstOrDefault(img => img.Id == imageId);

            var repositoryItem = new RepositoryItem(GetById(roomTypeId), image);
            Repository.Instance.Remove(repositoryItem);
        }

        public void UploadImages(string roomTypeId, HttpFileCollection imageFiles)
        {
            for(var i = 0; i < imageFiles.Count; i ++)
            {
                var file = imageFiles[0];

                var image = new ImageItem
                {
                    Name = file.FileName,
                    Description = string.Empty,
                };
                image = InsertImage(roomTypeId, image);

                var tempFile = Path.GetTempFileName();
                file.SaveAs(tempFile);
                var bytes = File.ReadAllBytes(tempFile);
                File.Delete(tempFile);

                var repositoryItem = new RepositoryItem(GetById(roomTypeId), image) {Data = bytes};
                Repository.Instance.Save(repositoryItem);

                image.ImagePath = Repository.Instance.GetWebPath(repositoryItem);

                UpdateImage(image);
            }
        }

        public void UpdateImage(ImageItem image)
        {
            var imageAdapter = new ImageAdapter();
            imageAdapter.Update(image);
        }
    }
}
