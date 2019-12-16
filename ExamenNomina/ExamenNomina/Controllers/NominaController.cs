using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ExamenNomina.Models;

namespace ExamenNomina.Controllers
{
    public class NominaController : Controller
    {
        private NominaDBEntities db = new NominaDBEntities();

        // GET: Nomina
        public ActionResult Index(String Fecha) //recibe una fecha
        {
           
            ViewBag.Fecha = new SelectList((from m in db.Nominas
                                            select  m.Fecha).Distinct()); //Obtiene todas las fechas de nominas existententes y las envia al buscador por fecha

            var busqueda = from s in db.Nominas select s; //obtiene todos los registro guardados en la tabla nomina

            busqueda = busqueda.Where(s =>  s.Empleado.Activo == true); // filtra los empleados no activos

            if (!String.IsNullOrEmpty(Fecha)) //si la fecha recibida no es null 
            {
                busqueda = busqueda.Where(s => s.Fecha == Fecha); //filta los registros y solo muestra deacuerdo a la fecha pedida y los activos

                if (busqueda.Count()>0)// asegura que la conslta tenga valores para evitar un error
                {
                    ViewBag.Total = busqueda.Select(o => o.SueldoQuincenal).Sum(); // suma todos los sueldo quincenales del anterior filtrado
                }
            }
            else //si la fecha es null
            {
                busqueda = busqueda.GroupBy(p => p.Empleado).Select(g => g.OrderByDescending(p => p.Fecha).FirstOrDefault()).OrderByDescending(p => p.Fecha); //muestra los registro con la fecha mas actual
                
                var query = db.Nominas.Max(e => e.Fecha);
                busqueda = busqueda.Where(p => p.Fecha == query);

                if (busqueda.Count() > 0)// asegura que la conslta tenga valores para evitar un error
                {
                    ViewBag.Total = busqueda.Select(o => o.SueldoQuincenal).Sum();// suma todos los sueldo quincenales del anterior filtrado
                }
            }

            
            return View(busqueda.ToList());//envia a la vista Nomina el filtrado
        }
        public ActionResult NominasPorEmpleado(int? id)
        {
            var nominas = db.Nominas.Include(n => n.Empleado).Where(n => n.Empleado.IdEmpleado == id);

            return View(nominas.ToList());
        }
        // GET: Nomina/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Nomina nomina = db.Nominas.Find(id);
            if (nomina == null)
            {
                return HttpNotFound();
            }
            return View(nomina);
        }

        // GET: Nomina/Create
        public ActionResult Create()
        {

            ViewBag.IdEmpleado = new SelectList(db.Empleadoes, "IdEmpleado", "NomEmpleado");

            return View() ;
        }

        // POST: Nomina/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdNomina,IdEmpleado,Fecha,Dias,SueldoQuincenal")] Nomina nomina)
        {
            if (ModelState.IsValid)
            {
                
                db.Nominas.Add(nomina);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdEmpleado = new SelectList(db.Empleadoes, "IdEmpleado", "NomEmpleado", nomina.IdEmpleado);
            return View(nomina);
        }

        [HttpGet]
        public JsonResult BuscarSueldo(int Id_Emplea)
        {
            try
            {
                var sueldo = (from p in db.Empleadoes
                             where p.IdEmpleado == Id_Emplea
                             select p.Sueldo).ToList();

                return Json(sueldo, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json("qqq", JsonRequestBehavior.AllowGet);
            }
            
        }

        // GET: Nomina/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Nomina nomina = db.Nominas.Find(id);
            if (nomina == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdEmpleado = new SelectList(db.Empleadoes, "IdEmpleado", "NomEmpleado", nomina.IdEmpleado);
            return View(nomina);
        }

        // POST: Nomina/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdNomina,IdEmpleado,Fecha,Dias,SueldoQuincenal")] Nomina nomina)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nomina).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdEmpleado = new SelectList(db.Empleadoes, "IdEmpleado", "NomEmpleado", nomina.IdEmpleado);
            return View(nomina);
        }

        // GET: Nomina/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Nomina nomina = db.Nominas.Find(id);
            if (nomina == null)
            {
                return HttpNotFound();
            }
            return View(nomina);
        }

        // POST: Nomina/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Nomina nomina = db.Nominas.Find(id);
            db.Nominas.Remove(nomina);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
