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

        IList<Models.Nodo> lista()
        {
            IList<Models.Nodo> lst = new List<Models.Nodo>();
            Models.Nodo item = new Models.Nodo();
            item.id = "id1";
            item.parentId = "root";
            item.name = "carpeta";
            IList<Models.Nodo> child = new List<Models.Nodo>();
            Models.Nodo c1a = new Models.Nodo();
            c1a.id = "id1a";
            c1a.parentId = "id1";
            IList<Models.Nodo> childc1a = new List<Models.Nodo>();
            c1a.children = childc1a;
            child.Add(c1a);
            item.children = child;
            lst.Add(item);
            return lst;
        }

        public IHttpActionResult Get(string id)
        {
            try
            {
                var resp = lista().FirstOrDefault(x => x.id == id);
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

        // GET api/values/5
        //public Models.Nodo Get(string id)
        //{
        //    var resp = lista().FirstOrDefault(x => x.id == id);
        //    return resp;
        //}                       
        
    }
}
