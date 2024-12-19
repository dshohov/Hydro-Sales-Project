using HydroLearningProject.Models;

namespace HydroLearningProject.ISerrvice
{
    public interface IProductSerrvice
    {
        List<Product> GetProducts();
        void AddProduct(Product product);
        void RemoveProduct(string productId);
        Product GetProduct(string productId);
    }
}
