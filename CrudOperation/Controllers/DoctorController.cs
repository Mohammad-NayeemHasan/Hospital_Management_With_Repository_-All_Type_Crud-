using CrudOperation.Models;
using CrudOperation.Repository;
using CrudOperation.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CrudOperation.Controllers
{
    public class DoctorController : Controller
    {
        private readonly IRepository<Doctor> _doctorRepo;
        private readonly IRepository<Appointment> _appointmentRepo;
        private readonly IRepository<Patient> _patientRepo;
        public DoctorController(IRepository<Doctor> doctorRepo, IRepository<Appointment> appointmentRepo, IRepository<Patient> patientRepo)
        {
            _doctorRepo = doctorRepo;
            _appointmentRepo = appointmentRepo;
            _patientRepo = patientRepo;
        }
       public async Task<IActionResult> Index()
        {
            var doctors = await _doctorRepo.GetAll(); // ✅ await the async method
            return View(doctors); // ✅ doctors is now List<Doctor>
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Patients = await _patientRepo.GetAll();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(DoctorCreateViewModel model)
        {
            try
            {
                var Doctor = new Doctor
                {
                    Name = model.Name,
                    Specialization = model.Specialization,
                    Phone = model.Phone

                };
                await _doctorRepo.AddAsync(Doctor);
                foreach (var appointmentModel in model.Appointments)
                {
                    Appointment appointment = new Appointment
                    {
                        //DoctorId = Doctor.Id,
                        PatientId = appointmentModel.PatientId,
                        AppointmentDate = appointmentModel.AppointmentDate,
                        Reason = appointmentModel.Reason,
                    };
                    // Assuming you have an appointment repository to save appointments
                    await _appointmentRepo.AddAsync(appointment);
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }


        }

    }
}

