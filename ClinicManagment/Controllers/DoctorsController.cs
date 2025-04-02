using ClinicManagment.Data;
using ClinicManagment.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace ClinicManagment.Controllers
{
    [Authorize]
    public class DoctorsController : Controller
    {
        private readonly ClinicDbContext _context;

        public DoctorsController(ClinicDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var doctors = await _context.Doctors
                .Include(d => d.Clinic)
                .ToListAsync();
            return View(doctors);
        }

        public IActionResult Create()
        {
            ViewBag.Clinics = new SelectList(_context.Clinics.ToList(), "Id", "Name");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Doctor doctor)
        {
            Console.WriteLine("🔍 تم استدعاء إجراء Create - التحقق من ModelState...");

            if (!ModelState.IsValid)
            {
                Console.WriteLine("❌ ModelState غير صالح! تفاصيل الأخطاء:");
                foreach (var error in ModelState)
                {
                    Console.WriteLine($"🔹 الحقل: {error.Key}");
                    foreach (var subError in error.Value.Errors)
                    {
                        Console.WriteLine($"   ➜ الخطأ: {subError.ErrorMessage}");
                    }
                }

                TempData["Error"] = "حدث خطأ في البيانات المدخلة. يرجى التحقق من المدخلات.";
                ViewBag.Clinics = new SelectList(await _context.Clinics.ToListAsync(), "Id", "Name");
                return View(doctor);
            }

            try
            {
                Console.WriteLine("✅ ModelState صالح - يتم الحفظ في قاعدة البيانات...");
                _context.Doctors.Add(doctor);
                await _context.SaveChangesAsync();
                Console.WriteLine("🎉 تم حفظ الطبيب بنجاح!");

                TempData["Success"] = "تم إضافة الطبيب بنجاح!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"⚠️ خطأ أثناء الحفظ: {ex.Message}");
                TempData["Error"] = "حدث خطأ أثناء الحفظ. يرجى المحاولة مرة أخرى.";

                ViewBag.Clinics = new SelectList(await _context.Clinics.ToListAsync(), "Id", "Name");
                return View(doctor);
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            var doctor = await _context.Doctors.FindAsync(id);
            if (doctor == null) return NotFound();
            return View(doctor);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Doctor doctor)
        {
            if (ModelState.IsValid)
            {
                _context.Update(doctor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(doctor);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var doctor = await _context.Doctors.FindAsync(id);
            if (doctor != null)
            {
                _context.Doctors.Remove(doctor);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
