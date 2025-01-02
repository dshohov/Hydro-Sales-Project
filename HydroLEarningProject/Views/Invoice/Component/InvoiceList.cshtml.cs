using Hydro;
using HydroLearningProject.ISerrvices;
using Microsoft.AspNetCore.Mvc;

namespace HydroLearningProject.Views.Invoice.Component
{
    /// <summary>
    /// Component for managing a list of invoices.
    /// </summary>
    public class InvoiceList(IInvoiceService _invoiceService) : HydroComponent
    {
        /// <summary>
        /// Local storage of the invoice list.
        /// Initialized on the first request via the <see cref="GetInvoices"/> method.
        /// </summary>
        private List<Models.Invoice> _invoices;
        
        /// <summary>
        /// List of invoices downloaded from the service <see cref="IInvoiceService"/>.
        /// </summary>
        public List<Models.Invoice> Invoices => _invoices??= _invoiceService.GetInvoices();
        
        /// <summary>
        /// Redirects to the page for adding a new invoice.
        /// </summary>
        public void Add() =>
            Location(Url.Page("/Invoice/Add"));

        /// <summary>
        /// Redirects to the invoice edit page by ID.
        /// </summary>
        /// <param name="id">Invoice ID to edit.</param>
        public void Edit(string id) =>
            Location(Url.Page("/Invoice/Edit", new { id }));

        /// <summary>
        /// Sorts the list of invoices by the specified parameter in ascending order.
        /// </summary>
        /// <param name="parameter">The name of the <see cref="Models.Invoice"/> model property to sort by.</param>
        /// <exception cref="ArgumentException">Thrown if the specified property does not exist.</exception>
        public void OrderByAscending(string parameter)
        {
            var propertyInfo = typeof(Models.Invoice).GetProperty(parameter);
            if (propertyInfo == null)
            {
                throw new ArgumentException($"Property '{parameter}' does not exist on type '{nameof(Customer)}'");
            }
            _invoices = Invoices.OrderBy(p => propertyInfo.GetValue(p)).ToList();
            CookieStorage.Set("OrderParametr", "Ascending", expiration: TimeSpan.FromDays(1), encryption: true);
            CookieStorage.Set("Parametr", parameter, expiration: TimeSpan.FromDays(1), encryption: true);
        }

        /// <summary>
        /// Sorts the list of invoices by the specified parameter in descending order.
        /// </summary>
        /// <param name="parameter">The name of the <see cref="Models.Invoice"/> model property to sort by.</param>
        /// <exception cref="ArgumentException">Thrown if the specified property does not exist.</exception>
        public void OrderByDescending(string parameter)
        {
            var propertyInfo = typeof(Models.Invoice).GetProperty(parameter);
            if (propertyInfo == null)
            {
                throw new ArgumentException($"Property '{parameter}' does not exist on type '{nameof(Customer)}'");
            }
            _invoices = Invoices.OrderByDescending(p => propertyInfo.GetValue(p)).ToList();
            CookieStorage.Set("OrderParametr", "Descending", expiration: TimeSpan.FromDays(1), encryption: true);
            CookieStorage.Set("Parametr", parameter, expiration: TimeSpan.FromDays(1), encryption: true);
        }

        /// <summary>
        /// Removes a invoice from the list by ID.
        /// </summary>
        /// <param name="invoiceId">The invoice ID to delete.</param>
        public void Remove(string invoiceId) =>
             _invoiceService.RemoveInvoice(invoiceId);

        /// <summary>
        /// Periodically updates the invoice list and performs sorting if previously set.
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
