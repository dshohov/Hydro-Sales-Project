using System.Reflection.Metadata;
using HydroLearningProject.Models;

namespace HydroLearningProject.ApplicationDbContext
{
    public class DBContext
    {
        public List<Product> Products { get; set; }
        public List<Customer> Customers { get; set; }
        public DBContext() 
        {            
            Products = CreateProducts();
            Customers = CreateCustomers();
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
                    Name = $"Product{i}",
                    Country = DateTime.Now.Microsecond % 2 == 0 ? "Germany" : "Ukraine",
                    City = $"City{i:D2}",
                    Address = $"Street{i:D2}",                    
                });
            };
            return customers;
        }



    }
}
