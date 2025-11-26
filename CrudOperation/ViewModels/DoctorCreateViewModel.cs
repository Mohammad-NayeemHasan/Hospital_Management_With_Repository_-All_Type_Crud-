using CrudOperation.Models;

namespace CrudOperation.ViewModels
{
    public class DoctorCreateViewModel
    {
        public DoctorCreateViewModel()
        {
            Appointments = new List<Appointment>();
        }
        public long Id { get; set; }
        public string? Name { get; set; }
        public string? Specialization { get; set; }
        public string? Phone { get; set; }

        // Navigation Property (One Doctor → Many Appointments)
        public virtual List<Appointment> Appointments { get; set; }
    }
}
