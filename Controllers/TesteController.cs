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
    [ApiExplorerSettings(IgnoreApi = true)]
    public class TesteController : ControllerBase
    {
        private IConfiguration _config;

        public TesteController(IConfiguration configuration)
        {
            _config = configuration;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Cliente>> Get()
        {
            using (SqlConnection conexao = new SqlConnection(
               _config.GetConnectionString("ExemplosDapper")))
            {

                var a = conexao.Query<Cliente, Endereco, Cliente>(
                "SELECT * from Clientes C LEFT JOIN ENDERECOS E ON E.ID = C.ID",
                (cliente, endereco) =>
                {
                    cliente.Endereco = endereco;
                    return cliente;
                },
                splitOn: "Id"
                ).ToList();
                
                return a;
            }
        }


    }
}
