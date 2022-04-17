using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using HouseTARgv20.Core.Domain;
using HouseTARgv20.Core.Dto;
using HouseTARgv20.Core.ServiceInterface;
using HouseTARgv20.Data;

namespace HouseTARgv20.ApplicationServices.Services
{
    public class HouseServices : IHouseService
    {
        private readonly HouseDbContext _context;

        public HouseServices
            (
                HouseDbContext context
            )
        {
            _context = context;
        }
        //Add
        public async Task<HouseDomain> Add(HouseDto dto)
        {
            HouseDomain house = new HouseDomain();

            house.Id = Guid.NewGuid();
            house.Address = dto.Address;
            house.HouseNumber = dto.HouseNumber;
            house.Floors = dto.Floors;
            house.Rooms = dto.Rooms;
            house.Price = dto.Price;
            house.Description = dto.Description;
            house.ModifiedAt = DateTime.Now;
            house.CreatedAt = DateTime.Now;

            await _context.House.AddAsync(house);
            await _context.SaveChangesAsync();

            return house;
        }
        //Delete
        public async Task<HouseDomain> Delete(Guid id)
        {
            var houseId = await _context.House.FirstOrDefaultAsync(x => x.Id == id);
            _context.House.Remove(houseId);
            await _context.SaveChangesAsync();

            return houseId;
        }
        //Edit
        public async Task<HouseDomain> Edit(Guid id)
        {
            var result = await _context.House
                .FirstOrDefaultAsync(x => x.Id == id);

            return result;
        }
        //Update
        public async Task<HouseDomain> Update(HouseDto dto)
        {
            HouseDomain house = new HouseDomain();

            house.Id = dto.Id;
            house.Address = dto.Address;
            house.HouseNumber = dto.HouseNumber;
            house.Floors = dto.Floors;
            house.Rooms = dto.Rooms;
            house.Price = dto.Price;
            house.Description = dto.Description;
            house.ModifiedAt = DateTime.Now;
            house.CreatedAt = DateTime.Now;

            _context.House.Update(house);
            await _context.SaveChangesAsync();

            return house;
        }
    }
}