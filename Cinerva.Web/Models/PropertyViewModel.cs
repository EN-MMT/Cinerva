﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Cinerva.Web.Models
{
    public class PropertyViewModel
    {
        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }
        [Range(0, 5, ErrorMessage = "Value between 0 and 5")]
        [Required]
        public decimal? Rating { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Description { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string Adress { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string Phone { get; set; }
        public string Zipcode { get; set; }
        [DisplayName("Administrator")]
        public int? AdministratorId { get; set; }
        [DisplayName("Refund days")]
        public int NumberOfDayForRefunds { get; set; }
        public int Id { get; set; }
        [DisplayName("Type")]
        public int? PropertyTypeId { get; set; }
        [DisplayName("City")]
        public int? CityId { get; set; }
        public string CityName { get; set; }
        public string AdminName { get; set; }

        public SelectList Cities { get; set; }
        public SelectList Admins { get; set; }

        public List<IFormFile> Files { get; set; } = new List<IFormFile>();
        [DisplayName("Gallery")]
        public List<string> imageUrls { get; set; } = new List<string>();
    }
}
