using System;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity;
using System.Security.Claims;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ExamenNomina.Models.ViewModel;
using ExamenNomina.Models;

namespace ExamenNomina.Controllers
{
    public class AccountController : BaseController
    {
        // GET: Account
        public ActionResult Login()
        {
            return View();
        }

        public Usuario GetUser(string email, string password)// recibo email y password
        {
            Usuario usuario;
            try
            {
                using (var Context = new NominaDBEntities())
                {
                    usuario = Context.Usuarios.FirstOrDefault(p => p.Email == email && p.Password == password); // si el usuario y la password coinciden con algun registro en la tabla
                }
                return usuario; //regresa el usuario encontrado
            }catch(Exception ex)
            {
                throw ex; //si no encuentra manda error
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Login(LoginVM data, string returnUrl)
        {
            ActionResult result;

            var usuario = GetUser(data.Email, data.Password); // llama la funcion GetUser descrita arriba

            if (usuario != null)//si el usuario es diferente de  null
            {

                result = SignInUser(usuario, data.RememberMe, returnUrl); //llama a la funcion SignInUser descrita abajo
            }
            else//si no
            {
                Danger("Los datos son incorrectos", false); //error
                result = View(data);//regresa a la vista

            }
            return result;
        }
        [AllowAnonymous]
        private ActionResult SignInUser(Usuario usuario, bool rememberMe, string returnUrl)
        {
            ActionResult Result;
            var Claims = new List<Claim>(); //creo una lista
            Claims.Add(new Claim(ClaimTypes.NameIdentifier, Convert.ToString(usuario.Id_Usuario)));// Identificador = a Id del usuario
            Claims.Add(new Claim(ClaimTypes.Name, usuario.Nombre)); //Name == Nombre
            Claims.Add(new Claim(ClaimTypes.Role, usuario.Rol)); // Role == Rol
            var Identity = new ClaimsIdentity(Claims, DefaultAuthenticationTypes.ApplicationCookie); //Cookies del navegador
            IAuthenticationManager AuthenticationManager = HttpContext.GetOwinContext().Authentication; // Autenticación

            AuthenticationManager.SignIn(new AuthenticationProperties()
            {
                IsPersistent = rememberMe
            }, Identity);
            if (string.IsNullOrEmpty(returnUrl))
            {
                returnUrl = Url.Action("Index", "Home");
            }
            Result = Redirect(returnUrl);
            return Result;
        }

        [Authorize]
        public ActionResult LogOff() // Deslogear
        {
            IAuthenticationManager AuthenticationManager = HttpContext.GetOwinContext().Authentication;
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");

        }
    }
}