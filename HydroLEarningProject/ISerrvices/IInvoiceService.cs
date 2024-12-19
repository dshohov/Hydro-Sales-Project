using HydroLearningProject.Models;

namespace HydroLearningProject.ISerrvices
{
    public interface IInvoiceService
    {
        List<Invoice> GetInvoices();
        void AddInvoice(Invoice invoice);
        void RemoveInvoice(string invoiceId);
        Invoice GetInvoice(string invoiceId);
    }
}
