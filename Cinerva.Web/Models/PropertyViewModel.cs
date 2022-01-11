using System.ComponentModel;

namespace Cinerva.Web.Models
{
    public class PropertyViewModel
    {
        public string Name { get; set; }
        public decimal? Rating { get; set; }
        public string Description { get; set; }
        public string Adress { get; set; }
        public string Phone { get; set; }
        public string Zipcode { get; set; }
        public int? AdministratorId { get; set; }
        public int NumberOfDayForRefunds { get; set; }
        public int Id { get; set; }
        [DisplayName("Type")]
        public int? PropertyTypeId { get;  set; }
        public int? CityId { get; set; }
        public string? CityName { get; set; }
        public string? AdminName { get; set; }
    }
}
