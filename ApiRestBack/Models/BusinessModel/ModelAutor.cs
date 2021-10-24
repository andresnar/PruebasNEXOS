using ApiRestBack.Models.ModelSQL;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace ApiRestBack.Models.BusinessModel
{
    public class ModelAutor
    {
        public string CrearAutor(autor autor)
        {
            ValidacionAutores validaciones = new ValidacionAutores();
            string respuesta = validaciones.ValidaObjeto(autor);
            if (respuesta.Equals("OK"))
            {
                string validaRepetido = validaciones.NoRepetido(autor);
                if (validaRepetido.Equals("OK"))
                {
                    try
                    {
                        var db = Conexion.CrearConexion();
                        db.autor.Add(autor);
                        db.SaveChanges();
                        return "OK";
                    }
                    catch (Exception ex)
                    {
                        return "Error: " + ex.Message.ToString();
                    }
                }
                else
                {
                    return validaRepetido;
                }
            }
            else
                return respuesta;
        }

        public List<autor> BuscarAutor(autor autor, out string respuesta)
        {
            var db = Conexion.CrearConexion();
            List<autor> resultado = new List<autor>();
            string sql = "SELECT * FROM NEXOS.AUTOR ";
            try
            {
                if (autor.nombre != null || autor.fechanacimiento != null || autor.ciudad != null || autor.email != null)
                {
                    sql += " WHERE ";
                    if (autor.nombre != null && !String.IsNullOrEmpty(autor.nombre.ToString()))
                        sql += $" nombre = '{autor.nombre}' and";
                    if (autor.fechanacimiento != null)
                    {
                        string fecha = String.Format("{0:yyyy-dd-MM}", autor.fechanacimiento);
                        sql += $" fechanacimiento = '{fecha}' and";
                    }
                    if (autor.ciudad != null && !String.IsNullOrEmpty(autor.ciudad.ToString()))
                        sql += $" ciudad = '{autor.ciudad}' and";
                    if (autor.email != null && !String.IsNullOrEmpty(autor.email.ToString()))
                        sql += $" email = '{autor.email.ToString()}' and";
                    sql = sql.Substring(0, sql.Length - 3);
                }
                

                resultado = db.Database.SqlQuery<autor>(sql).ToList();
                respuesta = "OK";
                return resultado;
            }
            catch (Exception ex)
            {
                respuesta = "Error: " + ex.Message.ToString();
                return null;
            }
        }

    }
}