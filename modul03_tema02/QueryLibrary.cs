using Cinerva.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinerva.TestConsole
{
    class QueryLibrary : IQueryLibrary
    {
        private static CinervaDbContext DB { get; set; }
        public QueryLibrary(CinervaDbContext db)
        {
            DB = db;
        }

        public void GetAllPropertiesInCity(string city)
        {
            var e2 = DB.Properties.Include(p => p.City).Where(c => c.City.Name == city).ToList();
            foreach (var line in e2)
            {
                System.Console.WriteLine($"{line.Name}");
            }
        }

        public void Ex3()
        {
            var e3 = DB.Reservations.Include(r => r.User).Where(r => r.PayedStatus)
                .Select(r => new { FName = r.User.FirstName, LName = r.User.LastName })
                .Distinct()
                .ToList();
            foreach (var line in e3)
            {
                System.Console.WriteLine($"{line.FName } {line.LName}");
            }
        }

        public void Ex4()
        {
            var e4 = DB.Properties.Include(p => p.User).Include(p => p.City).Where(p => p.City.Name == "Vaslui").OrderBy(p => p.User.FirstName).ToList();
            foreach (var line in e4)
            {
                System.Console.WriteLine($"{line.User.FirstName}, {line.User.LastName}");
            }
        }

        public void Ex5()
        {
            var e5 = DB.Rooms.Where(x => x.Property.City.Country.Name == "France")
                .Select(x => new { x.RoomCategory.Name, x.RoomCategory.PriceNight })
                .Distinct()
                .ToList();

            foreach (var line in e5)
            {
                System.Console.WriteLine($"{line.Name}, {line.PriceNight}");
            }
        }


        public void Ex6()
        {
            DateTime NovemberFirst = new DateTime(2021, 11, 1);
            DateTime NovemberLast = new DateTime(2021, 11, 30);
            var e6 = DB.RoomReservations
                .Where(r => r.Room.Property.City.Country.Name == "Romania"
                && r.Reservation.CheckInDate < NovemberLast && r.Reservation.CheckOutDate > NovemberFirst
                && r.Reservation.CheckOutDate > NovemberFirst && r.Reservation.CheckInDate < NovemberLast
                )
                .GroupBy(r => r.Room.Property.Name)
                .Select(g => new { Name = g.Key, ReservationCount = g.Count() })
                .OrderByDescending(g => g.ReservationCount)
                .Take(5)
                .ToList();

            foreach (var line in e6)
            {
                System.Console.WriteLine($"{line.Name}, {line.ReservationCount}");
            }
        }

        public void Ex8()
        {
            var e8 = DB.RoomReservations.Include(rr => rr.Reservation)
                .Include(rr => rr.Room)
                .Where(rr => rr.Room.Property.City.Country.Name == "Romania")
                .GroupBy(rr => new { Name = rr.Room.Property.Name })
                .Select(q => new { PropertyName = q.Key.Name, Count = q.Sum(rr => (1)) }
                ).ToList();

            foreach (var line in e8)
            {
                Console.WriteLine($"{line.PropertyName}, {line.Count}");
            }
        }

        public void Ex9()
        {
            var e9 = DB.RoomReservations.Include(rr => rr.Reservation)
            .Include(rr => rr.Room)
            .ThenInclude(rr => rr.Property).ThenInclude(p => p.PropertyFacilities)
            .Where(rr => rr.Room.Property.PropertyFacilities.Count(r => r.GeneralFeature.Name == "Parking") > 0 &&
            (rr.Reservation.CheckInDate > new DateTime(2021, 12, 19) ||
            rr.Reservation.CheckInDate < new DateTime(2021, 12, 18)))
            .OrderByDescending(rr => rr.Room.Property.Rating).Take(1)
            .ToList();

            foreach (var line in e9)
            {
                Console.WriteLine(line.Room.Property.Name);
            }
        }

        public void Ex10()
        {
            var e10 = DB.RoomReservations.Where(r => r.Room.Property.Name == "Daniel LLC")
                .GroupBy(r => r.Reservation.CheckInDate.Month)
                .Select(g => new { Month = g.Key, Count = g.Count() })
                .OrderByDescending(g => g.Count)
                .FirstOrDefault();

            Console.WriteLine($"{new DateTime(2021, e10.Month, 1).ToString("MMM", CultureInfo.InvariantCulture)}, {e10.Count}");

        }

        public void Ex11()
        {
            DateTime nextMonday = new DateTime(2021, 12, 16);
            DateTime nextSunday = new DateTime(2021, 12, 22);
            var e11 = DB.Rooms.Where(r => (r.Reservations.Count == 0
                || r.Reservations.Count(res => res.CheckOutDate <= nextSunday && res.CheckInDate >= nextMonday) == 0
                )
            && r.RoomCategory.BedsCount == 2
            && r.Property.City.Name == "Vaslui"
            //&& r.RoomCategory.PriceNight >=100 && r.RoomCategory.PriceNight <=150
            )
                .GroupBy(r => r.Property.Name)
                .Select(g => new { Name = g.Key })
                .ToList();

            foreach (var line in e11)
            {
                Console.WriteLine(line.Name);
            }
        }

        public void Ex12()
        {
            var e12 = DB.RoomReservations.Where(rres => rres.Room.Property.City.Name == "Vaslui"
            && rres.Reservation.CheckInDate.ToString().Contains("2021")
            )
            .GroupBy(rres => new { Name = rres.Room.Property.Name }, rres => new { User = rres.Reservation.User.Id })
            .Select(s => new { s.Key.Name, TotalUsers = s.Count(res => res.User != null) })
            .ToList();

            foreach (var line in e12)
            {
                Console.WriteLine($"{line.Name}, {line.TotalUsers}");
            }
        }

        public void Ex13()
        {
            DateTime MayFirst = new DateTime(2022, 5, 1);
            DateTime SeptemberLast = new DateTime(2022, 9, 30);
            var e13 = DB.Rooms.Where(r => r.Reservations.Count == 0
            || r.Reservations.Count(d => d.CheckInDate > MayFirst || d.CheckOutDate < SeptemberLast) > 0
            || r.Reservations.Count(d => d.CheckInDate < MayFirst && d.CheckOutDate > SeptemberLast) > 0
            )
            .GroupBy(r => new { r.Property.Name, r.Property.Description, r.Property.Adress })
            .Select(p => new { p.Key.Name, p.Key.Description, p.Key.Adress })
            .ToList();

            foreach (var line in e13)
            {
                Console.WriteLine($"{line.Name}\n\nAbout: {line.Description}\n\nAdress: {line.Adress}");
                Console.WriteLine("--------------");
            }
        }

        public void Ex14()
        {
            /*
            DateTime YearStart = new DateTime(2022, 1, 1);
            DateTime YearEnd = new DateTime(2022, 12, 31);
            var e14 = DB.RoomReservations.Where(rres => rres.Reservation.CheckInDate >= YearStart
                 || rres.Reservation.CheckOutDate <= YearEnd
                 && rres.Room.Property.City.Country.Name == "Romania"
            )
            .GroupBy(p => new { Name = p.Room.Property.Name, City = p.Room.Property.City.Name, Rate = p.Room.Property.Rating })
            .Select(s => new { s.Key.Name, s.Key.City, s.Key.Rate, Earnings = s.Sum(rres => rres.Room) })
            .OrderByDescending(s => s.Earnings)
            .Take(5)
            .ToList();
            */
        }
    }
}
