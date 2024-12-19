using AspNetCoreGeneratedDocument;
using Hydro;
using HydroLearningProject.ISerrvices;
using HydroLearningProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HydroLearningProject.Views.Invoice.Component
{
    public class InvoiceList(IInvoiceService _invoiceService) : HydroComponent
    {
        private List<Models.Invoice> _invoices;
        public List<Models.Invoice> Invoices => _invoices??= _invoiceService.GetInvoices();

        public void Add() =>
            Location(Url.Page("/Invoice/Add"));
        public void Edit(string id) =>
            Location(Url.Page("/Invoice/Edit", new { id }));

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

        public void Remove(string invoiceId)
        {
            _invoiceService.RemoveInvoice(invoiceId);
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
