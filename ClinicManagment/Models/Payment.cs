using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClinicManagment.Models
{
    public enum PaymentMethod
    {
        Cash,       // نقداً
        Card,       // بطاقة
        BankTransfer, // تحويل بنكي
        Insurance   // تأمين
    }

    public class Payment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int InvoiceId { get; set; }
        [ForeignKey("InvoiceId")]
        public virtual Invoice Invoice { get; set; }

        [Required]
        public DateTime PaymentDate { get; set; } = DateTime.UtcNow;

        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Amount { get; set; }

        [Required]
        public PaymentMethod Method { get; set; }

        public string? TransactionReference { get; set; } // e.g., Card transaction ID, Check number
        public string? Notes { get; set; }
    }
}