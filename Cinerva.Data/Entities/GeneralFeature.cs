using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
