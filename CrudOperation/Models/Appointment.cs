using System.ComponentModel.DataAnnotations.Schema;

namespace CrudOperation.Models
{
    public class Appointment
    {
        public long Id { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string? Reason { get; set; } = null!;

        // Foreign Keys
        [ForeignKey("Doctor")]
        public long? DoctorId { get; set; }
        [ForeignKey("Patient")]
        public long? PatientId { get; set; }

        // Navigation properties
        public virtual Doctor? Doctor { get; set; }
        public virtual Patient? Patient { get; set; }
    }

}
