using HydroLearningProject.ApplicationDbContext;
using HydroLearningProject.IRepositories;
using HydroLearningProject.ISerrvice;
using HydroLearningProject.Models;

namespace HydroLearningProject.Services
{
    public class ProductService(IProductRepository _productRepository) : IProductSerrvice
    {
        public void AddProduct(Product product)
        {
            _productRepository.AddProduct(product);
        }
        public void RemoveProduct(string productId)
        {
            _productRepository.RemoveProduct(productId);
        }

        public List<Product> GetProducts()
        {
            return _productRepository.GetProducts();
                
        }

        public Product GetProduct(string productId)
        {
            return _productRepository.GetProduct(productId);
        }
    }
}
