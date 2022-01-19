using System.Collections.Generic;

namespace Cinerva.Data.Entities
{
    public class RoomFeature
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string IconUrl { get; set; }
        public IList<Room> Rooms { get; internal set; }
        public IList<RoomFacility> RoomFacilities { get; internal set; }
    }
}
