using ApiRestBack.Models.ModelSQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace ApiRestBack.Models.BusinessModel
{
    public class ValidacionLibros
    {
        public string ValidaObjeto(libro libro)
        {
            string respuesta = "OK";
            PropertyInfo[] listado = typeof(libro).GetProperties();
            foreach(PropertyInfo item in listado)
            {
                if (!item.Name.ToString().Equals("id"))
                {
                    if (item.GetValue(libro) == null)
                    {
                        respuesta = "Error: Datos incompletos";
                    }

                }
            }
            return respuesta;
        }

        public string NoRepetido(libro libro)
        {
            string respuesta = "";
            try
            {
                var db = Conexion.CrearConexion();
                int contador = db.libro.Where(c => c.titulo == libro.titulo).Count();
                if(contador >0)
                {
                    respuesta = "El Libro ya existe.";
                }
                else
                {
                    respuesta = "OK";
                }
            }
            catch(Exception ex)
            {
                respuesta = "Error: " + ex.Message.ToString();
            }
            return respuesta;
        }

        public string AutorExiste(string autor)
        {
            string respuesta = "";
            try
            {
                var db = Conexion.CrearConexion();
                int contador = db.autor.Where(x => x.nombre == autor).Count();
                if (contador == 0)
                    respuesta = "El autor no existe.";
                else
                    respuesta = "OK";
            }
            catch(Exception ex)
            {
                respuesta = "Error: " + ex.Message.ToString();
            }
            return respuesta;
        }

        public string ValidaMaxLibros(int maximo)
        {
            string respuesta = "";
            try
            {
                var db = Conexion.CrearConexion();
                int cantidad = db.libro.Count();
                if(cantidad < maximo)
                {
                    return "OK";
                }
                respuesta = "No es posible registrar el libro, se alcanzó el máximo permitido.";
            }
            catch(Exception ex)
            {
                respuesta = "Error: " + ex.Message.ToString();
            }
            return respuesta;
        }
    }
}