using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using PruebaTGR;
using PruebaTGR.Models;

namespace PruebaTGR
{    
    public class DB
    {
        private string cadena = ConfigurationManager.ConnectionStrings["DB_TGREntities"].ConnectionString;

        EF.DB_TGREntities context = new EF.DB_TGREntities();

        public Models.Nodo GetNodo(string id)
        {
            EF.Nodes db= context.Nodes.FirstOrDefault(x => x.id == id);
            Models.Nodo resp = new Models.Nodo();
            if (db != null)
            {
                resp.id = db.id;
                resp.name = db.name;
                resp.parentId=db.parentid;
                var dblist = context.Nodes.Where(x => x.parentid == resp.id).ToList();
                IList<Models.Nodo> lstchild = new List<Models.Nodo>();
                foreach (var f in dblist)
                {
                    IList<Models.Nodo> subchild = new List<Models.Nodo>();
                    Models.Nodo item = new Models.Nodo();
                    item.id = f.id;
                    item.name = f.name;
                    item.parentId = f.parentid;
                    var dbChild = context.Nodes.Where(x => x.parentid == f.id).ToList();
                    foreach(var f2 in dbChild)
                    {
                        Models.Nodo item2 = new Models.Nodo();
                        item2.id = f2.id;
                        item2.name = f2.name;
                        item2.parentId = f2.parentid;
                        item2.children = new List<Models.Nodo>();
                        subchild.Add(item2);
                    }
                    item.children = subchild;
                    lstchild.Add(item);
                }
                resp.children = lstchild;

                return resp;
            }
            else
            {
                return null;
            }
                       
        }

        public Models.Nodo Post(Nodo nodo)
        {
            EF.Nodes dbVerifica;
            string id = string.Empty;
            byte[] numeroBytes;
            int numero;
            Random rnd = new Random();
            numero = rnd.Next(100);
            numeroBytes  = Encoding.UTF8.GetBytes(numero.ToString());
            id= System.Convert.ToBase64String(numeroBytes);
            dbVerifica = context.Nodes.FirstOrDefault(x => x.id == id);
            while (dbVerifica != null)
            {
                numero = rnd.Next(100);
                numeroBytes = Encoding.UTF8.GetBytes(numero.ToString());
                id = System.Convert.ToBase64String(numeroBytes);
                dbVerifica = context.Nodes.FirstOrDefault(x => x.id == id);                
            }
            nodo.id = id;
            EF.Nodes db = new EF.Nodes();
            db.id = nodo.id;
            db.name = nodo.name;
            db.parentid = nodo.parentId;

            context.Nodes.Add(db);
            context.SaveChanges();
            var db2 = context.Nodes.FirstOrDefault(x => x.id == db.id);
            if (db2 != null)
            {
                return GetNodo(nodo.id);
            }
            else
            {
                return null;
            }
        }

        public  bool DeleteNodo(string id)
        {
            var db = context.Nodes.FirstOrDefault(x => x.id == id);
            context.Nodes.Remove(db);
            context.SaveChanges();

            var db2 = context.Nodes.FirstOrDefault(x => x.id == id);
            if (db2 == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}