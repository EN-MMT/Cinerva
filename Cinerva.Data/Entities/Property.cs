using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinerva.Data.Entities
{
    public class Property
    {
        //public static string[] Types = { "Motel", "Hostel", "Hotel", "Bordei" } ;
        //public static string[] Cities = { "Rome", "Paris", "Vaslui", "Cluj", "București", "Chișinau" };
        public string Name { get; set; } 
        public decimal? Rating { get; set; }
        public string Description { get; set; }
        public string Adress { get; set; }
        public string Phone { get; set; }
        public string? Zipcode { get; set; }
        public City City { get; set; }
        //public string Type { get; set; }
        public int? AdministratorId { get; set; }
        public int NumberOfDayForRefunds { get; set; }
        public int Id { get; set; }
        public IList<PropertyFacility> ProperyFacilities { get; set; }
        public IList<Room> Rooms { get; set; }
        public IList<Review> Reviews { get; set; }
        public IList<GeneralFeature> GeneralFeatures { get; set; }
        public IList<PropertyImage> PropertyImages { get; set; }
        public User User { get; set; }
        public PropertyType PropertyType { get; internal set; }
        public int? PropertyTypeId { get;  set; }
        public int? CityId { get;  set; }
        public IList<PropertyFacility> PropertyFacilities { get; internal set; }
    }
}
