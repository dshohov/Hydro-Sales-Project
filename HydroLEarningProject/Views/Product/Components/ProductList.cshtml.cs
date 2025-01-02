using Hydro;
using HydroLearningProject.ISerrvice;
using Microsoft.AspNetCore.Mvc;

namespace HydroLearningProject.Views.Product.Components
{
    /// <summary>
    /// Component for creating product price chart.
    /// </summary>
    public class ProductList(IProductService _productService) : HydroComponent
    {
        /// <summary>
        /// Local storage of the product list.
        /// Initialized on the first request via the <see cref="GetProducts"/> method.
        /// </summary>
        private List<Models.Product> _products;
        
        /// <summary>
        /// List of products downloaded from the service <see cref="IProductService"/>.
        /// </summary>
        public List<Models.Product> Products => _products ??= _productService.GetProducts();
        
        /// <summary>
        /// Redirects to the page for adding a new product.
        /// </summary>
        public void Add() =>
            Location(Url.Page("/Product/Add"));

        /// <summary>
        /// Redirects to the product edit page by ID.
        /// </summary>
        /// <param name="id">Product ID to edit.</param>
        public void Edit(string id) =>
            Location(Url.Page("/Product/Edit", new { id }));

        /// <summary>
        /// Sorts the list of prodcts by the specified parameter in ascending order.
        /// </summary>
        /// <param name="parameter">The name of the <see cref="Models.Product"/> model property to sort by.</param>
        /// <exception cref="ArgumentException">Thrown if the specified property does not exist.</exception>
        public void OrderByAscending(string parameter)
        {
            var propertyInfo = typeof(Models.Product).GetProperty(parameter);
            if (propertyInfo == null)
            {
                throw new ArgumentException($"Property '{parameter}' does not exist on type '{nameof(Product)}'");
            }
            _products = Products.OrderBy(p => propertyInfo.GetValue(p)).ToList();
            CookieStorage.Set("OrderParametr", "Ascending", expiration: TimeSpan.FromDays(1), encryption: true);
            CookieStorage.Set("Parametr", parameter, expiration: TimeSpan.FromDays(1), encryption: true);

        }

        /// <summary>
        /// Sorts the list of products by the specified parameter in descending order.
        /// </summary>
        /// <param name="parameter">The name of the <see cref="Models.Product"/> model property to sort by.</param>
        /// <exception cref="ArgumentException">Thrown if the specified property does not exist.</exception>
        public void OrderByDescending(string parameter)
        {
            var propertyInfo = typeof(Models.Product).GetProperty(parameter);
            if (propertyInfo == null)
            {
                throw new ArgumentException($"Property '{parameter}' does not exist on type '{nameof(Product)}'");
            }
            _products = Products.OrderByDescending(p => propertyInfo.GetValue(p)).ToList();
            CookieStorage.Set("OrderParametr", "Descending", expiration: TimeSpan.FromDays(1), encryption: true);
            CookieStorage.Set("Parametr", parameter, expiration: TimeSpan.FromDays(1), encryption: true);
        }

        /// <summary>
        /// Removes a product from the list by ID.
        /// </summary>
        /// <param name="productId">The product ID to delete.</param>
        public void Remove(string productId) =>
            _productService.RemoveProduct(productId);

        /// <summary>
        /// Periodically updates the product list and performs sorting if previously set.
        /// </summary>
        [Poll(Interval = 60_000)]
        public async Task Refresh()
        {
            var orderParametr = CookieStorage.Get<string>("OrderParametr", encryption: true);
            var parametr = CookieStorage.Get<string>("Parametr", encryption: true);
            if (orderParametr != null && parametr != null)
            {
                if (orderParametr == "Ascending")
                    OrderByAscending(parametr);
                else
                    OrderByDescending(parametr);
            }
            
        }

    }
}
