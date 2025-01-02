using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace HydroLEarningProject.Controllers
{
    /// <summary>
    /// Controller class for working with Products
    /// </summary>
    public class ProductController : Controller
    {
        /// <summary>
        /// Main page
        /// </summary>
        /// <returns>View</returns>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Page for add new Product
        /// </summary>
        /// <returns>View</returns>
        public IActionResult Add()
        {            
            return View();
        }

        /// <summary>
        /// Page for edit Product
        /// </summary>
        /// <param name="id">Id Product</param>
        /// <returns>View</returns>
        public IActionResult Edit(string id)
        {
            ViewData["Id"] = id;
            return View();
        }

    }
}
