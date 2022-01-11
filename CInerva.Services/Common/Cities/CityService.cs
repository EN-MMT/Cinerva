using Cinerva.Data;
using Cinerva.Services.Common.Cities.Dto;
using System;

namespace Cinerva.Services.Common.Cities
{
    public class CityService : ICityService
    {
        private readonly CinervaDbContext dbContext;

        public CityService(CinervaDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public CityDto GetCity(int id)
        {
            if (id < 1) throw new ArgumentException(nameof(id));

            var cityEntity = dbContext.Cities.Find(id);
            if (cityEntity == null) return null;

            return new CityDto
            {
                Id = cityEntity.Id,
                Name = cityEntity.Name,
                CountryId = cityEntity.CountryId
            };

        }
    }
}
