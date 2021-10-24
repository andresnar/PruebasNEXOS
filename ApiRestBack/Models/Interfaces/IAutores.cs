using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApiRestBack.Models.ModelSQL;

namespace ApiRestBack.Models.Interfaces
{
    interface IAutores
    {
        string CrearAutor(autor autor);
    }
}
