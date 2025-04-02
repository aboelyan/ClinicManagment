using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ClinicManagment.Models
{
    // كيان الطبيب
    public class Doctor
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "اسم الطبيب مطلوب")]
        [Display(Name = "اسم الطبيب")]
        public string Name { get; set; }

        [Required(ErrorMessage = "التخصص مطلوب")]
        [Display(Name = "التخصص")]
        public string Specialty { get; set; }

       
        public int? ClinicId { get; set; }

        [ForeignKey("ClinicId")]
        public virtual Clinic? Clinic { get; set; }
        
        public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    }
}
