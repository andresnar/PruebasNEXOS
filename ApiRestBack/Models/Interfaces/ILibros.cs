using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApiRestBack.Models.ModelSQL;

namespace ApiRestBack.Models.Interfaces
{
    interface ILibros
    {
        string CrearLibro(libro libro);
        List<libro> BuscarLibro(libro libro, out string respuesta);
    }
}
