using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRUDApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace CRUDApi.Controllers
{
    [Route("api/[controller]")]
    // [Microsoft.AspNetCore.Cors.EnableCors("MyPolicy")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        public static List<Cliente> Clientes = new List<Cliente>();
        public static int maxIdCliente = 0;
        public static object lockar = new object();
        // GET api/values
        [HttpGet]
    [Microsoft.AspNetCore.Cors.EnableCors("MyPolicy")]
        public ActionResult<IEnumerable<Cliente>> Get()
        {
            return Clientes;
        }

        // GET api/values/5
        [HttpGet("{id}")]
    [Microsoft.AspNetCore.Cors.EnableCors("MyPolicy")]
        public ActionResult<Cliente> Get(int id)
        {
            return Clientes.FirstOrDefault(x => x.Id.Equals(id));
        }

        // POST api/values
        [HttpPost]
    [Microsoft.AspNetCore.Cors.EnableCors("MyPolicy")]
        public void Post([FromBody] Cliente value)
        {
            lock (lockar)
            {
                maxIdCliente++;                
                value.Id = maxIdCliente;
                Clientes.Add(value);
            }
        }

        // PUT api/values/5
        [HttpPut("{id}")]
    [Microsoft.AspNetCore.Cors.EnableCors("MyPolicy")]
        public void Put(int id, [FromBody] Cliente value)
        {
            var cliente = Clientes.FirstOrDefault(x => x.Id == id);

            if (cliente != null)
            {
                cliente.CPF = value.CPF;
                if (value.Endereco != null)
                    cliente.Endereco = value.Endereco;
                cliente.Nome = value.Nome;
            }
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
    [Microsoft.AspNetCore.Cors.EnableCors("MyPolicy")]
        public void Delete(int id)
        {
            Clientes.RemoveAll(x => x.Id == id);
        }
    }
}
