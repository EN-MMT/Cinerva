using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cinerva.Data.Entities;

namespace Cinerva.Data
{
    public class CinervaDbContext : DbContext
    {
        
        public CinervaDbContext(DbContextOptions<CinervaDbContext> options) : base(options) { }
        
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<PropertyType> PropertyTypes { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<RoomCategory> RoomCategories { get; set; }
        public DbSet<RoomReservation> RoomReservations { get; set; }
        public DbSet<RoomFeature> RoomFeatures { get; set; }
        public DbSet<RoomFacility> RoomFacilities { get; set; }
        public DbSet<GeneralFeature> GeneralFeatures { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<PropertyFacility> PropertyFacilities { get; set; }
        public DbSet<PropertyImage> PropertyImages { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source = Laptop-54; Initial Catalog = Cinerva; Integrated Security = True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<User>().Property(u => u.LastName).HasColumnName("LastName");
            modelBuilder.Entity<Role>()
               .HasKey(r => r.Id);

            modelBuilder.Entity<Country>()
                .HasKey(c => c.Id);

            modelBuilder.Entity<User>()
                .HasOne(u => u.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.RoleId);

            modelBuilder.Entity<PropertyImage>()
                .HasOne(i => i.Property)
                .WithMany(p => p.PropertyImages)
                .HasForeignKey(i => i.PropertyId);

            modelBuilder.Entity<Property>()
                .HasOne(p => p.PropertyType)
                .WithMany(t => t.Properties)
                .HasForeignKey(p => p.PropertyTypeId);

            modelBuilder.Entity<Property>()
                .HasOne(p => p.User)
                .WithMany(u => u.Properties)
                .HasForeignKey(p => p.AdministratorId);

            modelBuilder.Entity<Property>()
                .HasOne(p => p.City)
                .WithMany(c => c.Properties)
                .HasForeignKey(p => p.CityId);

            modelBuilder.Entity<City>()
                .HasOne(x => x.Country)
                .WithMany(x => x.Cities)
                .HasForeignKey(x => x.CountryId);

            modelBuilder.Entity<Review>()
                .HasOne(x => x.User)
                .WithMany(x => x.Reviews)
                .HasForeignKey(x => x.UserId);

            modelBuilder.Entity<Review>()
                .HasOne(x => x.Property)
                .WithMany(x => x.Reviews)
                .HasForeignKey(x => x.PropertyId);


            modelBuilder.Entity<Property>()
                .HasMany(p => p.GeneralFeatures)
                .WithMany(g => g.Properties)
                .UsingEntity<PropertyFacility>(
                    x => x.HasOne(f => f.GeneralFeature)
                        .WithMany(g => g.PropertyFacilities)
                        .HasForeignKey(f => f.GeneralFeatureId),
                    x => x.HasOne(f => f.Property)
                    .WithMany(p => p.ProperyFacilities)
                    .HasForeignKey(f => f.PropertyId),
                    x => x.HasKey(f => new { f.PropertyId, f.GeneralFeatureId })
                );



            modelBuilder.Entity<Room>()
                .HasOne(r => r.Property)
                .WithMany(p => p.Rooms)
                .HasForeignKey(r => r.PropertyId);

            modelBuilder.Entity<Room>(
                x => x.HasMany(r => r.RoomFeatures)
                .WithMany(g => g.Rooms)
                .UsingEntity<RoomFacility>
                (
                    x => x.HasOne(f => f.RoomFeature)
                    .WithMany(g => g.RoomFacilities)
                    .HasForeignKey(f => f.RoomFeatureId),
                    x => x.HasOne(f => f.Room)
                    .WithMany(p => p.RoomFacilities)
                    .HasForeignKey(f => f.RoomId),
                    x => x.HasKey(f => new { f.RoomId, f.RoomFeatureId }))
                );

            modelBuilder.Entity<Room>()
                .HasOne(r => r.RoomCategory)
                .WithMany(rc => rc.Rooms)
                .HasForeignKey(p => p.RoomCategoryId);

            modelBuilder.Entity<Room>()
                .HasMany(x => x.Reservations)
                .WithMany(x => x.Rooms)
                .UsingEntity<RoomReservation>
                (
                    x => x.HasOne(x => x.Reservation)
                    .WithMany(x => x.RoomReservations)
                    .HasForeignKey(x => x.ReservationId),
                    x => x.HasOne(x => x.Room)
                    .WithMany(x => x.RoomReservations)
                    .HasForeignKey(x => x.RoomId),
                    x => x.HasKey(x => x.Id)
                );

            modelBuilder.Entity<RoomFacility>()
               .HasOne(x => x.RoomFeature)
               .WithMany(x => x.RoomFacilities)
               .HasForeignKey(x => x.RoomFeatureId);
            modelBuilder.Entity<RoomFacility>()
                .HasOne(x => x.Room)
                .WithMany(x => x.RoomFacilities)
                .HasForeignKey(x => x.RoomId);
            modelBuilder.Entity<RoomFacility>()
                .HasKey(x => new { x.RoomId, x.RoomFeatureId });

            modelBuilder.Entity<PropertyFacility>()
               .HasOne(x => x.GeneralFeature)
               .WithMany(x => x.PropertyFacilities)
               .HasForeignKey(x => x.RoomFeatureId);
            modelBuilder.Entity<PropertyFacility>()
                .HasKey(x => new { x.PropertyId, x.RoomFeatureId });

            modelBuilder.Entity<Reservation>()
                .HasOne(x => x.User)
                .WithMany(x => x.Reservations)
                .HasForeignKey(x => x.UserId);

            modelBuilder.Entity<Review>().HasKey(r => new { r.UserId, r.PropertyId });
            modelBuilder.Entity<Reservation>().HasKey(r => new { r.Id});
            //modelBuilder.Entity<Room>().Property(r => r.RoomCategoryId).HasColumnName("RoomCategory");

        }
    }


}
