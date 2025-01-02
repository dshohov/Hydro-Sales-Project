using Hydro;
using HydroLearningProject.ISerrvice;
using Microsoft.AspNetCore.Mvc;

namespace HydroLearningProject.Views.Customer.Components
{
    /// <summary>
    /// Component for managing a list of customers.
    /// </summary>
    public class CustomerList(ICustomerService _customerSerrvice) : HydroComponent
    {
        /// <summary>
        /// Local storage of the customer list.
        /// Initialized on the first request via the <see cref="GetCustomers"/> method.
        /// </summary>
        private List<Models.Customer> _customers;

        /// <summary>
        /// List of customers downloaded from the service <see cref="ICustomerService"/>.
        /// </summary>
        public List<Models.Customer> Customers => _customers ??= _customerSerrvice.GetCustomers();

        /// <summary>
        /// Redirects to the page for adding a new customer.
        /// </summary>
        public void Add() =>
            Location(Url.Page("/Customer/Add"));

        /// <summary>
        /// Redirects to the customer edit page by ID.
        /// </summary>
        /// <param name="id">Customer ID to edit.</param>
        public void Edit(string id) =>
            Location(Url.Page("/Customer/Edit", new { id }));

        /// <summary>
        /// Sorts the list of customers by the specified parameter in ascending order.
        /// </summary>
        /// <param name="parameter">The name of the <see cref="Models.Customer"/> model property to sort by.</param>
        /// <exception cref="ArgumentException">Thrown if the specified property does not exist.</exception>
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

        /// <summary>
        /// Sorts the list of customers by the specified parameter in descending order.
        /// </summary>
        /// <param name="parameter">The name of the <see cref="Models.Customer"/> model property to sort by.</param>
        /// <exception cref="ArgumentException">Thrown if the specified property does not exist.</exception>
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

        /// <summary>
        /// Removes a customer from the list by ID.
        /// </summary>
        /// <param name="customerId">The customer ID to delete.</param>
        public void Remove(string customerId) =>
            _customerSerrvice.RemoveCustomer(customerId);

        /// <summary>
        /// Periodically updates the customer list and performs sorting if previously set.
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
