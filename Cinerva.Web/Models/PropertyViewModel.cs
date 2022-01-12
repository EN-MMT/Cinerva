using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Cinerva.Web.Models
{
    public class PropertyViewModel
    {
        [Required]
        public string Name { get; set; }
        public decimal? Rating { get; set; }
        public string Description { get; set; }
        public string Adress { get; set; }
        public string Phone { get; set; }
        public string Zipcode { get; set; }
        [DisplayName("Administrator")]
        public int? AdministratorId { get; set; }
        [DisplayName("Refund days")]
        public int NumberOfDayForRefunds { get; set; }
        public int Id { get; set; }
        [DisplayName("Type")]
        public int? PropertyTypeId { get;  set; }
        [DisplayName("City")]
        public int? CityId { get; set; }
        public string CityName { get; set; }
        public string AdminName { get; set; }

        public SelectList Cities { get; set; }
        public SelectList Admins { get; set; }
    }
}
