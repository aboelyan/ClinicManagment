using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClinicManagment.Models
{
    public class Patient
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "اسم المريض مطلوب")]
        [Display(Name = "اسم المريض")]
        public string Name { get; set; }

        [Required(ErrorMessage = "رقم الهاتف مطلوب")]
        [Phone(ErrorMessage = "رقم الهاتف غير صحيح")]
        [Display(Name = "رقم الهاتف")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "تاريخ الميلاد مطلوب")]
        [DataType(DataType.Date)]
        [Display(Name = "تاريخ الميلاد")]
        public DateTime BirthDate { get; set; }
        public string Status { get; set; } = "Active"; // تعيين قيمة افتراضية
        public int? ClinicId { get; set; }

        [ForeignKey("ClinicId")]
        public virtual Clinic? Clinic { get; set; }
        
        public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
        public virtual ICollection<LabTest> LabTests { get; set; } = new List<LabTest>();
    }
}
