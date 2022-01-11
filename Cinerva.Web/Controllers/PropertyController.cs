

using Cinerva.Data.Entities;
using Cinerva.Services.Common.Properties;
using Cinerva.Services.Common.Properties.Dto;
using Cinerva.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Cinerva.Web.Controllers
{
    public class PropertyController : Controller
    {
        private readonly IPropertyService propertyService;

        

        public PropertyController(IPropertyService propertyService)
        {
            this.propertyService = propertyService;
        }
        public IActionResult Index()
        {
            var propertiesDtos = propertyService.GetProperties();

            var propertiesViewModels = propertiesDtos.Select(
                x => new PropertyViewModel
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
                    Rating = x.Rating,

                }
            ).ToList();


            return View(propertiesViewModels);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create([FromForm] PropertyViewModel propertyViewModel)
        {
            var propertyDto = new PropertyDto
            {
                Name = propertyViewModel.Name,
                Adress = propertyViewModel.Adress,
                AdministratorId = propertyViewModel.AdministratorId,
                CityId = propertyViewModel.CityId,
                Description = propertyViewModel.Description,
                NumberOfDayForRefunds = propertyViewModel.NumberOfDayForRefunds,
                Phone = propertyViewModel.Phone,
                PropertyTypeId = propertyViewModel.PropertyTypeId,
                Rating = propertyViewModel.Rating
            };

            propertyService.CreateProperty(propertyDto);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            if (id < 1) return RedirectToAction("Index");

            var propertyDto = propertyService.GetProperty(id);
            if (propertyDto == null) return RedirectToAction("Index");

            var propertyViewModel = GetPropertyViewModelFromDto(propertyDto);

            return View(propertyViewModel);
            
        }

        [HttpPost]
        public IActionResult Edit(PropertyViewModel propertyViewModel)
        {
            var propertyDto = propertyService.GetProperty(propertyViewModel.Id);
            if (propertyDto == null) return RedirectToAction("Index");

            propertyDto.Id = propertyViewModel.Id;
            propertyDto.Name = propertyViewModel.Name;
            propertyDto.Adress = propertyViewModel.Adress;
            propertyDto.AdministratorId = propertyViewModel.AdministratorId;
            propertyDto.CityId = propertyViewModel.CityId;
            propertyDto.Description = propertyViewModel.Description;
            propertyDto.NumberOfDayForRefunds = propertyViewModel.NumberOfDayForRefunds;
            propertyDto.Phone = propertyViewModel.Phone;
            propertyDto.PropertyTypeId = propertyViewModel.PropertyTypeId;
            propertyDto.Rating = propertyViewModel.Rating;

            propertyService.UpdateProperty(propertyDto);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var propertyDto = propertyService.GetProperty(id);
            propertyService.DeleteEmployee(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Index(int page = 0)
        {
            const int PageSize = 10; // you can always do something more elegant to set this

            var count = propertyService.GetCount();

            var data = propertyService.GetSkip(page, PageSize);

            this.ViewBag.MaxPage = (count / PageSize) - (count % PageSize == 0 ? 1 : 0);

            this.ViewBag.Page = page;

            var propertyViewModel = GetListOfPropertyViewModelFromDto(data);
            return View(propertyViewModel);
        }

        #region [PRIVATE METHODS]
        private PropertyViewModel GetPropertyViewModelFromDto(PropertyDto propertyDto)
        {
            if (propertyDto == null) return null;

            return new PropertyViewModel
            {
                Id = propertyDto.Id,
                Name = propertyDto.Name,
                Adress = propertyDto.Adress,
                AdministratorId = propertyDto.AdministratorId,
                CityId = propertyDto.CityId,
                Description = propertyDto.Description,
                NumberOfDayForRefunds = propertyDto.NumberOfDayForRefunds,
                Phone = propertyDto.Phone,
                PropertyTypeId = propertyDto.PropertyTypeId,
                Rating = propertyDto.Rating,
                CityName = propertyService.GetCityName((int)propertyDto.CityId),
                AdminName = propertyService.GetAdminName((int)propertyDto.AdministratorId)
            };
        }

        private List<PropertyViewModel> GetListOfPropertyViewModelFromDto(List<PropertyDto> propertyDtos)
        {
            var propertyViewModels =new List<PropertyViewModel>(); 

            foreach(var propertyDto in propertyDtos)
            {
                propertyViewModels.Add(GetPropertyViewModelFromDto(propertyDto));
            }
            return propertyViewModels;
        }
        #endregion
    }
}
