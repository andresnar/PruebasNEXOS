using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiRestBack.Models.DTO
{
    public class JSONLIBRO
    {
        public string titulo { get; set; }
        public Nullable<int> anno { get; set; }
        public string genero { get; set; }
        public Nullable<int> paginas { get; set; }
        public string autor { get; set; }
    }
}