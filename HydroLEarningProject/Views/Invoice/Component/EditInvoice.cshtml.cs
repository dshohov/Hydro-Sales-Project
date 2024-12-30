using Hydro.Utils;
using Hydro;
using HydroLearningProject.Models;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using HydroLearningProject.ISerrvices;
using HydroLearningProject.ISerrvice;

namespace HydroLearningProject.Views.Invoice.Component
{
    public class EditInvoice(IInvoiceService _invoiceService, IProductService _productService, ICustomerService _customerService) : HydroComponent
    {
        public string InvoiceId { get; set; }
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
        public List<Models.Product> Products { get; set; }
        public override void Mount()
        {
            var invoice = _invoiceService.GetInvoice(InvoiceId);
            CustomerId = invoice.CustomerId;
            IssueDate = invoice.IssueDate;
            PaymentTerms = invoice.PaymentTerms;
            DueDate = invoice.DueDate;
            Remarks = invoice.Remarks;
            Lines = invoice.Lines;
            ValueGross = invoice.ValueGross;
            ValueNet = invoice.ValueNet;
            ValueTax = invoice.ValueTax;
            Customers = _customerService.GetCustomers();
            Products = _productService.GetProducts();
        }
        public void Save()
        {
            var invoice = _invoiceService.GetInvoice(InvoiceId);
            invoice.CustomerId = CustomerId;
            invoice.IssueDate = IssueDate;
            invoice.PaymentTerms = PaymentTerms;
            invoice.DueDate = DueDate;
            invoice.Remarks = Remarks;
            invoice.Lines = Lines;
            invoice.ValueGross = ValueGross;
            invoice.ValueNet = ValueNet;
            invoice.ValueTax = ValueTax;
            Location(Url.Action("Index", "Invoice"));
        }

        public void Reset() =>
             Location(Url.Action("Index", "Invoice"));

        public void AddLine()
        {
            Lines.Add(new InvoiceLineModel() { Quantity = 1 });
            FocusLastLine = true;
            Summarize();
        }

        public void RemoveLine(int index)
        {
            Lines.RemoveAt(index);
            Summarize();
        }
        public override void Bind(PropertyPath property, object value)
        {
            if (property.Name != "CustomerId")
            {
                var linesLast = Lines.Last();
                try
                {
                    var quantity = Convert.ToInt32(value);
                    var product2 = _productService.GetProduct(linesLast.IdProduct);
                    linesLast.Quantity = quantity;
                    linesLast.Tax = product2.Tax;
                    linesLast.ValueNet = product2.Price * quantity;
                    linesLast.ValueGross = linesLast.ValueNet + (linesLast.Tax * linesLast.ValueNet / 100);
                }
                catch
                {
                    var product = _productService.GetProduct(value.ToString());
                    linesLast.IdProduct = value.ToString();
                    linesLast.Tax = product.Tax;
                    linesLast.ValueNet = product.Price * linesLast.Quantity;
                    linesLast.ValueGross = linesLast.ValueNet + (linesLast.Tax * linesLast.ValueNet / 100);
                }
                Summarize();
            }
        }
        private void Summarize()
        {
            foreach (var line in Lines)
            {
                line.ValueGross = line.ValueNet + (line.Tax * line.ValueNet / 100);
            }
            ValueNet = Lines.Sum(t => t.ValueNet);
            ValueGross = Lines.Sum(t => t.ValueGross);
            ValueTax = ValueGross - ValueNet;
        }
    }
}
