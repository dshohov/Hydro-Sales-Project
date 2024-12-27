using Hydro;
using Hydro.Utils;
using HydroLearningProject.ISerrvice;
using HydroLearningProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HydroLearningProject.Views.Invoice.Component
{
    public class InvoiceLines(IProductSerrvice _productSerrvice) : HydroComponent
    {

        public List<InvoiceLineModel> LinesProduct { get; set; }
        //public InvoiceLineModel Line { get; set; }
        //public List <Product> Products { get; set; }
        public bool FocusLastLine { get; set; }
        public IInvoiceActions InvoiceActions { get; set; }
        public override void Mount()
        {
            
            Console.WriteLine();
        }
        

    }
}
