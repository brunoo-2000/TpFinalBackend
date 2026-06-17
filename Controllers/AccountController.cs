using Microsoft.AspNetCore.Mvc;
using TpfinalBack.Data;
using TpfinalBack.DTOs;
using TpfinalBack.Models;

namespace TpfinalBack.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
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

            
            
             var usuarioValido = _context.Usuarios.FirstOrDefault(u => u.Username == model.Username && u.Password == model.Password);
            
            
            

            if(usuarioValido == null)
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
