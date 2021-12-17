using Cinerva.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data;
using System.Globalization;
using System.Linq;

namespace Cinerva.TestConsole
{
    class Program
    {
        static void Header(int x)
        {
            System.Console.WriteLine($"======================= EX {x} =======================");
        }
        static void Main(string[] args)
        {
            var cinerva = new CinervaDbContext();

            Header(2);
            var e2 = cinerva.Properties.Include(p => p.City).Where(c => c.City.Name == "Vaslui").ToList();
            foreach(var line in e2)
            {
                System.Console.WriteLine($"{line.Name}");
            }

            
            Header(3);
            
            var e3 = cinerva.Reservations.Include(r => r.User).Where(r => r.PayedStatus)
                .Select(r => new { FName = r.User.FirstName, LName = r.User.LastName})
                .Distinct()
                .ToList();
            foreach (var line in e3)
            {
                System.Console.WriteLine($"{line.FName } {line.LName}");
            }

            Header(4);
            var e4 = cinerva.Properties.Include(p=>p.User).Include(p => p.City).Where(p=> p.City.Name == "Vaslui").OrderBy(p=> p.User.FirstName).ToList();
            foreach (var line in e4)
            {
                System.Console.WriteLine($"{line.User.FirstName}, {line.User.LastName}");
            }

            Header(5);
            var e5 = cinerva.Rooms.Where(x => x.Property.City.Country.Name == "France")
                .Select(x => new { x.RoomCategory.Name, x.RoomCategory.PriceNight })
                .Distinct()
                .ToList();

            foreach (var line in e5)
            {
                System.Console.WriteLine($"{line.Name}, {line.PriceNight}");
            }
            
            Header(6);
            DateTime NovemberFirst = new DateTime(2021, 11, 1);
            DateTime NovemberLast = new DateTime(2021, 11, 30);
            var e6 = cinerva.RoomReservations
                .Where(r => r.Room.Property.City.Country.Name == "Romania"
                && r.Reservation.CheckInDate < NovemberLast && r.Reservation.CheckOutDate >NovemberFirst
                && r.Reservation.CheckOutDate > NovemberFirst && r.Reservation.CheckInDate < NovemberLast
                )
                .GroupBy(r => r.Room.Property.Name)
                .Select(g => new {Name = g.Key, ReservationCount = g.Count() })
                .OrderByDescending(g => g.ReservationCount)
                .Take(5)
                .ToList();

            foreach (var line in e6)
            {
                System.Console.WriteLine($"{line.Name}, {line.ReservationCount}");
            }

            Header(8);
            var e8 = cinerva.RoomReservations.Include(rr => rr.Reservation)
                .Include(rr => rr.Room)
                .Where(rr => rr.Room.Property.City.Country.Name == "Romania")
                .GroupBy(rr => new { Name = rr.Room.Property.Name })
                .Select(q => new { PropertyName = q.Key.Name, Count = q.Sum(rr => (1)) }
                ).ToList();

            foreach(var line in e8)
            {
                Console.WriteLine($"{line.PropertyName}, {line.Count}");
            }

            /*
             * SELECT TOP(1) p.name, p.rating 
                FROM Properties as "p"
                INNER JOIN Rooms as "r" 
                on r.PropertyId = p.Id
                LEFT JOIN RoomReservations as "rr"
                on r.Id = rr.RoomId
                LEFT JOIN Reservations as "res"
                on res.Id = rr.ReservationId
                WHERE NOT ((res.CheckInDate >= '2021-12-04' and res.CheckInDate <='2021-12-06')
                or (res.CheckOutDate >= '2021-12-04' and res.CheckOutDate <='2021-12-06')
                or (res.CheckInDate <= '2021-12-04' and res.CheckOutDate >='2021-12-06'))
                GROUP BY p.name, p.rating
            */

            /*
            Header(9);
            var e9 = cinerva.RoomReservations.Include(rr => rr.Reservation)
            .Include(rr => rr.Room)
            .ThenInclude(rr => rr.Property).ThenInclude(p => p.PropertyFacilities)
            .Where(rr => rr.Room.Property.PropertyFacilities.Count(r => r.GeneralFeature.Name == "Parking")>0 &&
            (rr.Reservation.CheckInDate > new DateTime(2021, 12, 19) ||
            rr.Reservation.CheckInDate < new DateTime(2021, 12, 18)))
            .OrderByDescending(rr => rr.Room.Property.Rating ).Take(1)
            .ToList();

            foreach (var line in e9)
            {
                Console.WriteLine(line.Room.Property.Name);
            }
            */


            Header(10);
            var e10 = cinerva.RoomReservations.Where(r => r.Room.Property.Name == "Daniel LLC")
                .GroupBy(r => r.Reservation.CheckInDate.Month)
                .Select(g => new { Month = g.Key, Count = g.Count() })
                .OrderByDescending(g => g.Count)
                .FirstOrDefault();

            Console.WriteLine($"{new DateTime(2021, e10.Month,1).ToString("MMM", CultureInfo.InvariantCulture)}, {e10.Count}");

            Header(11);
            DateTime nextMonday = new DateTime(2021, 12, 16);
            DateTime nextSunday = new DateTime(2021, 12, 22);
            var e11 = cinerva.Rooms.Where(r => (r.Reservations.Count == 0 
                || r.Reservations.Count(res => res.CheckOutDate<= nextSunday && res.CheckInDate>= nextMonday) ==0
                )
            && r.RoomCategory.BedsCount == 2
            && r.Property.City.Name == "Vaslui"
            //&& r.RoomCategory.PriceNight >=100 && r.RoomCategory.PriceNight <=150
            )
                .GroupBy(r => r.Property.Name)
                .Select(g => new { Name = g.Key })
                .ToList();

            foreach(var line in e11)
            {
                Console.WriteLine(line.Name);
            }

            Header(12);
            var e12 = cinerva.RoomReservations.Where(rres => rres.Room.Property.City.Name == "Vaslui"
            && rres.Reservation.CheckInDate.ToString().Contains("2021")
            )
            .GroupBy(rres => new { Name = rres.Room.Property.Name }, rres => new { User = rres.Reservation.User.Id })
            .Select(s => new { s.Key.Name, TotalUsers = s.Count(res => res.User !=null) })
            .ToList();

            foreach(var line in e12)
            {
                Console.WriteLine($"{line.Name}, {line.TotalUsers}");
            }

            Header(13);
            DateTime MayFirst = new DateTime(2022,5,1);
            DateTime SeptemberLast = new DateTime(2022,9,30);
            var e13 = cinerva.Rooms.Where(r => r.Reservations.Count == 0
            || r.Reservations.Count(d => d.CheckInDate > MayFirst || d.CheckOutDate < SeptemberLast) > 0
            || r.Reservations.Count(d => d.CheckInDate < MayFirst && d.CheckOutDate > SeptemberLast) > 0
            )
            .GroupBy(r => new { r.Property.Name, r.Property.Description, r.Property.Adress })
            .Select(p => new { p.Key.Name, p.Key.Description, p.Key.Adress })
            .ToList();

            foreach(var line in e13)
            {
                Console.WriteLine($"{line.Name}\n\nAbout: {line.Description}\n\nAdress: {line.Adress}");
                Console.WriteLine("--------------");
            }
        }
    }
}
