using HydroLearningProject.ApplicationDbContext;
using HydroLearningProject.IRepositories;
using HydroLearningProject.Models;

namespace HydroLearningProject.Repositories
{
    public class ProductRepository(DBContext _context) : IProductRepository
    {
        public void AddProduct(Product product)
        {
            _context.Products.Add(product);
        }
        public void RemoveProduct(string productId)
        {
            _context.Products.Remove(GetProduct(productId));
        }

        public List<Product> GetProducts()
        {
            return _context.Products;
        }

        public Product GetProduct(string productId)
        {
            return _context.Products.FirstOrDefault(x => x.Id == productId);
        }
    }
}
