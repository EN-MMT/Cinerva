using System.Collections.Generic;

namespace Cinerva.Services.Common.Cities.Dto
{
    public interface ICityService
    {
        public List<CityDto> GetCities();
    }
}
