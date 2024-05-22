using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using кркр.Models;

namespace кркр.Infrastructure
{
    public class DbAppContext : DbContext
    {
        public DbSet<Roles> Roles { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<Rooms> Rooms { get; set; } 
        public DbSet<RoomTypes> RoomTypes { get; set; }
        public DbSet<RoomStates> RoomStates { get; set; }
        public DbSet<Violations> Violations { get; set; }
        public DbSet<Bookings> Bookings { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(
                "Host=localhost; Username=postgres; Password=root; Database=Otello");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Roles>().HasMany(p => p.UsersEntity)
                .WithOne(p => p.RolesEntity);
            modelBuilder.Entity<RoomTypes>().HasMany(p => p.RoomsEntity)
                .WithOne(p => p.RoomTypesEntity);
            modelBuilder.Entity<RoomStates>().HasMany(p => p.RoomsEntity)
                .WithOne(p => p.RoomStatesEntity);
            modelBuilder.Entity<Bookings>().HasOne(p => p.ViolationsEntity).
                WithMany(p => p.BookingsEntity);
            modelBuilder.Entity<Bookings>().HasOne(p => p.UsersEntity).
                WithMany(p => p.BookingsEntity);
            modelBuilder.Entity<Bookings>().HasOne(p => p.RoomsEntity).
                WithMany(p => p.BookingsEntity);
            modelBuilder.Entity<Bookings>().Property(p => p.DateOfDeparture).
                HasConversion(
                src => src.Kind == DateTimeKind.Utc ? src : DateTime.SpecifyKind(src, DateTimeKind.Utc),
                dst => dst.Kind == DateTimeKind.Utc ? dst : DateTime.SpecifyKind(dst, DateTimeKind.Utc)
                );

        }
        
    }
}
