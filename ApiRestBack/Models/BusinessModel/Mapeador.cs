using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace ApiRestBack.Models.BusinessModel
{
    public class Mapeador <T,U>
    {
        public U Mapear(T objetoIn, U objetoOut)
        {
            PropertyInfo[] inPropiedades = typeof(T).GetProperties();
            PropertyInfo[] outPropiedades = typeof(U).GetProperties();
            try
            {
                foreach (PropertyInfo item in outPropiedades)
                {                    
                    foreach (PropertyInfo item2 in inPropiedades)
                    {
                        if (item2.Name.ToString().Equals(item.Name.ToString()))
                        {
                            var contenido = item2.GetValue(objetoIn);
                            item.SetValue(objetoOut, contenido);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            return objetoOut;
        }
    }
}