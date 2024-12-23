using Hydro;
using HydroLearningProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HydroLearningProject.Views.Invoice.Component
{
    public class InvoiceLines : HydroComponent
    {

        public List<InvoiceLineModel> LinesProduct { get; set; }
        public bool FocusLastLine { get; set; }
        public IInvoiceActions InvoiceActions { get; set; } 

    }
}
