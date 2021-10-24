using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ApiRestBack.Models.Interfaces;
using ApiRestBack.Models.ModelSQL;


namespace ApiRestBack.Models.BusinessModel
{
    public class ModelLibro : ILibros
    {
        private int maximo { get; set; }

        public ModelLibro(int maximo)
        {
            this.maximo = maximo;
        }
        public string CrearLibro(libro libro)
        {
            ValidacionLibros validaciones = new ValidacionLibros();
            string respuesta = validaciones.ValidaObjeto(libro);
            if (respuesta.Equals("OK"))
            {
                string validaRepetido = validaciones.NoRepetido(libro);
                string validaExiste = validaciones.AutorExiste(libro.autor);
                string validaMax = validaciones.ValidaMaxLibros(maximo);
                if (validaRepetido.Equals("OK") && validaExiste.Equals("OK") && validaMax.Equals("OK"))
                {
                    try
                    {
                        var db = Conexion.CrearConexion();
                        db.libro.Add(libro);
                        db.SaveChanges();
                        return "OK";
                    }
                    catch (Exception ex)
                    {
                        return "Error: " + ex.Message.ToString();
                    }

                }
                return "Error : " + (!validaRepetido.Equals("OK")? validaRepetido + "," : "" ) 
                    + (!validaExiste.Equals("OK")? validaExiste + "," : "")
                    + (!validaMax.Equals("OK")? validaMax : "");
            }
            return respuesta;
        }

        public List<libro> BuscarLibro(libro libro, out string respuesta)
        {
            var db = Conexion.CrearConexion();
            List<libro> resultado = new List<libro>();
            string sql = "SELECT * FROM NEXOS.LIBRO ";
            try
            {
                if (libro.titulo != null || libro.anno != null || libro.genero != null || libro.paginas != null || libro.autor != null)
                {
                    sql += " WHERE ";
                    if (libro.titulo != null && !String.IsNullOrEmpty(libro.titulo.ToString()))
                        sql += $" titulo = '{libro.titulo}' and";
                    if (libro.anno != null && libro.anno != 0)
                        sql += $" anno = '{libro.anno.ToString()}' and";
                    if (libro.genero != null && !String.IsNullOrEmpty(libro.genero.ToString()))
                        sql += $" genero = '{libro.genero}' and";
                    if (libro.paginas != null && libro.paginas != 0)
                        sql += $" paginas = '{libro.paginas.ToString()}' and";
                    if (libro.autor != null && !String.IsNullOrEmpty(libro.autor.ToString()))
                        sql += $" autor = '{libro.autor}' and";
                    sql = sql.Substring(0, sql.Length - 3);
                }
                

                resultado = db.Database.SqlQuery<libro>(sql).ToList();
                respuesta = "OK";
                return resultado;
            }
            catch(Exception ex)
            {
                respuesta = "Error: " + ex.Message.ToString();
                return null;
            }
        }

    }
}
