using HydroLearningProject.Models;

namespace HydroLearningProject.IRepositories
{
    public interface IProductRepository
    {
        List<Product> GetProducts();
        void AddProduct(Product product);
        void RemoveProduct(string productId);
        Product GetProduct(string productId);
    }
}
