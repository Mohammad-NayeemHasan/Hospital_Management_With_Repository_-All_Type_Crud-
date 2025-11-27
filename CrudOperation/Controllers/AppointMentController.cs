using CrudOperation.Models;
using CrudOperation.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CrudOperation.Controllers
{
    public class AppointMentController : Controller
    {
        private readonly IRepository<Appointment> _appointmentRepo;
        private readonly IRepository<Doctor> _doctorRepo;
        private readonly IRepository<Patient> _patientRepo;
        public AppointMentController(IRepository<Appointment> appointmentRepo, IRepository<Doctor> doctorRepo, IRepository<Patient> patientRepo)
        {
            _appointmentRepo = appointmentRepo;
            _doctorRepo = doctorRepo;
            _patientRepo = patientRepo;
        }
        public async Task<IActionResult> Index()
        {
            var appointment = await _appointmentRepo.GetAll(q => q.Include(q => q.Doctor).Include(q => q.Patient));
            return View(appointment);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var doctors = await _doctorRepo.GetAll();
            var patients = await _patientRepo.GetAll();

            ViewBag.Doctors = doctors.Select(d => new SelectListItem
            {
                Text = d.Name,     // Doctor Name field
                Value = d.Id.ToString()
            }).ToList();

            ViewBag.Patients = patients.Select(p => new SelectListItem
            {
                Text = p.Name,     // Patient Name field
                Value = p.Id.ToString()
            }).ToList();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                await _appointmentRepo.AddAsync(appointment);
                return RedirectToAction("Index");
            }

            var doctors = await _doctorRepo.GetAll();
            var patients = await _patientRepo.GetAll();

            ViewBag.Doctors = doctors.Select(d => new SelectListItem
            {
                Text = d.Name,
                Value = d.Id.ToString()
            });

            ViewBag.Patients = patients.Select(p => new SelectListItem
            {
                Text = p.Name,
                Value = p.Id.ToString()
            });

            return View(appointment);
        }

    }
}
