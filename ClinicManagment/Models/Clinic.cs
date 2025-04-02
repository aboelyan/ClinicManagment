using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace ClinicManagment.Models
{


    // كيان العيادة
    public class Clinic
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public List<Doctor> Doctors { get; set; } = new List<Doctor>();
        public string Email { get; set; }
        public string Password { get; set; }
        public string TelegramBotToken { get; set; }
        public string TelegramChatId { get; set; }
        public bool IsOnline { get; set; }
        public DateTime LastSync { get; set; }
    }

}
