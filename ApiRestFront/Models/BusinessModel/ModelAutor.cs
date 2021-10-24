using ApiRestFront.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiRestFront.Models.BusinessModel
{
    public class ModelAutor
    {
        public List<Autor> BuscarAutor(Autor autor,out int statusCode, out string respuesta)
        {
            Peticiones<Autor, List<Autor>> model = new Peticiones<Autor, List<Autor>>();
            string url = "https://localhost:44341/api/Autor/BuscarAutor";
            List<Autor> listado = new List<Autor>();
            listado = model.EjecutarPeticionGET(url, autor, listado, out statusCode, out respuesta);
            return listado;
        }

        public Autor InsertarAutor(Autor autor, out int statusCode, out string respuesta)
        {
            Peticiones<Autor, Autor> model = new Peticiones<Autor, Autor>();
            string url = "https://localhost:44341/api/Autor/RegistrarAutor";
            Autor autorRespuesta = new Autor();
            autorRespuesta = model.EjecutarPeticionPOST(url, autor, autorRespuesta, out statusCode, out respuesta);
            return autorRespuesta;
        }
    }
}