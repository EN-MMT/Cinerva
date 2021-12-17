using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinerva.Data.Entities
{
    public class Reservation
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public bool PayedStatus { get; set; }
        public string PaymentMethod { get; set; }
        public DateTime CancelDate { get; set; }
        public IList<Room> Rooms { get; set; }
        public IList<RoomReservation> RoomReservations { get; set; }
        public User User { get; set; }
    }
}
