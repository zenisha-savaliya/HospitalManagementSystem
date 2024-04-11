using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    UserId = 1,
                    FirstName = "zenisha",
                    LastName = "savaliya",
                    Password = "e606e38b0d8c19b24cf0ee3808183162ea7cd63ff7912dbb22b5e803286b4446",
                    ContactNumber = "1234567890",
                    Email = "zenishasavaliya96@gmail.com",
                    DateOfBirth = new DateTime(2003, 08, 15),
                    Gender = "Female",
                    Role = "Doctor"
                });

            modelBuilder.Entity<Doctor>().HasData(
                new Doctor
                {
                    DoctorId = 1,
                    FirstName = "zenisha",
                    LastName = "savaliya",
                    Password = "e606e38b0d8c19b24cf0ee3808183162ea7cd63ff7912dbb22b5e803286b4446",
                    ContactNumber = "1234567890",
                    Email = "zenishasavaliya96@gmail.com",
                    DateOfBirth = new DateTime(2003, 08, 15),
                    Gender = "Female",
                    Specialist = "Brain Surgery",
                    UserId = 1
                });

            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Patient)
                .WithMany()
                .HasForeignKey(a => a.PatientId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Patient>()
                .HasOne(p => p.User)
                .WithMany()
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Duty>()
                .HasOne(d => d.Nurse)
                .WithMany()
                .HasForeignKey(d => d.NurseId)
                .OnDelete(DeleteBehavior.Restrict); 

            modelBuilder.Entity<Duty>()
                .HasOne(d => d.Doctor)
                .WithMany()
                .HasForeignKey(d => d.DoctorId)
                .OnDelete(DeleteBehavior.Restrict); 

            modelBuilder.Entity<Duty>()
                .HasOne(d => d.Patient)
                .WithMany()
                .HasForeignKey(d => d.PatientId)
                .OnDelete(DeleteBehavior.Restrict);
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Nurse> Nurses { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Receptionist> Receptionists { get; set; }       
        public DbSet<Duty> Duty { get; set; }
        public DbSet<Appointment> Appointments { get; set; }

    }
}
