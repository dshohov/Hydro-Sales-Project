using Microsoft.AspNetCore.Mvc;

namespace HydroLearningProject.Controllers
{
    /// <summary>
    /// Controller class for working with Customers
    /// </summary>
    public class CustomerController : Controller
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
        /// Page for add new Customer
        /// </summary>
        /// <returns>View</returns>
        public IActionResult Add()
        {
            return View();
        }

        /// <summary>
        /// Page for edit Customer
        /// </summary>
        /// <param name="id">id customer</param>
        /// <returns>View</returns>
        public IActionResult Edit(string id)
        {
            ViewData["Id"] = id;
            return View();
        }

    }
}
