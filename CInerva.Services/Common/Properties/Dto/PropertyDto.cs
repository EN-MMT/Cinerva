namespace Cinerva.Services.Common.Properties.Dto
{
    public class PropertyDto
    {
        public string Name { get; set; }
        public decimal? Rating { get; set; }
        public string Description { get; set; }
        public string Adress { get; set; }
        public string Phone { get; set; }
        public string? Zipcode { get; set; }
        public int? AdministratorId { get; set; }
        public int NumberOfDayForRefunds { get; set; }
        public int Id { get; set; }
         public int? PropertyTypeId { get;  set; }
        public int? CityId { get;  set; }
       
    }
}
