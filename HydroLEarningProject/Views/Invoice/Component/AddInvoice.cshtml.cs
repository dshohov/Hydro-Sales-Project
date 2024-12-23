using System.ComponentModel.DataAnnotations;
using Hydro;
using HydroLearningProject.IRepositories;
using HydroLearningProject.ISerrvices;
using HydroLearningProject.Models;
using HydroLearningProject.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace HydroLearningProject.Views.Invoice.Component
{
    public class AddInvoice(IInvoiceService _invoiceService, ICustomerRepository _customerRepository, IProductRepository _productRepository) : HydroComponent, IInvoiceActions
    {
        public string Id { get; set; }
        [Required]
        public string CustomerId { get; set; }
        [Required]
        public DateTime IssueDate { get; set; }
        [Required]
        public int PaymentTerms { get; set; }
        [Required]
        public DateTime DueDate { get; set; }
        public string Remarks { get; set; }
        [Required]
        public List<InvoiceLineModel> Lines { get; set; }
        [Required]
        public decimal ValueGross { get; set; }
        [Required]
        public decimal ValueNet { get; set; }
        [Required]
        public decimal ValueTax { get; set; }
        [Transient]
        public bool FocusLastLine { get; set; }
        public List<Models.Customer> Customers { get; set; }
        public List<Product> Products { get; set; } 
        public override void Mount()
        { 
            Lines = new List<InvoiceLineModel>();
            Customers = _customerRepository.GetCustomers();
            Products = _productRepository.GetProducts();
            IssueDate = DateTime.Today;
            PaymentTerms = 30;
            DueDate = DateTime.Today.AddDays(PaymentTerms);
        }
        public void Add()
        {


            Console.WriteLine();
        }

        public void Reset()
        {
            Location(Url.Action("Index", "Invoice"));
        }

        public void AddLine()
        {
            Lines.Add(new InvoiceLineModel() { Quantity = 1});
            FocusLastLine = true;
            Summarize();
        }

        public void RemoveLine(int index)
        {
            Lines.RemoveAt(index);
            Summarize();
        }
        private void Summarize()
        {
            foreach (var line in Lines)
            {
                line.ValueNet = line.ValueNet * line.Quantity;
                line.ValueGross = line.ValueNet + line.Tax;
            }

            ValueNet = Lines.Sum(t => t.ValueNet);
            ValueTax = Lines.Sum(t => t.Tax);
            ValueGross = Lines.Sum(t => t.ValueGross);
        }
    }
}
