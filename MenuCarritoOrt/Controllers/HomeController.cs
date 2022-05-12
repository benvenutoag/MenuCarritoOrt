using MenuCarritoOrt.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MenuCarritoOrt.Controllers
{
    public class HomeController : Controller /*test*/
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {

            return View();
            //aca
        }


        //public IActionResult Index(string email)
        //{
        //    if (email == "schargo7@gmail.com")
        //    {
        //        return RedirectToAction("Index", "Usuarios");
        //    }
        //    else
        //    {

                
        //    }


        //}
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
