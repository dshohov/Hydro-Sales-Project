using HydroLearningProject.Models;

namespace HydroLearningProject.IRepositories
{
    /// <summary>
    /// Interface for working with Product from the database
    /// </summary>
    public interface IProductRepository
    {
        /// <summary>
        /// Method to get all Products from the database
        /// </summary>
        /// <returns>Products list from database</returns>
        List<Product> GetProducts();

        /// <summary>
        /// Method to add a new Product object to the database
        /// </summary>
        /// <param name="product">Product object ready to be added to the database</param>
        void AddProduct(Product product);

        /// <summary>
        /// Method to remove a new Product object to the database
        /// </summary>
        /// <param name="productId">Id number of the Product that needs to be removed</param>
        void RemoveProduct(string productId);

        /// <summary>
        /// The method searches for a product object in the database using id
        /// </summary>
        /// <param name="productId">Id number of the Product that needs to be obtained as an object Product </param>
        /// <returns>Product object</returns>
        Product GetProduct(string productId);
    }
}
