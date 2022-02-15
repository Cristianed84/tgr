using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PruebaTGR.Models
{
    public class Nodo
    {
        public string id { get; set; }
        public string name { get; set; }
        public string parentId { get; set; }
        public IList<Nodo> children { get; set; }
    }
}