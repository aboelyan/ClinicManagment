using ClinicManagment.Data;
using ClinicManagment.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClinicManagment.Controllers
{
    [Authorize]
    public class PatientsController : Controller
    {
        private readonly ClinicDbContext _context;

        public PatientsController(ClinicDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var patients = await _context.Patients
                .Include(p => p.Appointments)
                .Include(p => p.LabTests)
                .ToListAsync();
            return View(patients);
        }

        public IActionResult Create()
        {
            ViewBag.ClinicId = User.FindFirst("ClinicId")?.Value;
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Patient patient)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // التحقق من عدم تكرار رقم الهاتف
                    if (await _context.Patients.AnyAsync(p => p.Phone == patient.Phone))
                    {
                        ModelState.AddModelError("Phone", "رقم الهاتف مستخدم مسبقاً");
                        return View(patient);
                    }

                    // الحصول على ClinicId من المستخدم
                    var clinicIdClaim = User.FindFirst("ClinicId")?.Value;
                    Console.WriteLine($"ClinicId from Claims: {clinicIdClaim}"); // طباعة ClinicId

                    if (string.IsNullOrEmpty(clinicIdClaim))
                    {
                        ModelState.AddModelError("", "لم يتم العثور على العيادة المرتبطة بالمستخدم.");
                        return View(patient);
                    }

                    patient.ClinicId = int.Parse(clinicIdClaim); // تعيين ClinicId

                    // إضافة المريض إلى قاعدة البيانات
                    _context.Patients.Add(patient);
                    await _context.SaveChangesAsync();

                    TempData["Success"] = "تم إضافة المريض بنجاح";
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"حدث خطأ أثناء الحفظ: {ex.Message}");
            }

            return View(patient);
        }
        public async Task<IActionResult> Edit(int id)
        {
            var patient = await _context.Patients.FindAsync(id);
            if (patient == null) return NotFound();
            return View(patient);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Patient patient)
        {
            if (ModelState.IsValid)
            {
                _context.Update(patient);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(patient);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var patient = await _context.Patients.FindAsync(id);
            if (patient != null)
            {
                _context.Patients.Remove(patient);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id)
        {
            var patient = await _context.Patients
                .Include(p => p.Appointments)
                .ThenInclude(a => a.Doctor)
                .Include(p => p.LabTests)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (patient == null) return NotFound();
            return View(patient);
        }
    }
}
