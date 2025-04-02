using ClinicManagment.Data;
using ClinicManagment.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClinicManagment.Controllers
{
    [Authorize]
    public class AppointmentsController : Controller
    {
        private readonly ClinicDbContext _context;

        public AppointmentsController(ClinicDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var appointments = await _context.Appointments
                .Include(a => a.Patient)
                .Include(a => a.Doctor)
                .ToListAsync();
            return View(appointments);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Patients = await _context.Patients
                .Select(p => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Value = p.Id.ToString(),
                    Text = p.Name
                })
                .ToListAsync();
            ViewBag.Doctors = await _context.Doctors
                .Select(d => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Value = d.Id.ToString(),
                    Text = d.Name
                })
                .ToListAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Appointment appointment)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    throw new InvalidOperationException("بيانات غير صالحة");
                }

                // Validate business rules
                if (appointment.EndTime <= appointment.StartTime)
                {
                    ModelState.AddModelError("EndTime", "يجب أن يكون وقت الانتهاء بعد وقت البدء");
                }

                // Check for conflicting appointments
                var conflict = await _context.Appointments
                    .AnyAsync(a => a.DoctorId == appointment.DoctorId &&
                                 a.AppointmentDate == appointment.AppointmentDate &&
                                 ((a.StartTime <= appointment.StartTime && a.EndTime > appointment.StartTime) ||
                                  (a.StartTime < appointment.EndTime && a.EndTime >= appointment.EndTime) ||
                                  (a.StartTime >= appointment.StartTime && a.EndTime <= appointment.EndTime)));

                if (conflict)
                {
                    ModelState.AddModelError("", "هذا الموعد يتعارض مع موعد آخر للطبيب");
                }

                if (!ModelState.IsValid)
                {
                    await PopulateViewBags();
                    return View(appointment);
                }

                _context.Appointments.Add(appointment);
                await _context.SaveChangesAsync();

                TempData["Success"] = "تم إضافة الموعد بنجاح";
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException ex)
            {
                ModelState.AddModelError("", "خطأ في قاعدة البيانات: " + ex.InnerException?.Message ?? ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "حدث خطأ غير متوقع: " + ex.Message);
            }

            await PopulateViewBags();
            return View(appointment);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment == null) return NotFound();

            ViewBag.Patients = await _context.Patients
                .Select(p => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Value = p.Id.ToString(),
                    Text = p.Name
                })
                .ToListAsync();
            ViewBag.Doctors = await _context.Doctors
                .Select(d => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Value = d.Id.ToString(),
                    Text = d.Name
                })
                .ToListAsync();
            return View(appointment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Appointment appointment)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    throw new InvalidOperationException("بيانات غير صالحة");
                }

                // Validate business rules
                if (appointment.EndTime <= appointment.StartTime)
                {
                    ModelState.AddModelError("EndTime", "يجب أن يكون وقت الانتهاء بعد وقت البدء");
                }

                // Check for conflicting appointments (excluding current appointment)
                var conflict = await _context.Appointments
                    .AnyAsync(a => a.Id != appointment.Id &&
                                 a.DoctorId == appointment.DoctorId &&
                                 a.AppointmentDate == appointment.AppointmentDate &&
                                 ((a.StartTime <= appointment.StartTime && a.EndTime > appointment.StartTime) ||
                                  (a.StartTime < appointment.EndTime && a.EndTime >= appointment.EndTime) ||
                                  (a.StartTime >= appointment.StartTime && a.EndTime <= appointment.EndTime)));

                if (conflict)
                {
                    ModelState.AddModelError("", "هذا الموعد يتعارض مع موعد آخر للطبيب");
                }

                if (!ModelState.IsValid)
                {
                    await PopulateViewBags();
                    return View(appointment);
                }

                _context.Update(appointment);
                await _context.SaveChangesAsync();
                TempData["Success"] = "تم تعديل الموعد بنجاح";
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException ex)
            {
                ModelState.AddModelError("", "خطأ في قاعدة البيانات: " + ex.InnerException?.Message ?? ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "حدث خطأ غير متوقع: " + ex.Message);
            }

            await PopulateViewBags();
            return View(appointment);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var appointment = await _context.Appointments.FindAsync(id);
                if (appointment == null)
                {
                    TempData["Error"] = "لم يتم العثور على الموعد المطلوب";
                    return RedirectToAction(nameof(Index));
                }

                _context.Appointments.Remove(appointment);
                await _context.SaveChangesAsync();
                TempData["Success"] = "تم حذف الموعد بنجاح";
            }
            catch (DbUpdateException ex)
            {
                TempData["Error"] = "خطأ في قاعدة البيانات: " + ex.InnerException?.Message ?? ex.Message;
            }
            catch (Exception ex)
            {
                TempData["Error"] = "حدث خطأ غير متوقع أثناء الحذف: " + ex.Message;
            }
            
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id)
        {
            var appointment = await _context.Appointments
                .Include(a => a.Patient)
                .Include(a => a.Doctor)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (appointment == null) return NotFound();
            return View(appointment);
        }

        private async Task PopulateViewBags()
        {
            ViewBag.Patients = await _context.Patients
                .Select(p => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Value = p.Id.ToString(),
                    Text = p.Name
                })
                .ToListAsync();
            ViewBag.Doctors = await _context.Doctors
                .Select(d => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Value = d.Id.ToString(),
                    Text = d.Name
                })
                .ToListAsync();
        }
    }
}