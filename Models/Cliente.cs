using System;

namespace CRUDApi.Models
{    
    public class Cliente
    {
        public int Id{get;set;}
        public string Nome{get;set;}
        public string CPF{get;set;}
        public Endereco Endereco{get;set;}
    }

    public class Endereco{
        public string Estado { get; set; }
        public string Cidade { get; set; }
        public string Complemento { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
    }
}