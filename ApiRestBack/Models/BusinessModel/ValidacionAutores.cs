using ApiRestBack.Models.ModelSQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace ApiRestBack.Models.BusinessModel
{
    public class ValidacionAutores
    {
        public string ValidaObjeto(autor autor)
        {
            string respuesta = "OK";
            PropertyInfo[] listado = typeof(autor).GetProperties();
            foreach (PropertyInfo item in listado)
            {
                if (!item.Name.ToString().Equals("id"))
                {
                    if (item.GetValue(autor) == null)
                    {
                        respuesta = "Error: Datos incompletos";
                    }
                }                
            }
            return respuesta;
        }

        public string NoRepetido(autor autor)
        {
            string respuesta = "";
            try
            {
                var db = Conexion.CrearConexion();
                int contador = db.autor.Where(c => c.nombre == autor.nombre).Count();
                if (contador > 0)
                {
                    respuesta = "El autor ya existe.";
                }
                else
                {
                    respuesta = "OK";
                }
            }
            catch (Exception ex)
            {
                respuesta = "Error: " + ex.Message.ToString();
            }
            return respuesta;
        }
    }
}