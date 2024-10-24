using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using MiSegundaAPI.Model;

namespace MiSegundaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : Controller
    {
        private static List<Producto> productos = new List<Producto>
        {
            new Producto {ID=1,Nombre="Laptop", Precio=1500},
            new Producto {ID=2, Nombre="Mouse", Precio=25 }
        };

        //GET:api/Productos
        [HttpGet]
        public ActionResult<IEnumerable<Producto>> GetProductos()
        {
            return Ok(productos);
        }

        [HttpGet("{id}")]
        public ActionResult<Producto> GetProducto(int id) 
        { 
            var producto = productos.FirstOrDefault(p => p.ID == id);
            if (producto == null)
            { 
                return NotFound();
            }
            return Ok(producto);
        }

        [HttpPost]
        public ActionResult<Producto> CrearProducto(Producto nuevoproducto)
        { 
            nuevoproducto.ID=productos.Max(p => p.ID) + 1;
            productos.Add(nuevoproducto);
            return CreatedAtAction(nameof(GetProducto), new {id=nuevoproducto.ID}, nuevoproducto);
        }

        [HttpPut("{id}")]
        public IActionResult ActualizarProducto(int id, Producto productoAct)
        { 
            var producto = productos.FirstOrDefault(p => p.ID == id);
            if (producto == null)
            {
                return NotFound();
            }

            producto.Nombre = productoAct.Nombre;
            producto.Precio = productoAct.Precio;
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult EliminarProductos(int id)
        { 
            var producto = productos.FirstOrDefault(p => p.ID == id);
            if (producto == null)
            {
                return NotFound();
            }

            productos.Remove(producto);
            return NoContent();
        }
    }
}
