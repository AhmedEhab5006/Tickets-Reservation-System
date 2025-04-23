using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
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

        protected override void OnModelCreating(ModelBuilder builder)
        {

            base.OnModelCreating(builder);

            // Client Configuration
            builder.Entity<Client>()
                .HasOne(c => c.user)
                .WithOne(u => u.client)
                .HasForeignKey<Client>(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);

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
            builder.Entity<User>()
                .HasOne(u => u.client)
                .WithOne(c => c.user)
                .HasForeignKey<Client>(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<User>()
                .HasOne(u => u.vendor)
                .WithOne(v => v.user)
                .HasForeignKey<Vendor>(v => v.userId)
                .OnDelete(DeleteBehavior.Cascade);

            // Vendor Configuration
            builder.Entity<Vendor>()
                .HasMany(v => v.Events)
                .WithOne(e => e.vendor)
                .HasForeignKey(e => e.vendorId)
                .OnDelete(DeleteBehavior.Restrict);

        }
            
        

        public DbSet<Address> Address { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<EntertainmentEvent> EntertainmentEvents { get; set; }
        public DbSet<SportEvent> SportEvents { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet <Vendor> vendors { get; set; }
        public DbSet<Reservation> Reservations { get; set; }

    }
}
