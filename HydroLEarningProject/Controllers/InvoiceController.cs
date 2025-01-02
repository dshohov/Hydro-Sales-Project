using Microsoft.AspNetCore.Mvc;

namespace HydroLearningProject.Controllers
{
    /// <summary>
    /// Controller class for working with Invoices
    /// </summary>
    public class InvoiceController : Controller
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
        /// Page for add new Invoice
        /// </summary>
        /// <returns>View</returns>
        public IActionResult Add()
        {
            return View();
        }

        /// <summary>
        /// Page for edit Invoice
        /// </summary>
        /// <param name="id">Id Invoice</param>
        /// <returns>View</returns>
        public IActionResult Edit(string id)
        {
            ViewData["Id"] = id;
            return View();
        }
    }
}
