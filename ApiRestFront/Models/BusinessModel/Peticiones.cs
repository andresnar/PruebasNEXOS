using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.Json;
using RestSharp;

namespace ApiRestFront.Models.BusinessModel
{
    public class Peticiones <T,U>
    {
        public U EjecutarPeticionGET(string url, T objetoInn, U objetoOut,out int statusCode, out string rpta)
        {
            IRestResponse respuesta;
            try
            {
                string strJson = JsonSerializer.Serialize(objetoInn);
                RestClient cliente = new RestClient(url);
                RestRequest solicitud = new RestRequest(Method.POST);
                solicitud.AddHeader("content-type", "application/json");
                solicitud.AddParameter("application/json", strJson, ParameterType.RequestBody);
                respuesta = cliente.Execute(solicitud);
                if (respuesta.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    objetoOut = JsonSerializer.Deserialize<U>(respuesta.Content);
                    rpta = "OK";
                }
                else
                    rpta = respuesta.Content;
                statusCode = (int)respuesta.StatusCode;
            }
            catch(Exception ex)
            {
                statusCode = 500;
                rpta = ex.Message.ToString();
            }
            
            return objetoOut;
        }
        public U EjecutarPeticionPOST(string url, T objetoInn, U objetoOut, out int statusCode, out string rpta)
        {
            IRestResponse respuesta;
            try
            {
                string strJson = JsonSerializer.Serialize(objetoInn);
                RestClient cliente = new RestClient(url);
                RestRequest solicitud = new RestRequest(Method.POST);
                solicitud.AddHeader("content-type", "application/json");
                solicitud.AddParameter("application/json", strJson, ParameterType.RequestBody);
                respuesta = cliente.Execute(solicitud);
                if (respuesta.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    objetoOut = JsonSerializer.Deserialize<U>(respuesta.Content);
                    rpta = "OK";
                }
                else
                    rpta = respuesta.Content;
                statusCode = (int)respuesta.StatusCode;
            }
            catch (Exception ex)
            {
                statusCode = 500;
                rpta = ex.Message.ToString();
            }

            return objetoOut;
        }
    }
}