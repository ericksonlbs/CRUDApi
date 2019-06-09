using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRUDApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace CRUDApi.Controllers
{
    [Route("api/[controller]")]
    [Microsoft.AspNetCore.Cors.EnableCors("MyPolicy")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        public static List<Produto> Produtos = new List<Produto>();
        public static int maxIdProduto = 0;
        public static object lockar = new object();
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<Produto>> Get()
        {
            return Produtos;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<Produto> Get(int id)
        {
            return Produtos.FirstOrDefault(x => x.Id.Equals(id));
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] Produto value)
        {
            lock (lockar)
            {
                maxIdProduto++;                
                value.Id = maxIdProduto;
                Produtos.Add(value);
            }
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Produto value)
        {
            var Produto = Produtos.FirstOrDefault(x => x.Id == id);

            if (Produto != null)
            {
                Produto.Nome = value.Nome;
                Produto.Descricao = value.Descricao;
                Produto.Valor = value.Valor;
                Produto.UrlImagem = value.UrlImagem;
            }
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            Produtos.RemoveAll(x => x.Id == id);
        }
    }
}
