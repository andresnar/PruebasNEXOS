using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ApiRestBack.Models.ModelSQL;

namespace ApiRestBack.Models.BusinessModel
{
    public class Conexion
    {
        public static nexosEntities CrearConexion()
        {
            nexosEntities db = new nexosEntities();
            return db;
        }

        public static bool ProbarConexion()
        {
            try
            {
                nexosEntities db = new nexosEntities();
                var prueba = db.ciudad.Count();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }

        }
    }
}