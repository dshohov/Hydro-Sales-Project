using Microsoft.AspNetCore.Mvc;

namespace HydroLearningProject.Controllers
{
    public class InvoiceController : Controller
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
    }
}
