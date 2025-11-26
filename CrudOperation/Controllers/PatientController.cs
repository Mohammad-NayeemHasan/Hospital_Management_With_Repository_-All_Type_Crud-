using CrudOperation.Models;
using CrudOperation.Repository;
using CrudOperation.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CrudOperation.Controllers
{
    public class PatientController : Controller
    {
        private readonly IRepository<Patient> _patientRepo;
        private readonly IRepository<Doctor> _doctorRepo;
        private readonly IRepository<Appointment> _appointmentRepo;

        public PatientController(IRepository<Patient> patientRepo,
                           IRepository<Doctor> doctorRepo,
                           IRepository<Appointment> appointmentRepo)
        {
            _patientRepo = patientRepo;
            _doctorRepo = doctorRepo;
            _appointmentRepo = appointmentRepo;
        }

        public async Task<IActionResult> Index()
        {

            var patients = await _patientRepo.GetAll(q => q.Include(q => q.Appointments)
            .ThenInclude(q => q.Doctor));
            return View(patients);

        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Doctors = await _doctorRepo.GetAll();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(PatientCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Doctors = await _doctorRepo.GetAll();
                return View(model);
            }

            // ✅ Image Upload
            string fileName = "";

            if (model.Picture != null)
            {
                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Pictures");

                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                fileName = Guid.NewGuid().ToString() + Path.GetExtension(model.Picture.FileName);

                string filePath = Path.Combine(path, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await model.Picture.CopyToAsync(stream);
                }
            }

            // ✅ Save Patient
            Patient patient = new Patient
            {
                Name = model.Name,
                Age = model.Age,
                Phone = model.Phone,
                Address = model.Address,
                DateOfBirth = model.DateOfBirth,
                Picture = fileName
            };

            await _patientRepo.AddAsync(patient);   

            foreach (var appointmentModel in model.Appointments)
            {
                Appointment appointment = new Appointment
                {
                    DoctorId = appointmentModel.DoctorId,
                    AppointmentDate = appointmentModel.AppointmentDate,
                    Reason = appointmentModel.Reason
                };

                await _appointmentRepo.AddAsync(appointment); 
            }

            return RedirectToAction("Index");
        }


    }
}
