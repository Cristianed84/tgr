using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PruebaTGR.Controllers
{   
    [RoutePrefix("nodes")]    
    public class NodesController : ApiController
    {
        DB data = new DB();        

        public IHttpActionResult Get(string id)
        {
            try
            {
                var resp = data.GetNodo(id);                
                if (resp != null)
                {
                    return Ok(resp);                    
                }
                else
                {
                    return Ok("El nodo no fue encontrado");
                }
            }
            catch
            {
                return Ok("Los parametros no son correctos");
            }
        }

        public IHttpActionResult Delete(string id)
        {
            try
            {
                var resp = data.DeleteNodo(id);
                if (resp == true)
                {
                    return Ok("Operacion Exitosa");
                }
                else
                {
                    return Ok("El nodo no fue encontrado");
                }
            }
            catch
            {
                return Ok("Los parametros no son correctos");
            }
        }

        public IHttpActionResult Post([FromBody] Models.Nodo nodo)
        {
            try
            {   
                var resp = data.Post(nodo);
                if (resp!=null)
                {
                    return Ok(resp);
                }
                else
                {
                    return Ok("No fue posible agregar el nodo");
                }
            }
            catch
            {                
                return Ok("Los parametros no son correctos");
            }
        }
    }
}
