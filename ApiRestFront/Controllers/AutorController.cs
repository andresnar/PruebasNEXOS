using ApiRestFront.Models.BusinessModel;
using ApiRestFront.Models.Entities;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ApiRestFront.Controllers
{
    public class AutorController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult Buscar(Autor autor)
        {
            if (ModelState.IsValid)
            {
                ModelAutor model = new ModelAutor();
                int statusCode;
                string respuesta;
                List<Autor> listado = model.BuscarAutor(autor, out statusCode, out respuesta);
                if(statusCode == 200)
                {
                    return View(listado);
                }
                return RedirectToAction("Error", new { statusCode = statusCode, respuesta = respuesta });
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Insertar(Autor autor)
        {
            if (ModelState.IsValid)
            {
                ModelAutor model = new ModelAutor();
                int statusCode;
                string respuesta;
                Autor autorRespuesta = model.InsertarAutor(autor, out statusCode, out respuesta);
                if (statusCode == 200)
                {
                    return Redirect("/Autor/Index");
                    return RedirectToAction("Index","Autor");
                }
                return RedirectToAction("Error", new { statusCode = statusCode, respuesta = respuesta });
            }
            return RedirectToAction("Index","Autor");
        }

        [HttpGet]
        public ActionResult Error(int statusCode,string respuesta)
        {
            ViewBag.statusCode = statusCode;
            ViewBag.mensaje = respuesta;
            return View();
        }
    }
}