using HydroLearningProject.Models;

namespace HydroLearningProject.ISerrvice
{
    /// <summary>
    /// An interface for working with Product objects
    /// before passing them to the database or 
    /// before passing them to the RazorPage
    /// </summary>
    public interface IProductService
    {
        /// <summary>
        /// Gets a list of Products from the repository class
        /// </summary>
        /// <returns>Product List</returns>
        List<Product> GetProducts();

        /// <summary>
        /// Works with the Product model and sends it to the repository class
        /// </summary>
        /// <param name="product">object of type Product</param>
        void AddProduct(Product product);

        /// <summary>
        /// Calls a repository method to delete a Product using the product id
        /// </summary>
        /// <param name="productId">Product ID number</param>
        void RemoveProduct(string productId);

        /// <summary>
        /// Calls a repository method to get a Product object using the Product id
        /// </summary>
        /// <param name="productId">Product ID number</param>
        /// <returns>Object of type Product</returns>
        Product GetProduct(string productId);
    }
}
