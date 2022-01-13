using Cinerva.Services.Common.Cities;
using Cinerva.Services.Common.Cities.Dto;
using Cinerva.Services.Common.Properties.Dto;
using Cinerva.Services.Common.Users.Dto;
using Cinerva.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

namespace Cinerva.Web.Controllers
{
    public class PropertyController : Controller
    {
        private readonly IPropertyService propertyService;
        private readonly ICityService cityService;
        private readonly IUserService userService;

        private readonly IHttpContextAccessor httpContextAccessor;

        private string userAgent;

        public PropertyController(
            IPropertyService propertyService,
            ICityService cityService,
            IUserService userService,
            IHttpContextAccessor httpContextAccessor
            )
        {
            this.propertyService = propertyService;
            this.cityService = cityService;
            this.userService = userService;
            this.httpContextAccessor = httpContextAccessor;
            userAgent = httpContextAccessor.HttpContext.User.ToString();
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
            var propertyViewModel = new PropertyViewModel();
            propertyViewModel.Cities = new SelectList(cityService.GetCities(), "Id", "Name");
            propertyViewModel.Admins = new SelectList(userService.GetAdmins(), "Id", "FullName");


            return View(propertyViewModel);
        }

        [HttpPost]
        public IActionResult Create([FromForm] PropertyViewModel propertyViewModel)
        {
            if(!ModelState.IsValid)
            {
                return View(propertyViewModel);
            }

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
            propertyViewModel.Cities = new SelectList(cityService.GetCities(), "Id", "Name", propertyDto.Id);
            propertyViewModel.Admins = new SelectList(userService.GetAdmins(), "Id", "FullName", propertyDto.Id);

            return View(propertyViewModel);

        }

        [HttpPost]
        public IActionResult Edit(PropertyViewModel propertyViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(propertyViewModel);
            }

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

        [HttpPost]
        public IActionResult Delete(PropertyViewModel propertyViewModel)
        {
            var propertyDto = propertyService.GetProperty(propertyViewModel.Id);
            propertyService.DeleteEmployee(propertyDto.Id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var propertyDto = propertyService.GetProperty(id);

            return View(GetPropertyViewModelFromDto(propertyDto));
        }

        [HttpGet]
        public IActionResult Index(int page = 0)
        {
            //ViewBag

            const int PageSize = 10;

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
                CityName = propertyService.GetCityName(propertyDto.CityId),
                AdminName = propertyService.GetAdminName((int)propertyDto.AdministratorId)
            };
        }

        private List<PropertyViewModel> GetListOfPropertyViewModelFromDto(List<PropertyDto> propertyDtos)
        {
            var propertyViewModels = new List<PropertyViewModel>();

            foreach (var propertyDto in propertyDtos)
            {
                propertyViewModels.Add(GetPropertyViewModelFromDto(propertyDto));
            }
            return propertyViewModels;
        }
        #endregion
    }
}
