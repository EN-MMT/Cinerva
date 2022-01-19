namespace Cinerva.Data.Entities
{
    public class RoomReservation
    {
        public int Id { get; set; }
        public int RoomId { get; set; }
        public int ReservationId { get; set; }
        //public  IList<Room> Rooms { get; set; }
        //public  IList<Reservation> RoomReservations { get; set; }
        public Reservation Reservation { get; internal set; }
        public Room Room { get; internal set; }
    }
}
