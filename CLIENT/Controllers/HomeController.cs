using CLIENT.Models;
using CLIENT.Models.ViewModels;
using CLIENT.Services;
using CLIENT.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CLIENT.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IServiceTarjeta _serviceTarjeta;

        public HomeController(ILogger<HomeController> logger, IServiceTarjeta serviceTarjeta)
        {
            _logger = logger;
            _serviceTarjeta = serviceTarjeta;
        }

        //Mostraremos las tarjetas de credito
        public async Task<IActionResult> Index()
        {
            List<TarjetaCreditoVM> TarjetasCredito = await _serviceTarjeta.ObtenerTarjetasCredito();
            return View(TarjetasCredito);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}