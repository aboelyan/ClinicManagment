using ClinicManagment.Data;
using ClinicManagment.Models.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ClinicManagment.Controllers
{
    public class AccountController : Controller
    {
        private readonly ClinicDbContext _context;

        public AccountController(ClinicDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Dashboard", "Clinic");

            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            if (ModelState.IsValid)
            {
                var clinic = await _context.Clinics
                    .FirstOrDefaultAsync(c => c.Email == model.Email);

                if (clinic == null)
                {
                    ModelState.AddModelError(string.Empty, "البريد الإلكتروني غير مسجل");
                    return View(model);
                }

                if (clinic.Password != model.Password) // في الواقع يجب استخدام تشفير لكلمة المرور
                {
                    ModelState.AddModelError(string.Empty, "كلمة المرور غير صحيحة");
                    return View(model);
                }

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, clinic.Name),
                    new Claim(ClaimTypes.Email, clinic.Email),
                    new Claim(ClaimTypes.NameIdentifier, clinic.Id.ToString()),
                    new Claim("ClinicId", clinic.Id.ToString())
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = model.RememberMe,
                    RedirectUri = returnUrl
                };

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);

                return RedirectToLocal(returnUrl);
            }

            return View(model);
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);
            
            return RedirectToAction("Dashboard", "Clinic");
        }

        [Authorize]
        public async Task<IActionResult> ChangePassword()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var clinic = await _context.Clinics.FindAsync(int.Parse(userId));
            
            if (clinic == null)
                return NotFound();

            var model = new ChangePasswordViewModel();
            return View(model);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var clinic = await _context.Clinics.FindAsync(int.Parse(userId));

            if (clinic == null)
                return NotFound();

            if (clinic.Password != model.CurrentPassword)
            {
                ModelState.AddModelError(string.Empty, "كلمة المرور الحالية غير صحيحة");
                return View(model);
            }

            clinic.Password = model.NewPassword;
            await _context.SaveChangesAsync();

            // إعادة تسجيل الدخول بعد تغيير كلمة المرور
            await HttpContext.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }
    }
}
