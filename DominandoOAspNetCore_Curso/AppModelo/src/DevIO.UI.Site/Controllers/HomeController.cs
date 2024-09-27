using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevIO.UI.Site.Data;
using Microsoft.AspNetCore.Mvc;

namespace DevIO.UI.Site.Controllers
{
    ///TODO DI na Controller
    public class HomeController : Controller
    {
        private readonly IPedidoRepository _pedidoRepository;

        public HomeController(IPedidoRepository pedidoRepository)
        {
            _pedidoRepository = pedidoRepository;
        }

        public IActionResult Index()
        {
            var pedido = _pedidoRepository.ObterPedido();

            return View();
        }
    }

    ///Caso seja necessario fazer a DI num projeto legado e
    ///nao possa mexer no construtor por algum motivo:
    //public class HomeController : Controller
    //{
    //    ///Caso seja necessario fazer a DI num projeto legado:
    //    public IActionResult Index([FromServices] IPedidoRepository pedidoRepository)
    //    {
    //        var pedido = _pedidoRepository.ObterPedido();

    //        return View();
    //    }
    //}
}
