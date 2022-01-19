using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cinerva.Data.Entities
{
    public class Room
    {
        [Column("RoomCategory")]
        public int RoomCategoryId { get; set; }
        public RoomCategory RoomCategory { get; set; }
        public int Id { get; set; }
        public int PropertyId { get; set; }
        public int Price { get; set; }
        public IList<RoomFeature> RoomFeatures { get; internal set; }
        public IList<RoomFacility> RoomFacilities { get; internal set; }
        public IList<Reservation> Reservations { get; set; }
        public IList<RoomReservation> RoomReservations { get; internal set; }
        public Property Property { get; set; }
    }
}
