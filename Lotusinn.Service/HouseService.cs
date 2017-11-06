using System;
using System.Collections.Generic;
using Lotusinn.Data.DataAdapter;
using LotusInn.Model;

namespace Lotusinn.Service
{
    public class HouseService
    {        
        public List<House> GetAll()
        {
            var houseAdapter = new HouseAdapter();
            return houseAdapter.GetAll();
        }

        public House GetById(string id)
        {
            var houseAdapter = new HouseAdapter();
            return houseAdapter.GetById(id);
        }

        public House CreateHouse()
        {
            var houses = GetAll();

            var house = new House
            {
                Name = "Lotus Inn " + Convert.ToString(houses.Count + 1),
                Address = "",
                Latitude = 21.0545967678264,
                Longitude = 105.808778080908,
                Order = houses.Count + 1,
                Thumbnail = string.Empty
            };

            var houseAdapter = new HouseAdapter();
            return houseAdapter.Insert(house);
        }

        public void Update(House house)
        {
            var houseAdapter = new HouseAdapter();
            houseAdapter.Update(house);
        }

        public void Delete(string id)
        {
            var roomTypeSvc = new RoomTypeService();
            var roomTypes = roomTypeSvc.GetByHouseId(id);

            foreach (var roomType in roomTypes)
            {
                roomTypeSvc.Delete(roomType.Id);
            }

            var houseAdapter = new HouseAdapter();
            houseAdapter.Delete(id);            
        }
    }
}
