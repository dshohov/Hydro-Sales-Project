using Hydro;
using HydroLearningProject.ApplicationDbContext;
using HydroLearningProject.ISerrvice;
using HydroLearningProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HydroLearningProject.Views.Home.Components
{
    public class ProductList(IProductSerrvice _productSerrvice) : HydroComponent
    {
        private List<Product> _products;

        public List<Product> Products => _products ??= _productSerrvice.GetProducts();

        public void Add() =>
            Location(Url.Page("/Home/Add"));
        public void Edit(string id) =>
            Location(Url.Page("/Home/Edit", new { id }));

        public void OrderByAscending(string parameter)
        {
            var propertyInfo = typeof(Product).GetProperty(parameter);
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
            var propertyInfo = typeof(Product).GetProperty(parameter);
            if (propertyInfo == null)
            {
                throw new ArgumentException($"Property '{parameter}' does not exist on type '{nameof(Product)}'");
            }

            _products = Products.OrderByDescending(p => propertyInfo.GetValue(p)).ToList();
            CookieStorage.Set("OrderParametr", "Descending", expiration: TimeSpan.FromDays(1), encryption: true);
            CookieStorage.Set("Parametr", parameter, expiration: TimeSpan.FromDays(1), encryption: true);
        }
        public void Remove(string productId)
        {
            _productSerrvice.RemoveProduct(productId);
        }


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
