using HydroLearningProject.Models;

namespace HydroLearningProject.IRepositories
{
    public interface IInvoiceRepository
    {
        List<Invoice> GetInvoices();
        void AddInvoice(Invoice invoice);
        void RemoveInvoice(string invoiceId);
        Invoice GetInvoice(string invoiceId);
    }
}
