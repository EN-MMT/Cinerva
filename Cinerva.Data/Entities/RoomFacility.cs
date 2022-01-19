namespace Cinerva.Data.Entities
{
    public class RoomFacility
    {
        public int RoomId { get; set; }
        public int RoomFeatureId { get; set; }
        public RoomFeature RoomFeature { get; internal set; }
        public Room Room { get; internal set; }
        //public int RoomFeatureId { get; internal set; }
    }
}
