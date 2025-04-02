using System;
using System.ComponentModel.DataAnnotations;

namespace ClinicManagment.Models
{
    public class Appointment
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "يجب اختيار المريض")]
        public int PatientId { get; set; }
        public Patient? Patient { get; set; }
        
        [Required(ErrorMessage = "يجب اختيار الطبيب")]
        public int DoctorId { get; set; }
        public Doctor? Doctor { get; set; }
        
        [Required(ErrorMessage = "يجب تحديد تاريخ الموعد")]
        [DataType(DataType.Date, ErrorMessage = "تاريخ غير صحيح")]
        public DateTime AppointmentDate { get; set; }
        
        [Required(ErrorMessage = "يجب تحديد وقت البدء")]
        [DataType(DataType.Time, ErrorMessage = "وقت غير صحيح")]
        public TimeSpan StartTime { get; set; }
        
        [Required(ErrorMessage = "يجب تحديد وقت الانتهاء")]
        [DataType(DataType.Time, ErrorMessage = "وقت غير صحيح")]
        [CustomValidation(typeof(Appointment), nameof(ValidateEndTime))]
        public TimeSpan EndTime { get; set; }
        
        [StringLength(500, ErrorMessage = "يجب ألا تتجاوز الملاحظات 500 حرف")]
        public string? Notes { get; set; }
        
        [Required(ErrorMessage = "يجب تحديد حالة الموعد")]
        [RegularExpression("Scheduled|Completed|Cancelled", ErrorMessage = "حالة غير صالحة")]
        public string Status { get; set; } = "Scheduled";

        public static ValidationResult? ValidateEndTime(TimeSpan endTime, ValidationContext context)
        {
            var appointment = (Appointment)context.ObjectInstance;
            if (endTime <= appointment.StartTime)
            {
                return new ValidationResult("يجب أن يكون وقت الانتهاء بعد وقت البدء");
            }
            return ValidationResult.Success;
        }
    }
}