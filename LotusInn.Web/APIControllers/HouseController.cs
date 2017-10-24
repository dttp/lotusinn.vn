using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using LotusInn.Core;
using LotusInn.Model;
using Microsoft.Owin.Security.Notifications;

namespace LotusInn.Web.APIControllers
{
    public class HouseController : BaseApiController
    {
        [AcceptVerbs("GET")]
        public HouseList GetHouses()
        {
            return GetData<HouseList>("houses.json");
        }

        [AcceptVerbs("GET")]
        public House GetHouse(string houseId)
        {
            var houses = GetData<HouseList>("houses.json");
            var house = houses.Houses.SingleOrDefault(h => h.Id.Equals(houseId));
            if (house == null) 
                throw new Exception("House does not exist");
            return house;
        }

        [AcceptVerbs("POST")]
        public House CreateHouse()
        {
            var houses = GetData<HouseList>("houses.json");

            var house = new House()
            {
                Id = ShortGuid.NewGuid(),
                Name = "Lotus Inn " + Convert.ToString(houses.Houses.Count + 1),
                Address = "",
                Latitude = 21.0545967678264,
                Longitude = 105.808778080908,
                Order = houses.Houses.Count + 1,
                Thumbnail = string.Empty                
            };
            house.Article = new Article()
            {
                Id = ShortGuid.NewGuid(),
                Content = string.Empty,
                Description = String.Empty,
                Name = house.Name,
                Title = house.Name
            };

            houses.Houses.Add(house);
            SaveData("houses.json", houses);

            return house;
        }

        [AcceptVerbs("POST")]
        public HttpResponseMessage SaveHouse(House house)
        {
            var houses = GetData<HouseList>("houses.json");
            for (var i = 0; i < houses.Houses.Count; i++)
            {
                if (houses.Houses[i].Id.Equals(house.Id))
                {
                    houses.Houses.Remove(houses.Houses[i]);
                    houses.Houses.Insert(i, house);
                    break;
                }
            }            
            SaveData("houses.json", houses);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [AcceptVerbs("DELETE")]
        public HttpResponseMessage DeleteHouse(string id)
        {
            var houses = GetData<HouseList>("houses.json");
            var currentHouse = houses.Houses.Single(h => h.Id.Equals(id));
            houses.Houses.Remove(currentHouse);
            SaveData("houses.json", houses);

            var roomTypesFilePath = Path.Combine(HttpContext.Current.Server.MapPath("~/"), @"app\data",
                currentHouse.Id + "-roomtypes.json");
            if (File.Exists(roomTypesFilePath)) File.Delete(roomTypesFilePath);
            return Request.CreateResponse(HttpStatusCode.NoContent);
        }
        

        [AcceptVerbs("GET")]
        public List<RoomTypeList> GetAllRoomTypes()
        {
            var result = new List<RoomTypeList>();
            var houses = GetData<HouseList>("houses.json");
            foreach (var house in houses.Houses)
            {
                var fileName = $"{house.Id}-roomtypes.json";
                result.Add(GetData<RoomTypeList>(fileName));
            }
            return result;
        }

        [AcceptVerbs("GET")]
        public RoomTypeList GetRoomTypes(string houseId)
        {
            var fileName = $"{houseId}-roomtypes.json";
            return GetData<RoomTypeList>(fileName);
        }

        [AcceptVerbs("GET")]
        public RoomType GetRoomType(string houseId, string roomTypeId)
        {
            var fileName = $"{houseId}-roomtypes.json";
            var list = GetData<RoomTypeList>(fileName);
            return list.RoomTypes.FirstOrDefault(r => r.Id.Equals(roomTypeId));
        }

        [AcceptVerbs("POST")]
        public RoomType CreateRoomType(string houseId)
        {
            var fileName = $"{houseId}-roomtypes.json";
            var list = GetData<RoomTypeList>(fileName) ?? new RoomTypeList() {RoomTypes = new List<RoomType>()};
            var newRoomType = new RoomType
            {
                HouseId = houseId,
                Id = ShortGuid.NewGuid(),
                Price = 500,
                Features = new List<string>(),
                Name = "Type " + Convert.ToString(list.RoomTypes.Count + 1),
                Article = new Article()
                {
                    Content = "",
                    Description = "",
                    Id = ShortGuid.NewGuid(),
                    Name = "",
                    Title = ""
                }
            };
            list.RoomTypes.Add(newRoomType);
            SaveData(fileName, list);
            return newRoomType;
        }

        [AcceptVerbs("DELETE")]
        public HttpResponseMessage DeleteRoomType(string houseId, string roomTypeId)
        {
            var fileName = $"{houseId}-roomtypes.json";
            var list = GetData<RoomTypeList>(fileName);
            foreach (var roomType in list.RoomTypes)
            {
                if (roomType.Id.Equals(roomTypeId))
                {
                    list.RoomTypes.Remove(roomType);
                    break;
                }
            }

            SaveData(fileName, list);
            return Request.CreateResponse(HttpStatusCode.NoContent);
        }

        [AcceptVerbs("POST")]
        public HttpResponseMessage SaveRoomType(string houseId, RoomType roomType)
        {
            var fileName = $"{houseId}-roomtypes.json";
            var list = GetData<RoomTypeList>(fileName);
            int idx = 0;
            foreach (var r in list.RoomTypes)
            {                
                if (r.Id.Equals(roomType.Id))
                {                    
                    list.RoomTypes.Remove(r);
                    list.RoomTypes.Insert(idx, roomType);
                    break;
                }
                idx++;
            }

            SaveData(fileName, list);
            // remove images that does not belong to this roomType
            var uploadFolder = Path.Combine(HttpContext.Current.Server.MapPath("~"), @"app\data\images", houseId, roomType.Id);
            var dirInfo = new DirectoryInfo(uploadFolder);
            foreach (var file in dirInfo.GetFiles())
            {
                if (!roomType.Images.Exists(item => item.Name.Equals(file.Name, StringComparison.CurrentCultureIgnoreCase)))
                {
                    file.Delete();
                }
            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [AcceptVerbs("POST")]
        public HttpResponseMessage UploadImages(string houseId, string roomTypeId)
        {
            var uploadFolder = Path.Combine(HttpContext.Current.Server.MapPath("~"), @"app\data\images", houseId, roomTypeId);
            if (!Directory.Exists(uploadFolder))
                Directory.CreateDirectory(uploadFolder);

            var fileName = $"{houseId}-roomtypes.json";
            var list = GetData<RoomTypeList>(fileName);
            foreach (var roomType in list.RoomTypes)
            {
                if (roomType.Id.Equals(roomTypeId))
                {

                    for (var i = 0; i < HttpContext.Current.Request.Files.Count; i++)
                    {
                        var httpPostedFile = HttpContext.Current.Request.Files[i];
                        var item = new ImageItem
                        {
                            Id = ShortGuid.NewGuid(),
                            Name = httpPostedFile.FileName,
                            Description = "",
                            ImagePath = "/app/data/images/" +  houseId + "/" + roomTypeId + "/" + httpPostedFile.FileName
                        };
                        var imageFileName = Path.Combine(uploadFolder, httpPostedFile.FileName);
                        if (File.Exists(imageFileName)) File.Delete(imageFileName);
                        httpPostedFile.SaveAs(imageFileName);
                        
                        if (roomType.Images == null) roomType.Images = new List<ImageItem>();
                        roomType.Images.Add(item);
                    }

                    break;
                }
            }            

            SaveData(fileName, list);

            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
