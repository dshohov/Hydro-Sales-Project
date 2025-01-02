using HydroLearningProject.Models;

namespace HydroLearningProject.ISerrvices
{
    /// <summary>
    /// An interface for working with Invoice objects
    /// before passing them to the database or 
    /// before passing them to the RazorPage
    /// </summary>
    public interface IInvoiceService
    {
        /// <summary>
        /// Gets a list of Invoice from the repository class
        /// </summary>
        /// <returns>Invoice List</returns>
        List<Invoice> GetInvoices();

        /// <summary>
        /// Works with the Invoice model and sends it to the repository class
        /// </summary>
        /// <param name="invoice">object of type Invoice</param>
        void AddInvoice(Invoice invoice);

        /// <summary>
        /// Calls a repository method to delete a Invoice using the invoice id
        /// </summary>
        /// <param name="invoiceId">Invoice ID number</param>
        void RemoveInvoice(string invoiceId);

        /// <summary>
        /// Calls a repository method to get a Invoice object using the Invoice id
        /// </summary>
        /// <param name="invoiceId">Invoice ID number</param>
        /// <returns>Object of type Invoice</returns>
        Invoice GetInvoice(string invoiceId);
    }
}
