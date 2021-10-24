using ApiRestFront.Models.BusinessModel;
using ApiRestFront.Models.Entities;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ApiRestFront.Controllers
{
    public class LibroController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Buscar(Libro libro)
        {
            if (ModelState.IsValid)
            {
                ModelLibro model = new ModelLibro();
                int statusCode;
                string respuesta;
                List<Libro> listado = model.BuscarLibro(libro, out statusCode, out respuesta);
                if(statusCode == 200)
                {
                    return View(listado);
                }
                return RedirectToAction("Error", new { statusCode = statusCode, respuesta = respuesta });
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Insertar(Libro libro)
        {
            if (ModelState.IsValid)
            {
                ModelLibro model = new ModelLibro();
                int statusCode;
                string respuesta;
                Libro autorRespuesta = model.InsertarLibro(libro, out statusCode, out respuesta);
                if (statusCode == 200)
                {
                    return Redirect("/Libro/Index");
                    //return RedirectToAction("Index","Libro");
                }
                return RedirectToAction("Error", new { statusCode = statusCode, respuesta = respuesta });
            }
            return RedirectToAction("Index","Libro");
        }

        [HttpGet]
        public ActionResult Error(int statusCode, string respuesta)
        {
            ViewBag.statusCode = statusCode;
            ViewBag.mensaje = respuesta;
            return View();
        }
    }
}