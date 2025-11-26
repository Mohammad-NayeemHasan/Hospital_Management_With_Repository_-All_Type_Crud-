using CrudOperation.Models;

namespace CrudOperation.ViewModels
{
    public class PatientCreateViewModel
    {
        public PatientCreateViewModel()
        {
            Appointments = new List<Appointment>();
        }
        public long Id { get; set; }
        public string? Name { get; set; } 
        public int? Age { get; set; }
        public string? Phone { get; set; } 
        public string? Address { get; set; } 
        public DateTime DateOfBirth { get; set; }
        public IFormFile? Picture { get; set; }

        // Navigation Property (One Patient → Many Appointments)
        public virtual List<Appointment> Appointments { get; set; } 
    }
}
