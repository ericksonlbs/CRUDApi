using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using CRUDApi.Models;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace CRUDApi.Controllers
{
    [Route("api/[controller]")]
    [Microsoft.AspNetCore.Cors.EnableCors("AllowMyOrigin")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private IConfiguration _config;

        public ClientesController(IConfiguration configuration)
        {
            _config = configuration;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Cliente>> Get()
        {
            using (SqlConnection conexao = new SqlConnection(
               _config.GetConnectionString("SqlAzure")))
            {

                var a = conexao.Query<Cliente>("SELECT * from Clientes C").ToList();
                return a;
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Cliente> Get(int id)
        {
            using (SqlConnection conexao = new SqlConnection(
               _config.GetConnectionString("SqlAzure")))
            {

                var a = conexao.QueryFirstOrDefault<Cliente>("SELECT * from Clientes C WHERE ID = @Id", new { @Id = id });
                return a;
            }
        }

        [HttpPost]
        public ActionResult<Cliente> Post(Cliente cliente)
        {
            using (SqlConnection conexao = new SqlConnection(
               _config.GetConnectionString("SqlAzure")))
            {
                var clienteInserido = conexao.QueryFirstOrDefault<Cliente>("INSERT INTO CLIENTES (nome, cpf) VALUES (@NOME, @CPF); SELECT * from Clientes C WHERE ID = @@IDENTITY", cliente);
                return clienteInserido;
            }
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            using (SqlConnection conexao = new SqlConnection(
               _config.GetConnectionString("SqlAzure")))
            {
                conexao.Execute("DELETE Clientes WHERE ID = @Id", new { @Id = id });
            }
        }

        [HttpPut("{id}")]
        public void Put(int id, Cliente cliente)
        {
            using (SqlConnection conexao = new SqlConnection(
               _config.GetConnectionString("SqlAzure")))
            {
                cliente.Id = id;
                conexao.Execute("UPDATE CLIENTES SET NOME = @Nome, CPF = @CPF WHERE ID = @Id", cliente);
            }
        }
    }
}
