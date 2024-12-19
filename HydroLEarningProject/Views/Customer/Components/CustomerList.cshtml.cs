using Hydro;
using HydroLearningProject.ApplicationDbContext;
using HydroLearningProject.ISerrvice;
using HydroLearningProject.Models;
using HydroLearningProject.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HydroLearningProject.Views.Customer.Components
{
    public class CustomerList(ICustomerService _customerSerrvice) : HydroComponent
    {
        private List<Models.Customer> _customers;

        public List<Models.Customer> Customers => _customers ??= _customerSerrvice.GetCustomers();

        public void Add() =>
            Location(Url.Page("/Customer/Add"));
        public void Edit(string id) =>
            Location(Url.Page("/Customer/Edit", new { id }));

        public void OrderByAscending(string parameter)
        {
            var propertyInfo = typeof(Models.Customer).GetProperty(parameter);
            if (propertyInfo == null)
            {
                throw new ArgumentException($"Property '{parameter}' does not exist on type '{nameof(Customer)}'");
            }

            _customers = Customers.OrderBy(p => propertyInfo.GetValue(p)).ToList();
            CookieStorage.Set("OrderParametr", "Ascending", expiration: TimeSpan.FromDays(1), encryption: true);
            CookieStorage.Set("Parametr", parameter, expiration: TimeSpan.FromDays(1), encryption: true);

        }

        public void OrderByDescending(string parameter)
        {
            var propertyInfo = typeof(Models.Customer).GetProperty(parameter);
            if (propertyInfo == null)
            {
                throw new ArgumentException($"Property '{parameter}' does not exist on type '{nameof(Customer)}'");
            }

            _customers = Customers.OrderByDescending(p => propertyInfo.GetValue(p)).ToList();
            CookieStorage.Set("OrderParametr", "Descending", expiration: TimeSpan.FromDays(1), encryption: true);
            CookieStorage.Set("Parametr", parameter, expiration: TimeSpan.FromDays(1), encryption: true);
        }
        public void Remove(string customerId)
        {
            _customerSerrvice.RemoveCustomer(customerId);
        }


        [Poll(Interval = 6_000)]
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
