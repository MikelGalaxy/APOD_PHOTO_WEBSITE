using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using API_Conection_Layer;
using Microsoft.AspNetCore.Mvc;
using NASA_API_PHOTOSHOTS.Models;

namespace NASA_API_PHOTOSHOTS.Controllers
{
    public class HomeController : Controller
    {
        private ApiMain apiMain;


        public IActionResult Index()
        {
            apiMain = new ApiMain();
            apiMain.UpdateBase();
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

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
