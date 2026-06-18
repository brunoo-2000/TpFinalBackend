using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TpfinalBack.Data;
using TpfinalBack.DTOs;
using TpfinalBack.Models;

namespace TpfinalBack.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly PasswordHasher<Usuario> _passwordHasher;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
            _passwordHasher = new PasswordHasher<Usuario>();
        }

        [HttpGet]
        public IActionResult Login()
        {
            if(HttpContext.Session.GetString("UsuarioId") != null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginDto model)
        {
            if (!ModelState.IsValid)
            {
                return View(model); 
            }

            var usuarioValido = _context.Usuario.FirstOrDefault(u => u.Username == model.Username);
            
            if(usuarioValido == null)
            {
                ModelState.AddModelError(string.Empty, "Usuario o contraseña incorrectos.");
                return View(model);
            }

            var resultado = _passwordHasher.VerifyHashedPassword(usuarioValido, usuarioValido.PasswordHash, model.Password);

            if(resultado == PasswordVerificationResult.Failed)
            {
                ModelState.AddModelError(string.Empty, "Usuario o contraseña incorrectos. ");
                return View(model);
            }

            HttpContext.Session.SetString("UsuarioId", usuarioValido.Id.ToString());
            HttpContext.Session.SetString("Username", usuarioValido.Username);
            HttpContext.Session.SetString("Rol", usuarioValido.Rol);

            return RedirectToAction("Index", "Home");

        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear(); 
            return RedirectToAction("Login"); 
        }

    }
}
