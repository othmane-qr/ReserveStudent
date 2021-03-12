using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ReserveStudent.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReserveStudent.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Student> Students { get; set; }
        public DbSet<ReservationType> ReservationTypes { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
    }
}
