using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinerva.Data.Entities
{
    public class PropertyFacility
    {
        public int? PropertyId { get; set; }
        public int? GeneralFeatureId { get; set; }
        public GeneralFeature GeneralFeature { get; set; }
        public Property Property { get; set; }
        public int RoomFeatureId { get; set; }
    }
}
