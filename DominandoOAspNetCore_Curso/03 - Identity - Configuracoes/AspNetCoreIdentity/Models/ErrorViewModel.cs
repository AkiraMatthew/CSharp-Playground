using System;

namespace AspNetCoreIdentity.Models
{
    public class ErrorViewModel
    {
        public int ErroCode { get; set; }
        public string Titulo { get; set; }
        public string Mensagem { get; set; }
        public string RequestId { get; internal set; }
    }
}