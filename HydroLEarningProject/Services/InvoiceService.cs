using HydroLearningProject.IRepositories;
using HydroLearningProject.ISerrvice;
using HydroLearningProject.ISerrvices;
using HydroLearningProject.Models;
using HydroLearningProject.Repositories;

namespace HydroLearningProject.Services
{
    public class InvoiceService(IInvoiceRepository _invoiceRepository, ICustomerService _customerService) : IInvoiceService
    {
        public void AddInvoice(Invoice invoice)
        {
            _invoiceRepository.AddInvoice(invoice);
        }
        public void RemoveInvoice(string invoiceId)
        {
            _invoiceRepository.RemoveInvoice(invoiceId);
        }

        public List<Invoice> GetInvoices()
        {
            var invoices = _invoiceRepository.GetInvoices();
            foreach (var invoice in invoices) {
                invoice.Customer = _customerService.GetCustomer(invoice.CustomerId);
            }
            return invoices;
        }

        public Invoice GetInvoice(string invoiceId)
        {
            var invoice = _invoiceRepository.GetInvoice(invoiceId);
            invoice.Customer = _customerService.GetCustomer(invoiceId);
            return invoice;
        }
    }
}
