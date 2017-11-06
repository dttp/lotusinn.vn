using System;
using System.IO;
using LotusInn.Core;
using LotusInn.Model;

namespace Lotusinn.Service
{
    public class Repository
    {
        private const string ROOMTYPE_IMAGE = @"Repository\Houses\{HouseId}\RoomTypes\{RoomTypeId}\Images\{ImageId}.{ext}";
        private const string ALBUM_IMAGE = @"Repository\Albums\{AlbumId}\Images\{ImageId}.{ext}";
        private const string ALBUM_THUMBNAIL = @"Repository\Albums\{AlbumId}\thumb.jpg";
        public string RootFolder { get; set; }

        public Repository(string rootFolder)
        {
            RootFolder = rootFolder;
        }

        // Singleton implement
        public static Repository Instance { get; private set; }
        public static void Create(string rootFolder)
        {
            Instance = new Repository(rootFolder);
        }

        public void Save(RepositoryItem item)
        {
            var filePath = GetPath(item, true);
            File.WriteAllBytes(filePath, item.Data);
        }

        public void Remove(RepositoryItem item)
        {
            var filePath = GetPath(item);
            if (File.Exists(filePath))
                File.Delete(filePath);
        }

        public string GetPath(RepositoryItem item, bool checkExists = false)
        {
            string result = string.Empty;
            switch (item.Type)
            {
                case RepositoryItemType.RoomTypeImage:
                    result = ROOMTYPE_IMAGE.Replace("{HouseId}", item.Props["HouseId"])
                                           .Replace("{RoomTypeId}", item.Props["RoomTypeId"])
                                           .Replace("{ImageId}", item.Props["ImageId"])
                                           .Replace("{ext}", item.Props["ext"]);
                    break;
                case RepositoryItemType.AlbumImage:
                    result = ALBUM_IMAGE.Replace("{AlbumId}", item.Props["AlbumId"])
                                        .Replace("{ImageId}", item.Props["ImageId"])
                                        .Replace("{ext}", item.Props["ext"]);
                    break;
                case RepositoryItemType.AlbumThumbnail:
                    result = ALBUM_THUMBNAIL.Replace("{AlbumId}", item.Props["AlbumId"]);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            result = Path.Combine(RootFolder, result);

            if (checkExists)
            {
                var folder = Path.GetDirectoryName(result);
                if (!Directory.Exists(folder))
                    Directory.CreateDirectory(folder);
            }

            return result;
        }

        public string GetWebPath(RepositoryItem item)
        {
            var localPath = GetPath(item);
            localPath = localPath.Replace(RootFolder, "");
            localPath = localPath.Replace("\\", "/");
            localPath = ConfigManager.APIEndPoint + localPath;
            return localPath;
        }
    }
}
