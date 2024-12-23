using System.Reflection.Metadata;
using HydroLearningProject.Models;

namespace HydroLearningProject.ApplicationDbContext
{
    public class DBContext
    {
        public List<Product> Products { get; set; }
        public List<Customer> Customers { get; set; }
        public List<Invoice> Invoices { get; set; }
        public DBContext() 
        {            
            Products = CreateProducts();
            Customers = CreateCustomers();
            Invoices = CreateInvoices();
        }

        private List<Invoice> CreateInvoices()
        {
            var invoices = new List<Invoice>();
            invoices.Add(new Invoice()
            {
                CustomerId = Customers.First().Id,
                IssueDate = DateTime.Now,
                PaymentTerms = 30,
                DueDate = DateTime.Now,
                Remarks = "Test Invoice",
                Lines = new List<InvoiceLineModel>()
                {
                    new InvoiceLineModel() {
                        IdProduct = Products.First().Id,
                        Quantity = 1,
                        ValueNet = Products.First().Price,
                        Tax = Products.First().Tax,
                        ValueGross =  Products.First().Price + ( Products.First().Price * Products.First().Tax / 100)
                    }
                },
                ValueGross = 0,
                ValueNet = 0,
                ValueTax = 0

            });
            invoices.Add(new Invoice()
            {
                CustomerId = Customers.Last().Id,
                IssueDate = DateTime.Now,
                PaymentTerms = 30,
                DueDate = DateTime.Now,
                Remarks = "Test Invoice2",
                Lines = new List<InvoiceLineModel>()
                {
                    new InvoiceLineModel() {
                        IdProduct = Products.Last().Id,
                        Quantity = 1,
                        ValueNet = Products.Last().Price,
                        Tax = Products.Last().Tax,
                        ValueGross =  Products.Last().Price + ( Products.Last().Price * Products.Last().Tax / 100)
                    },new InvoiceLineModel() {
                        IdProduct = Products.First().Id,
                        Quantity = 1,
                        ValueNet = Products.First().Price,
                        Tax = Products.First().Tax,
                        ValueGross =  Products.First().Price + ( Products.First().Price * Products.First().Tax / 100)
                    }

                },
                ValueGross = 0,
                ValueNet = 0,
                ValueTax = 0

            });
            foreach (var invoice in invoices)
            {
                invoice.ValueGross = invoice.Lines.Select(x => x.ValueGross).Sum();
                invoice.ValueNet = invoice.Lines.Select(x => x.ValueNet).Sum();
                invoice.ValueTax = invoice.Lines.Select(x => x.Tax).Sum();
            }
            return invoices;
        }

        public List<Product> CreateProducts()
        {
            var products = new List<Product>();
            for (int i = 1; i <= 50; i++)
            {
                products.Add(new Product
                {
                    Name = $"Product{i}",
                    Code = $"Code{i:D4}",
                    Price = Math.Round((decimal)(new Random().NextDouble() * 1000), 2), // Random price between 0 and 1000
                    Tax = new Random().Next(0, 101) // Random tax between 0 and 100
                });
            };
            return products;
        }

        public List<Customer> CreateCustomers()
        {
            var customers = new List<Customer>();
            for (int i = 1; i <= 5; i++)
            {
                customers.Add(new Customer
                {
                    Name = $"Customer{i}",
                    Country = DateTime.Now.Microsecond % 2 == 0 ? "Germany" : "Ukraine",
                    City = $"City{i:D2}",
                    Address = $"Street{i:D2}",                    
                });
            };
            return customers;
        }



    }
}
