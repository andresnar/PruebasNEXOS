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
    public class AutorController : ApiController
    {
        [HttpPost]
        [ResponseType(typeof(JSONAUTOR))]
        public IHttpActionResult BuscarAutor(JObject jAutor)
        {
            string respuesta;

            // DesSerializa el JSON recibido a un objeto tipo JSONAUTOR
            JSONAUTOR inAutor = JsonConvert.DeserializeObject<JSONAUTOR>(jAutor.ToString());
            autor outAutor = new autor();
            if(inAutor != null)
            {
                // Mapea el DTO a un Entity para ingresar a la BD
                Mapeador<JSONAUTOR,autor> mapper = new Mapeador<JSONAUTOR, autor>();
                autor temporal = mapper.Mapear(inAutor,outAutor);

                ModelAutor model = new ModelAutor();
                List<autor> listadoRespuesta = model.BuscarAutor(temporal, out respuesta);
                if (listadoRespuesta != null && respuesta.Equals("OK"))
                {
                    return Ok(listadoRespuesta);
                }
                else
                    return Content(HttpStatusCode.NotFound, respuesta);
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, "No Envia campos de búsqueda.");
            }

        }


        [HttpPost]
        [ResponseType(typeof(JSONAUTOR))]
        public IHttpActionResult RegistrarAutor(JObject jautor)
        {
            string respuesta;
            if (Conexion.ProbarConexion())
            {
                try
                {
                    // DesSerializa el JSON recibido a un objeto tipo JSONAUTOR
                    JSONAUTOR inAutor= JsonConvert.DeserializeObject<JSONAUTOR>(jautor.ToString());
                    autor outAutor = new autor();

                    // Valida que el JSON estè completo para la inserciòn en la BD
                    ValidadorJSON<JSONAUTOR> validador = new ValidadorJSON<JSONAUTOR>();
                    respuesta = validador.ValidarEstado(inAutor);

                    if (respuesta.Equals("OK"))
                    {
                        // Mapea el DTO a un Entity para ingresar a la BD
                        Mapeador<JSONAUTOR,autor> mapper = new Mapeador<JSONAUTOR, autor>();
                        autor temporal = mapper.Mapear(inAutor, outAutor);
                        if (temporal != null)
                        {
                            ModelAutor MAutor = new ModelAutor();
                            respuesta = MAutor.CrearAutor(temporal);
                            if (respuesta.Equals("OK"))
                            {
                                return Ok(jautor);
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