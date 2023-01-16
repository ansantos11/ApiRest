using Microsoft.AspNetCore.Mvc;
using NetCoreYouTube.Models;
using NetCoreYouTube.Recursos;
using Newtonsoft.Json;
using System.Data;

namespace NetCoreYouTube.Controllers
{
    [ApiController]
    [Route("producto")]
    public class Productocontroller : ControllerBase
    {
        [HttpGet]
        [Route("Listar")]
        public dynamic ListarProductos() 
        {
            List<Parametro> parametros = new List<Parametro>
            {
                new Parametro("@Estado","1")
            };
            DataTable tCategoria = DBDatos.Listar("Categoria_Listar", parametros);
            DataTable tProducto = DBDatos.Listar("Producto_Listar");

            string jsoncategoria = JsonConvert.SerializeObject(tCategoria);
            string jsonProducto = JsonConvert.SerializeObject(tProducto);

            return new
            {
                success = true,
                mesagge = "Exito",
                Result = new
                {
                    categoria = JsonConvert.DeserializeObject<List<Categoria>>(jsoncategoria),
                    producto = JsonConvert.DeserializeObject<List<Producto>>(jsonProducto),
                    
                }
            }; 
        }
        [HttpPost]
        [Route("agregar")]
        public dynamic AgregarProducto(Producto producto)
        {
            List<Parametro> parametros = new List<Parametro>
            {
                new Parametro("@IDCategoria",producto.IDCategoria),
                new Parametro("@Nombre",producto.Nombre),
                new Parametro("@Precio",producto.Precio)
            };
            bool exito = DBDatos.Ejecutar("Producto_Agregar", parametros);
            
            return new
            {
                success = exito,
                mesagge = exito ? "exito" : "Error al guardar",
                Result = ""
            };
        }
    }
}
