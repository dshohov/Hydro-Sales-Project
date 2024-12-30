using System.ComponentModel.DataAnnotations;
using Hydro;
using Hydro.Utils;
using HydroLearningProject.ISerrvice;
using HydroLearningProject.ISerrvices;
using HydroLearningProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace HydroLearningProject.Views.Invoice.Component
{
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
        public override void Mount()
        { 
            Lines = new List<InvoiceLineModel>();
            Customers = _customerService.GetCustomers();
            Products = _productService.GetProducts();
            IssueDate = DateTime.Today;
            PaymentTerms = 30;
            DueDate = DateTime.Today.AddDays(PaymentTerms);
        }
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

        public void Reset() =>
            Location(Url.Action("Index", "Invoice"));

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
