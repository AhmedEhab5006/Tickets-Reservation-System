using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using TicketsReservationSystem.DAL.Models;

namespace TicketsReservationSystem.DAL.Database
{
    public class ProgramContext : IdentityDbContext<ApplicationUser>
    {

        public ProgramContext(DbContextOptions<ProgramContext> options) : base(options)
        {

        }

        public DbSet<Vendor> vendors { get; set; }
        public DbSet<Client> Clients { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            base.OnModelCreating(builder);

            // Client Configuration
            
            builder.Entity<Client>()
                .HasOne(c => c.address)
                .WithMany()
                .HasForeignKey(c => c.addressId)
                .OnDelete(DeleteBehavior.Restrict);

            
            // EntertainmentEvent Configuration
            builder.Entity<EntertainmentEvent>()
                .HasOne(ee => ee.Event)
                .WithOne(e => e.entertainment)
                .HasForeignKey<EntertainmentEvent>(ee => ee.EventId)
                .OnDelete(DeleteBehavior.Cascade);

            // Event Configuration
            builder.Entity<Event>()
                .HasOne(e => e.vendor)
                .WithMany(v => v.Events)
                .HasForeignKey(e => e.vendorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Event>()
                .HasMany(e => e.Tickets)
                .WithOne(t => t.Event)
                .HasForeignKey(t => t.EventId)
                .OnDelete(DeleteBehavior.Restrict);

            // SportEvent Configuration
            builder.Entity<SportEvent>()
                .HasOne(se => se.Event)
                .WithOne(e => e.sportEvent)
                .HasForeignKey<SportEvent>(se => se.EventId)
                .OnDelete(DeleteBehavior.Cascade);


            // User Configuration
            builder.Entity<ApplicationUser>().ToTable("AspNetUsers");
            builder.Entity<Vendor>().ToTable("vendors");
            builder.Entity<Client>().ToTable("Clients");
  

            // Optional: map base table name
            

            // Vendor Configuration
            builder.Entity<Vendor>()
                .HasMany(v => v.Events)
                .WithOne(e => e.vendor)
                .HasForeignKey(e => e.vendorId)
                .OnDelete(DeleteBehavior.Restrict);



            var roleId = "8f8d4f6a-4e90-4a5c-92b2-f32483c6e7d1";
            var userId = "14c60e1c-6f6d-4c6a-8a6c-623b9d2c9110";

            // Role
            builder.Entity<ApplicationUserRole>().HasData(new ApplicationUserRole
            {
                Id = roleId,
                Name = "Admin",
                NormalizedName = "ADMIN"
            });

            // User
            var hasher = new PasswordHasher<ApplicationUser>();
            var adminUser = new ApplicationUser
            {
                Id = userId,
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                Email = "admin@example.com",
                NormalizedEmail = "ADMIN@EXAMPLE.COM",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = Guid.NewGuid().ToString("D"),
                ConcurrencyStamp = Guid.NewGuid().ToString("D"),
            };
            adminUser.PasswordHash = hasher.HashPassword(adminUser, "Admin@123");

            builder.Entity<ApplicationUser>().HasData(adminUser);
            


        }
            
        

        public DbSet<Address> Address { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<EntertainmentEvent> EntertainmentEvents { get; set; }
        public DbSet<SportEvent> SportEvents { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<ApplicationUserRole> applicationUserRoles { get; set; }

    }
}
