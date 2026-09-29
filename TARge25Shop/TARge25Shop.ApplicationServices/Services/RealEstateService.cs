using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using TARge25Shop.Core;
using TARge25Shop.Core.Domain;
using TARge25Shop.Core.Dto;
using TARge25Shop.Data;

namespace TARge25Shop.ApplicationServices.Services
{
    public class RealEstateServices : IRealEstateServices
    {
        private readonly TARge25ShopContext _dbContext;

        public RealEstateServices
            (
                TARge25ShopContext dbContext
            )
        {
            _dbContext = dbContext;
        }

        public async Task<RealEstate> Create(RealEstateDto dto)
        {
            var realEstate = new RealEstate();

            realEstate.Id = Guid.NewGuid();
            realEstate.Area = dto.Area;
            realEstate.Location = dto.Location;
            realEstate.RoomNumber = dto.RoomNumber;
            realEstate.BuildingType = dto.BuildingType;
            realEstate.CreatedAt = DateTime.Now;
            realEstate.ModifiedAt = DateTime.Now;

            _dbContext.RealEstates.Add(realEstate);
            await _dbContext.SaveChangesAsync();

            return realEstate;
        }

        public async Task<RealEstate> Update(RealEstateDto dto)
        {
            var realEstate = new RealEstate();

            realEstate.Id = dto.Id;
            realEstate.Area = dto.Area;
            realEstate.Location = dto.Location;
            realEstate.RoomNumber = dto.RoomNumber;
            realEstate.BuildingType = dto.BuildingType;
            realEstate.CreatedAt = dto.CreatedAt;
            realEstate.ModifiedAt = DateTime.Now;

            _dbContext.RealEstates.Update(realEstate);
            await _dbContext.SaveChangesAsync();

            return realEstate;
        }

        public async Task<RealEstate?> DetailAsync(Guid id)
        {
            return await _dbContext.RealEstates
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<RealEstate?> Delete(Guid id)
        {
            var realEstate = await _dbContext.RealEstates
                .FirstOrDefaultAsync(x => x.Id == id);
            
            if (realEstate == null) return null;

            _dbContext.RealEstates.Remove(realEstate);
            await _dbContext.SaveChangesAsync();

            return realEstate;
        }
    }
}