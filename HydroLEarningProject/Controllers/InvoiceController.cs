using Microsoft.AspNetCore.Mvc;

namespace HydroLearningProject.Controllers
{
    public class InvoiceController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
