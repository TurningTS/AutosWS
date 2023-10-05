using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using AutosWS.Model;

namespace AutosWS
{
    /// <summary>
    /// Summary description for AutoService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class AutoService : System.Web.Services.WebService
    {
        [WebMethod(Description = "Consultar datos")]
        public List<Automovil> Consulta(string marca)
        {
            using (var conexion = new AutosEntities())
            {
                if (marca == "")
                {
                    var consulta = from c in conexion.Automovil select c;
                    // select * from clientes
                    return consulta.ToList();
                }
                var query = from c in conexion.Automovil where c.Marca == marca select c;
                return query.ToList();
            }
        }
        [WebMethod(Description = "Insertar datos")]
        public List<Automovil> Insertar(string marca, string modelo, string fabricacion, string precio, string combustible)
        {
            using (var conexion = new AutosEntities())
            {
                Automovil nuevo = new Automovil();
                nuevo.Marca = marca;
                nuevo.Modelo = modelo;
                nuevo.Año_fabricacion = fabricacion;
                nuevo.Precio = precio;
                nuevo.Combustible = combustible;
                conexion.Automovil.Add(nuevo);
                conexion.SaveChanges();

                var consulta = from c in conexion.Automovil where c.Modelo == modelo && c.Año_fabricacion == fabricacion select c;
                return consulta.ToList();
            }
        }
        [WebMethod(Description = "Eliminar registros")]
        public List<Automovil> Eliminar(string marca, string modelo, string fabricacion)
        {
            using (var conexion = new AutosEntities())
            {
                var cSelect = from c in conexion.Automovil where c.Marca == marca && c.Modelo == modelo && c.Año_fabricacion == fabricacion select c;
                foreach (var row in cSelect)
                {
                    conexion.Automovil.Remove(row);
                }
                conexion.SaveChanges();

                var consulta = from c in conexion.Automovil select c;
                return consulta.ToList();
            }
        }
        [WebMethod(Description = "Modificar datos")]
        public List<Automovil> Modificar(string marca, string modelo, string fabricacion, string nuevoPrecio)
        {
            using (var conexion = new AutosEntities())
            {
                var consulta = (from c in conexion.Automovil where c.Marca == marca && c.Modelo == modelo && c.Año_fabricacion == fabricacion select c).FirstOrDefault();
                consulta.Precio = nuevoPrecio;
                conexion.SaveChanges();
                // Modificar datos
                var query = from c in conexion.Automovil where c.Marca == marca && c.Modelo == modelo && c.Año_fabricacion == fabricacion select c;
                // mostrar datos
                return query.ToList();
            }
        }
    }
}
