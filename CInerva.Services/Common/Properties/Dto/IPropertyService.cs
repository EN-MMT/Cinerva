using System.Collections.Generic;

namespace Cinerva.Services.Common.Properties.Dto
{
    public interface IPropertyService
    {
        List<PropertyDto> GetProperties();
        public int GetCount();
        void CreateProperty(PropertyDto propertyDto);
        PropertyDto GetProperty(int id);
        void UpdateProperty(PropertyDto propertyDto);
        void DeleteEmployee(int id);
        public List<PropertyDto> GetSkip(int page, int PageSize);
        public string GetCityName(int id);
        public string GetAdminName(int id);
    }
}
