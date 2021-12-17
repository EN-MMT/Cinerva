using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinerva.Data.Entities
{
    public class Country
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public IList<City> Cities { get; internal set; }
    }
}
