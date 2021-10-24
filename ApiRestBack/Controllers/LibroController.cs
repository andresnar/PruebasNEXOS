using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using ApiRestBack.Models.DTO;
using ApiRestBack.Models.BusinessModel;
using ApiRestBack.Models.ModelSQL;

namespace ApiRestBack.Controllers
{
    public class LibroController : ApiController
    {

        [HttpPost]
        [ResponseType(typeof(JSONLIBRO))]
        public IHttpActionResult BuscarLibro(JObject jLibro)
        {
            string respuesta;
            // DesSerializa el JSON recibido a un objeto tipo JSONLIBRO
            JSONLIBRO inLibro = JsonConvert.DeserializeObject<JSONLIBRO>(jLibro.ToString());
            libro outLibro = new libro();

            if (inLibro != null)
            {
                // Mapea el DTO a un Entity para ingresar a la BD
                Mapeador<JSONLIBRO,libro> mapper = new Mapeador<JSONLIBRO, libro>();
                libro temporal = mapper.Mapear(inLibro,outLibro);
                ModelLibro model = new ModelLibro(3);
                List<libro> listadoRespuesta = model.BuscarLibro(temporal, out respuesta);
                if (listadoRespuesta != null && respuesta.Equals("OK"))
                {
                    return Ok(listadoRespuesta);
                }
                else
                    return Content(HttpStatusCode.NotFound, respuesta);
            }
            else
                return Content(HttpStatusCode.BadRequest, "Criterios de busqueda desconocidos.");
        }
        

        [HttpPost]
        [ResponseType(typeof(JSONLIBRO))]
        public IHttpActionResult RegistrarLibro(JObject jLibro)
        {
            string respuesta;
            if (Conexion.ProbarConexion())
            {
                try
                {
                    // DesSerializa el JSON recibido a un objeto tipo JSONLIBRO
                    JSONLIBRO inLibro = JsonConvert.DeserializeObject<JSONLIBRO>(jLibro.ToString());
                    libro outLibro = new libro();

                    // Valida que el JSON estè completo para la inserciòn en la BD
                    ValidadorJSON<JSONLIBRO> validador = new ValidadorJSON<JSONLIBRO>();
                    respuesta = validador.ValidarEstado(inLibro);
                    if (respuesta.Equals("OK"))
                    {
                        // Mapea el DTO a un Entity para ingresar a la BD
                        Mapeador<JSONLIBRO, libro> mapper = new Mapeador<JSONLIBRO, libro>();
                        libro temporal = mapper.Mapear(inLibro,outLibro);
                        if (temporal != null)
                        {
                            ModelLibro MLibro = new ModelLibro(13);
                            respuesta = MLibro.CrearLibro(temporal);
                            if (respuesta.Equals("OK"))
                            {
                                return Ok(jLibro);
                            }
                            else
                            {
                                return Content(HttpStatusCode.BadRequest, respuesta);
                            }
                        }
                        else
                            return Content(HttpStatusCode.BadRequest, "Error Al leer el objeto, consulte al administrador.");
                    }
                    else
                        return Content(HttpStatusCode.BadRequest, respuesta);
                }
                catch (Exception ex)
                {
                    return InternalServerError(new Exception("Error: " + ex.Message.ToString()));
                }
            }
            else
            {
                return InternalServerError(new Exception("Error: No hay conexion"));
            }
        }
    }
}