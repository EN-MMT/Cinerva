using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinerva.Data.Entities
{
    public class PropertyType
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public IList<Property> Properties { get; internal set; }
    }
}
