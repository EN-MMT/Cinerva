using Cinerva.Data;
using Cinerva.Data.Entities;
using Cinerva.Services.Common.Properties.Dto;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Cinerva.Services.Common.Properties
{
    public class PropertyService : IPropertyService
    { 
        public readonly CinervaDbContext dbContext;

        public PropertyService(CinervaDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public PropertyDto GetProperty(int id)
        {
            if (id < 1) throw new ArgumentException(nameof(id));

            var propertyEntity = dbContext.Properties.Find(id);
            if (propertyEntity == null) return null;

            return new PropertyDto
            {
                Id = propertyEntity.Id,
                Name = propertyEntity.Name,
                Adress = propertyEntity.Adress,
                AdministratorId = propertyEntity.AdministratorId,
                CityId = propertyEntity.CityId,
                Description = propertyEntity.Description,
                NumberOfDayForRefunds = propertyEntity.NumberOfDayForRefunds,
                Phone = propertyEntity.Phone,
                PropertyTypeId = propertyEntity.PropertyTypeId,
                Rating = propertyEntity.Rating
            };
            
        }

        public int GetCount()
        {
            return dbContext.Properties.Count();
        }

        public List<PropertyDto> GetSkip(int page, int PageSize)
        {
            var pagedSelection = dbContext.Properties.Skip(page * PageSize).Take(PageSize).ToList();

            return pagedSelection.Select(
                x => new PropertyDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Adress = x.Adress,
                    AdministratorId = x.AdministratorId,
                    CityId = x.CityId,
                    Description = x.Description,
                    NumberOfDayForRefunds = x.NumberOfDayForRefunds,
                    Phone = x.Phone,
                    PropertyTypeId = x.PropertyTypeId,
                    Rating = x.Rating

                }
            ).ToList();

        }


        public List<PropertyDto> GetProperties()
        {
            return dbContext.Properties.Select(
                x => new PropertyDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Adress = x.Adress,
                    AdministratorId = x.AdministratorId,
                    CityId = x.CityId,
                    Description = x.Description,
                    NumberOfDayForRefunds = x.NumberOfDayForRefunds,
                    Phone = x.Phone,
                    PropertyTypeId = x.PropertyTypeId,
                    Rating = x.Rating
                    
                }
            ).ToList();
        }
       
        public void CreateProperty(PropertyDto property)
        {
            if (property == null) throw new ArgumentNullException(nameof(property));

            var propertyEntity = new Property
            {
                Name = property.Name,
                Adress = property.Adress,
                AdministratorId = property.AdministratorId,
                CityId = property.CityId,
                Description = property.Description,
                NumberOfDayForRefunds = property.NumberOfDayForRefunds,
                Phone = property.Phone,
                PropertyTypeId = property.PropertyTypeId,
                Rating = property.Rating
            };

            dbContext.Properties.Add(propertyEntity);

            dbContext.SaveChanges();
        }

        public void UpdateProperty(PropertyDto property)
        {
            if (property == null) throw new ArgumentNullException(nameof(property));

            var propertyEntity = dbContext.Properties.Find(property.Id);
            if (propertyEntity == null) throw new Exception();

            propertyEntity.Id = property.Id;
            propertyEntity.Name = property.Name;
            propertyEntity.Adress = property.Adress;
            propertyEntity.AdministratorId = property.AdministratorId;
            propertyEntity.CityId = property.CityId;
            propertyEntity.Description = property.Description;
            propertyEntity.NumberOfDayForRefunds = property.NumberOfDayForRefunds;
            propertyEntity.Phone = property.Phone;
            propertyEntity.PropertyTypeId = property.PropertyTypeId;
            propertyEntity.Rating = property.Rating;

            dbContext.SaveChanges();
        }

        public void DeleteEmployee(int id)
        {
            var propertyEntity = dbContext.Properties.Find(id);
            if (propertyEntity == null) throw new Exception();
            dbContext.Remove(propertyEntity);

            dbContext.SaveChanges();
        }

        public string GetCityName(int? id)
        {
            return dbContext.Properties
                .Where(p => p.CityId == id)
                .Select(c => new { CityName = c.City.Name})
                .FirstOrDefault().CityName;
        }

        public string GetAdminName(int id)
        {
            return String.Join(
                " ",

                dbContext.Properties
                .Where(p => p.AdministratorId == id)
                .Select(c => new { AdminName = c.User.FirstName })
                .FirstOrDefault().AdminName,
                
                dbContext.Properties
                .Where(p => p.AdministratorId == id)
                .Select(c => new { AdminName = c.User.LastName })
                .FirstOrDefault().AdminName
                
                );
        }

    }
}
