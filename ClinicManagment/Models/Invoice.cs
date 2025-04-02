using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClinicManagment.Models
{
    public enum InvoiceStatus
    {
        Draft,      // مسودة
        Issued,     // صادرة
        Paid,       // مدفوعة
        Partial,    // مدفوعة جزئياً
        Overdue,    // متأخرة
        Cancelled   // ملغاة
    }

    public class Invoice
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int PatientId { get; set; }
        [ForeignKey("PatientId")]
        public virtual Patient Patient { get; set; }

        // Optional: Link invoice to a specific appointment
        public int? AppointmentId { get; set; }
        [ForeignKey("AppointmentId")]
        public virtual Appointment? Appointment { get; set; }

        [Required]
        public DateTime IssueDate { get; set; } = DateTime.UtcNow;

        public DateTime? DueDate { get; set; }

        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal TotalAmount { get; set; }

        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal AmountPaid { get; set; } = 0;

        [Required]
        public InvoiceStatus Status { get; set; } = InvoiceStatus.Draft;

        public string? Notes { get; set; }

        // Navigation property for invoice items
        public virtual ICollection<InvoiceItem> InvoiceItems { get; set; } = new List<InvoiceItem>();

        // Navigation property for payments
        public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
    }
}