using HydroLearningProject.ApplicationDbContext;
using HydroLearningProject.IRepositories;
using HydroLearningProject.Models;

namespace HydroLearningProject.Repositories
{
    public class InvoiceRepository(DBContext _context) : IInvoiceRepository
    {
        public void AddInvoice(Invoice invoice)
        {
            _context.Invoices.Add(invoice);
        }
        public void RemoveInvoice(string invoiceId)
        {
            _context.Invoices.Remove(GetInvoice(invoiceId));
        }
        public List<Invoice> GetInvoices()
        {
            return _context.Invoices;
        }
        public Invoice GetInvoice(string invoiceId)
        {
            return _context.Invoices.FirstOrDefault(x => x.Id == invoiceId);
        }
    }
}
