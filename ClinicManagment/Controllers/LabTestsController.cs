using ClinicManagment.Data;
using ClinicManagment.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ClinicManagment.Controllers
{
    [Authorize]
    public class LabTestsController : Controller
    {
        private readonly ClinicDbContext _context;

        public LabTestsController(ClinicDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var labTests = await _context.LabTests
                .Include(l => l.Patient)
                .OrderByDescending(l => l.Date)
                .ToListAsync();
            return View(labTests);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Patients = new SelectList(await _context.Patients.ToListAsync(), "Id", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(LabTest labTest)
        {
            if (ModelState.IsValid)
            {
                labTest.Date = DateTime.Now;
                _context.LabTests.Add(labTest);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Patients = new SelectList(await _context.Patients.ToListAsync(), "Id", "Name");
            return View(labTest);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var labTest = await _context.LabTests.FindAsync(id);
            if (labTest == null) return NotFound();
            
            ViewBag.Patients = new SelectList(await _context.Patients.ToListAsync(), "Id", "Name");
            return View(labTest);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(LabTest labTest)
        {
            if (ModelState.IsValid)
            {
                _context.Update(labTest);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Patients = new SelectList(await _context.Patients.ToListAsync(), "Id", "Name");
            return View(labTest);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var labTest = await _context.LabTests.FindAsync(id);
            if (labTest != null)
            {
                _context.LabTests.Remove(labTest);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
