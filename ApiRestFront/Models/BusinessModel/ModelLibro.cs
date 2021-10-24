using ApiRestFront.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiRestFront.Models.BusinessModel
{
    public class ModelLibro
    {
        public List<Libro> BuscarLibro(Libro libro, out int statusCode, out string respuesta)
        {
            Peticiones<Libro, List<Libro>> model = new Peticiones<Libro, List<Libro>>();
            string url = "https://localhost:44341/api/Libro/BuscarLibro";
            List<Libro> listado = new List<Libro>();
            listado = model.EjecutarPeticionGET(url, libro, listado, out statusCode, out respuesta);
            return listado;
        }

        public Libro InsertarLibro(Libro libro, out int statusCode, out string respuesta)
        {
            Peticiones<Libro, Libro> model = new Peticiones<Libro, Libro>();
            string url = "https://localhost:44341/api/Libro/RegistrarLibro";
            Libro libroRespuesta = new Libro();
            libroRespuesta = model.EjecutarPeticionPOST(url, libro, libroRespuesta, out statusCode, out respuesta);
            return libroRespuesta;
        }
    }
}