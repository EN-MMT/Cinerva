using System.Collections.Generic;

namespace Cinerva.Data.Entities
{
    public class GeneralFeature
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string IconUrl { get; set; }
        public IList<Property> Properties { get; set; }
        public IList<PropertyFacility> PropertyFacilities { get;  set; }
    }
}
