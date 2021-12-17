using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinerva.Data.Entities
{
    public class Review
    {
        public int UserId { get; set; }
        public int PropertyId { get; set; }
        public string? Description { get; set; }
        public int? Rating { get; set; }
        public DateTime ReviewDate { get; set; }
        //public IList<Property> Properties { get; set; }
        //public IList<User> Users { get; set; }
        public User User { get; internal set; }
        public Property Property { get; internal set; }
    }
}
