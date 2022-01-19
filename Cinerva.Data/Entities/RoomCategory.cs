using System.Collections.Generic;

namespace Cinerva.Data.Entities
{
    public class RoomCategory
    {
        public int Id { get; set; }
        public int BedsCount { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? PriceNight { get; set; }
        public IList<Room> Rooms { get; set; }
    }
}
