using System.Collections.Generic;

namespace LotusInn.Model
{
    public enum RepositoryItemType
    {
        RoomTypeImage,
        AlbumThumbnail,
        AlbumImage,
        Image,
    }

    public class RepositoryItem
    {
        public RepositoryItemType Type { get; private set; }
        public Dictionary<string, string> Props { get; set; }

        public byte[] Data { get; set; }

        public RepositoryItem(RoomType roomType, ImageItem image)
        {
            Type = RepositoryItemType.RoomTypeImage;
            Props = new Dictionary<string, string>
            {
                {"HouseId", roomType.HouseId},
                {"RoomTypeId", roomType.Id},
                {"ImageId", image.Id},
                {"ext", image.Name.Substring(image.Name.LastIndexOf(".") + 1)}
            };
        }

        public RepositoryItem(PhotoAlbum album, ImageItem image)
        {
            Type = RepositoryItemType.AlbumImage;
            Props = new Dictionary<string, string>
            {
                {"AlbumId", album.Id},
                {"ImageId", image.Id},
                {"ext", image.Name.Substring(image.Name.LastIndexOf(".") + 1)}
            };
        }

        public RepositoryItem(PhotoAlbum album)
        {
            Type = RepositoryItemType.AlbumThumbnail;
            Props = new Dictionary<string, string>
            {
                {"AlbumId", album.Id }                
            };
        }
    }
}
