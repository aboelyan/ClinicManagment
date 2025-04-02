using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ClinicManagment.Models
{
    // كيان التحاليل الطبية
    public class LabTest
    {
        [Key]
        public int Id { get; set; }
        public string TestName { get; set; }
        public string Result { get; set; }
        public DateTime Date { get; set; }

        [ForeignKey("Patient")]
        public int PatientId { get; set; }
        public Patient Patient { get; set; }
    }

}
