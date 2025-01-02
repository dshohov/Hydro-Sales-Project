using System.ComponentModel.DataAnnotations;
using Hydro;
using Hydro.Utils;
using HydroLearningProject.ISerrvice;
using HydroLearningProject.ISerrvices;
using HydroLearningProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace HydroLearningProject.Views.Invoice.Component
{
    /// <summary>
    /// Component for creating new invoices.
    /// </summary
    public class AddInvoice(IInvoiceService _invoiceService, ICustomerService _customerService, IProductService _productService) : HydroComponent
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
        public List<Models.Product> Products { get; set; }

        /// <summary>
        /// Method of filling some fields when loading a page.
        /// </summary>
        public override void Mount()
        { 
            Lines = new List<InvoiceLineModel>();
            Customers = _customerService.GetCustomers();
            Products = _productService.GetProducts();
            IssueDate = DateTime.Today;
            PaymentTerms = 30;
            DueDate = DateTime.Today.AddDays(PaymentTerms);
        }

        /// <summary>
        /// Method for preparing and creating a new model invoices 
        /// </summary>
        public void Add()
        {
            var invoice = new Models.Invoice()
            {
                CustomerId = CustomerId,
                IssueDate = IssueDate,
                PaymentTerms = PaymentTerms,
                DueDate = DueDate,
                Remarks = Remarks,
                Lines = Lines,
                ValueGross = ValueGross,
                ValueNet = ValueNet,
                ValueTax = ValueTax,
            };
            _invoiceService.AddInvoice(invoice);
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
            Lines.Add(new InvoiceLineModel() { Quantity = 1});
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
