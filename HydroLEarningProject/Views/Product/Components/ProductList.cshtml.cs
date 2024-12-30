using Hydro;
using HydroLearningProject.ISerrvice;
using Microsoft.AspNetCore.Mvc;

namespace HydroLearningProject.Views.Product.Components
{
    public class ProductList(IProductService _productService) : HydroComponent
    {
        private List<Models.Product> _products;

        public List<Models.Product> Products => _products ??= _productService.GetProducts();

        public void Add() =>
            Location(Url.Page("/Product/Add"));
        public void Edit(string id) =>
            Location(Url.Page("/Product/Edit", new { id }));

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
        public void Remove(string productId) =>
            _productService.RemoveProduct(productId);


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
