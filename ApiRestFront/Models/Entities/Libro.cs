using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiRestFront.Models.Entities
{
    public class Libro
    {
        public int id { get; set; }
        public string titulo { get; set; }
        public Nullable<int> anno { get; set; }
        public string genero { get; set; }
        public Nullable<int> paginas { get; set; }
        public string autor { get; set; }
    }
}