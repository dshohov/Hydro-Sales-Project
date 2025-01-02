using Hydro.Utils;
using Hydro;
using HydroLearningProject.Models;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using HydroLearningProject.ISerrvices;
using HydroLearningProject.ISerrvice;

namespace HydroLearningProject.Views.Invoice.Component
{
    /// <summary>
    /// Component for changing invoices.
    /// </summary
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

        /// <summary>
        /// Method of filling some fields when loading a page.
        /// </summary>
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

        /// <summary>
        /// Method for saving changes
        /// </summary>
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

        /// <summary>
        /// Method for Redirect to the Invoices Home Page
        /// </summary>
        public void Reset() =>
             Location(Url.Action("Index", "Invoice"));

        /// <summary>
        /// The method adds a new field to add a new product to the Invoice
        /// </summary>
        public void AddLine()
        {
            Lines.Add(new InvoiceLineModel() { Quantity = 1 });
            FocusLastLine = true;
            Summarize();
        }

        /// <summary>
        /// The method removes the product field from the invoice.
        /// </summary>
        /// <param name="index">The index of the field to be deleted</param>
        public void RemoveLine(int index)
        {
            Lines.RemoveAt(index);
            Summarize();
        }

        /// <summary>
        /// Method for updating fields that depend on the field
        /// that was changed and called this method
        /// </summary>
        /// <param name="property">Information about the field that called the method after it was modified</param>
        /// <param name="value">The value that the field has</param>
        public override void Bind(PropertyPath property, object value)
        {
            var linesLast = Lines.Last();
            var product = new Models.Product();
            if (property.Child.Name == "IdProduct")
            {
                product = _productService.GetProduct(value.ToString());
                linesLast.IdProduct = value.ToString();
                linesLast.Tax = product.Tax;
                linesLast.ValueNet = product.Price * linesLast.Quantity;
                linesLast.ValueGross = linesLast.ValueNet + (linesLast.Tax * linesLast.ValueNet / 100);
            }
            if (property.Child.Name == "Quantity")
            {
                var quantity = Convert.ToInt32(value);
                product = _productService.GetProduct(linesLast.IdProduct);
                linesLast.Quantity = quantity;
                linesLast.Tax = product.Tax;
                linesLast.ValueNet = product.Price * quantity;
                linesLast.ValueGross = linesLast.ValueNet + (linesLast.Tax * linesLast.ValueNet / 100);
            }
            Summarize();
        }

        /// <summary>
        /// Method for recalculating Inoice prices
        /// </summary>
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
