using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiRestFront.Models.Entities
{
    public class Autor
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public Nullable<System.DateTime> fechanacimiento { get; set; }
        public string ciudad { get; set; }
        public string email { get; set; }
    }
}