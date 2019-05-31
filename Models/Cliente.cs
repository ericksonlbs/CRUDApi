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
}