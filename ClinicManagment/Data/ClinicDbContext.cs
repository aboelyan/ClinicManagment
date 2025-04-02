using ClinicManagment.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace ClinicManagment.Data
{
    public class ClinicDbContext : DbContext
    {
        public ClinicDbContext(DbContextOptions<ClinicDbContext> options) : base(options) { }

        public DbSet<Clinic> Clinics { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
      
        public DbSet<LabTest> LabTests { get; set; }
        public DbSet<Prescription> Prescriptions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure Primary Keys and Identity Fields
         

            // Configure relationships with NO ACTION to prevent cycles
            modelBuilder.Entity<Patient>(entity =>
            {
                entity.HasOne(p => p.Clinic)
                    .WithMany()
                    .HasForeignKey(p => p.ClinicId)
                    .OnDelete(DeleteBehavior.NoAction)
                    .IsRequired();
            });

            modelBuilder.Entity<Doctor>(entity =>
            {
                entity.HasOne(d => d.Clinic)
                    .WithMany(c => c.Doctors)
                    .HasForeignKey(d => d.ClinicId)
                    .OnDelete(DeleteBehavior.NoAction)
                    .IsRequired();
            });

           

            modelBuilder.Entity<LabTest>(entity =>
            {
                entity.HasOne(l => l.Patient)
                    .WithMany(p => p.LabTests)
                    .HasForeignKey(l => l.PatientId)
                    .OnDelete(DeleteBehavior.NoAction)
                    .IsRequired();
            });

            modelBuilder.Entity<Prescription>(entity =>
            {
                entity.HasOne(pr => pr.Patient)
                    .WithMany()
                    .HasForeignKey(pr => pr.PatientId)
                    .OnDelete(DeleteBehavior.NoAction)
                    .IsRequired();

                entity.HasOne(pr => pr.Doctor)
                    .WithMany()
                    .HasForeignKey(pr => pr.DoctorId)
                    .OnDelete(DeleteBehavior.NoAction)
                    .IsRequired();
            });

           modelBuilder.Entity<Appointment>(entity =>
           {
               entity.HasOne(a => a.Patient)
                   .WithMany(p => p.Appointments)
                   .HasForeignKey(a => a.PatientId)
                   .OnDelete(DeleteBehavior.NoAction)
                   .IsRequired();

               entity.HasOne(a => a.Doctor)
                   .WithMany(d => d.Appointments)
                   .HasForeignKey(a => a.DoctorId)
                   .OnDelete(DeleteBehavior.NoAction)
                   .IsRequired();
           });

            // Seed Default Clinic
            modelBuilder.Entity<Clinic>().HasData(
                new Clinic
                {
                    Id = 1,
                    Name = "العيادة الرئيسية",
                    Email = "admin@clinic.com",
                    Password = "Admin123!",
                    Address = "شارع الملك فهد - الرياض",
                    Phone = "0123456789",
                    IsOnline = true,
                    LastSync = DateTime.Now,
                    TelegramBotToken = "default_token",
                    TelegramChatId = "default_chat_id"
                }
            );

            // Seed Default Doctors
            modelBuilder.Entity<Doctor>().HasData(
                new Doctor { Id = 1, Name = "د. أحمد محمد", Specialty = "طب عام", ClinicId = 1 },
                new Doctor { Id = 2, Name = "د. سارة خالد", Specialty = "أسنان", ClinicId = 1 },
                new Doctor { Id = 3, Name = "د. محمد علي", Specialty = "جلدية", ClinicId = 1 }
            );

            // Seed Default Patients
            modelBuilder.Entity<Patient>().HasData(
                new Patient
                {
                    Id = 1,
                    Name = "عمر أحمد",
                    Phone = "0501234567",
                    BirthDate = new DateTime(1990, 1, 1),
                    ClinicId = 1,
                    Status = "Active"
                },
                new Patient
                {
                    Id = 2,
                    Name = "فاطمة محمد",
                    Phone = "0507654321",
                    BirthDate = new DateTime(1985, 5, 15),
                    ClinicId = 1,
                    Status = "Inactive"
                }
            );

           

            // Seed Default Appointments
            modelBuilder.Entity<Appointment>().HasData(
                new Appointment
                {
                    Id = 1,
                    PatientId = 1,
                    DoctorId = 1,
                    AppointmentDate = DateTime.Today.AddDays(1),
                    StartTime = new TimeSpan(10, 0, 0),
                    EndTime = new TimeSpan(10, 30, 0),
                    Status = "Scheduled"
                },
                new Appointment
                {
                    Id = 2,
                    PatientId = 2,
                    DoctorId = 2,
                    AppointmentDate = DateTime.Today.AddDays(2),
                    StartTime = new TimeSpan(14, 0, 0),
                    EndTime = new TimeSpan(14, 45, 0),
                    Status = "Scheduled"
                }
            );

            // Seed Default Lab Tests
            modelBuilder.Entity<LabTest>().HasData(
                new LabTest
                {
                    Id = 1,
                    TestName = "تحليل دم شامل",
                    PatientId = 1,
                    Result = "طبيعي",
                    Date = DateTime.Now.AddDays(-1)
                },
                new LabTest
                {
                    Id = 2,
                    TestName = "تحليل سكر",
                    PatientId = 2,
                    Result = "يحتاج متابعة",
                    Date = DateTime.Now
                }
            );
        }
    }
}