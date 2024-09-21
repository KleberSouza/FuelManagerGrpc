using FuelManagerGrpcClient.Models;
using Grpc.Net.ClientFactory;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FuelManagerGrpcClient.Controllers
{
    public class HomeController : Controller
    {
        protected readonly Greeter.GreeterClient _grcClient;

        public HomeController(GrpcClientFactory factory)
        {
            _grcClient = factory.CreateClient<Greeter.GreeterClient>("Greeter");
        }

        [HttpGet]
        public IActionResult Calculator()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Calculator(FuelRequest model)
        {
            FuelReply reply = await _grcClient
                .FuelCalculatorAsync(model);

            ViewData["Pct"] = reply.Pct;
            ViewData["Message"] = reply.Message;

            return View(model);
        }

        public IActionResult Index()
        {
            return View();
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