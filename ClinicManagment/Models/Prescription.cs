using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClinicManagment.Models
{
    public class Prescription
    {
        [Key]
        public int Id { get; set; } // Changed from Guid to int for consistency

        [Required]
        public int PatientId { get; set; } // Changed from Guid to int
        [ForeignKey("PatientId")]
        public virtual Patient Patient { get; set; }

        [Required]
        public int DoctorId { get; set; } // Changed from Guid to int
        [ForeignKey("DoctorId")]
        public virtual Doctor Doctor { get; set; }

        // Optional: Link prescription to a specific appointment
        public int? AppointmentId { get; set; } // Changed from Guid? to int?
        [ForeignKey("AppointmentId")]
        public virtual Appointment? Appointment { get; set; }

        [Required]
        [StringLength(200)]
        public string Medication { get; set; } // Name of the medication

        [StringLength(100)]
        public string? Dosage { get; set; } // e.g., "10mg", "5ml"

        [Required]
        public string Instructions { get; set; } // e.g., "Take one tablet twice daily after meals"

        [Required]
        public DateTime DatePrescribed { get; set; } = DateTime.UtcNow;

        // Optional: Duration for the medication
        public string? Duration { get; set; } // e.g., "7 days", "1 month"

        // Optional: Refills allowed
        public int Refills { get; set; } = 0;
    }
}