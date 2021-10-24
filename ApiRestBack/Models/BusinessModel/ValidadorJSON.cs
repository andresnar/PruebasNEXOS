using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace ApiRestBack.Models.BusinessModel
{
    public class ValidadorJSON <T>
    {
        public string ValidarEstado (T objeto)
        {
            string respuesta = "OK";
            PropertyInfo[] listado = typeof(T).GetProperties();
            try
            {
                foreach (PropertyInfo item in listado)
                {
                    if (!item.Name.ToString().Equals("id"))
                    {
                        if (item.GetValue(objeto) == null)
                        {
                            respuesta = "Error: Datos incompletos";
                        }
                    }
                }
            }
            catch
            {
                respuesta = "Error: Datos incompletos";
            }
            
            return respuesta;
        }
    }
}
