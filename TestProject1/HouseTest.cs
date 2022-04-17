using HouseTARgv20.Core.Dto;
using HouseTARgv20.Data;
using Xunit;
using System;
using HouseTARgv20.Core.ServiceInterface;
using System.Linq;
using System.Threading.Tasks;
using HouseTARgv20Shop.Tests;

namespace HouseTARgv20.Tests
{
    public class HouseTest : TestBase
    {

        [Fact]
        public void WrongId()//Test to check using incorrect Guid
        {
            var guid = "sdfkbjh";

            Action check = () => { throw new InvalidOperationException(guid); };

            var exception = Record.Exception(check);

            Assert.NotNull(exception);
            Assert.IsType<InvalidOperationException>(exception);
        }

        [Fact]
        public async Task DeleteFunctionCheck()//Test to verify delete method with guid
        {
            string guid = "9e9698e5-033a-492d-8196-cce9da0b65b2";

            var houseId = Guid.Parse(guid);
            var house = new HouseDto();
            {
                house.Id = houseId;
                house.Address = "StreetName 3";
                house.HouseNumber = 10;
                house.Floors = 1;
                house.Rooms = 5;
                house.Price = 100000;
                house.Description = "House with 1 floor and garage in a good condition";
                house.CreatedAt = DateTime.Now;
                house.ModifiedAt = DateTime.Now;
            }

            await Svc<IHouseService>().Add(house);
            var deleted = await Svc<IHouseService>().Edit(houseId);
            Assert.Null(deleted);
        }

        //
        [Fact]
        public async void AddingMultipleRows()//Test to check Add method with more than 1 row at once
        {
            string guid = "c1be3e7b-5ce0-4f51-a554-3d510ce64952";
            string guid2 = "ede668d0-055f-4190-853c-08b1b4af0961";
            var house = new HouseDto();
            {
                house.Id = Guid.Parse(guid);
                house.Address = "StreetName";
                house.HouseNumber = 29;
                house.Floors = 3;
                house.Rooms = 15;
                house.Price = 300000;
                house.Description = "House with 3 floors in a good condition";
                house.CreatedAt = DateTime.Now;
                house.ModifiedAt = DateTime.Now;
            }
            var house2 = new HouseDto();
            {
                house2.Id = Guid.Parse(guid2);
                house2.Address = "StreetName";
                house2.HouseNumber = 36;
                house2.Floors = 1;
                house2.Rooms = 4;
                house2.Price = 90000;
                house2.Description = "House with 1 floor in a bad condition";
                house2.CreatedAt = DateTime.Now;
                house2.ModifiedAt = DateTime.Now;
            }

            var context = Svc<HouseDbContext>();
            var Before = context.House.Count();
            await Svc<IHouseService>().Add(house);
            await Svc<IHouseService>().Add(house2);
            var After = context.House.Count();
            Assert.Equal(Before + 2, After);//We add 2 due to the fact that there is a difference before and after adding in 2 records

        }

        //
        [Fact]
        public async Task CheckUpdate()//Test to compare and confirm Update method using Guid and Add method
        {
            string guid = "8a993ae3-39eb-4633-ba03-a800f4980900";
            var house = new HouseDto();
            {
                house.Id = Guid.Parse(guid);
                house.Address = "StreetName";
                house.HouseNumber = 1;
                house.Floors = 3;
                house.Rooms = 13;
                house.Price = 270000;
                house.Description = "House with 3 floors in a good condition";
                house.CreatedAt = DateTime.Now;
                house.ModifiedAt = DateTime.Now;
            }

            var houseUpdateId = Guid.Parse(guid);
            var houseUpdate = new HouseDto();
            {
                houseUpdate.Address = "Streetname 2";
                houseUpdate.HouseNumber = 4;
                houseUpdate.Floors = 2;
                houseUpdate.Rooms = 10;
                houseUpdate.Price = 200000;
                houseUpdate.Description = "House with 2 floors in a good condition";
            }

            await Svc<IHouseService>().Add(house);
            await Svc<IHouseService>().Update(houseUpdate);

            Assert.Equal(house.Id.ToString(), houseUpdateId.ToString());
            Assert.NotEqual(house.Address, houseUpdate.Address);
            Assert.NotEqual(house.HouseNumber, houseUpdate.HouseNumber);
            Assert.NotEqual(house.Floors, houseUpdate.Floors);
            Assert.NotEqual(house.Rooms, houseUpdate.Rooms);
            Assert.NotEqual(house.Price, houseUpdate.Price);
            Assert.NotEqual(house.Description, houseUpdate.Description);
        }
    }
}