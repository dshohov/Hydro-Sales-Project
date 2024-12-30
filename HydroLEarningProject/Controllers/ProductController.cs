using System.Diagnostics;
using HydroLEarningProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace HydroLEarningProject.Controllers
{
    public class ProductController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Add()
        {            
            return View();
        }
        public IActionResult Edit(string id)
        {
            ViewData["Id"] = id;
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
