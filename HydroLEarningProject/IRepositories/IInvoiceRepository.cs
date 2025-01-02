using HydroLearningProject.Models;

namespace HydroLearningProject.IRepositories
{
    /// <summary>
    /// Interface for working with Invoice from the database
    /// </summary>
    public interface IInvoiceRepository
    {
        /// <summary>
        /// Method to get all Invoices from the database
        /// </summary>
        /// <returns>Invoices list from database</returns>
        List<Invoice> GetInvoices();

        /// <summary>
        /// Method to add a new Invoice object to the database
        /// </summary>
        /// <param name="invoice">Invoice object ready to be added to the database</param>
        void AddInvoice(Invoice invoice);

        /// <summary>
        /// Method to remove a new Invoice object to the database
        /// </summary>
        /// <param name="invoiceId">Id number of the Invoice that needs to be removed</param>
        void RemoveInvoice(string invoiceId);

        /// <summary>
        /// The method searches for a invoice object in the database using id
        /// </summary>
        /// <param name="invoiceId">Id number of the Invoice that needs to be obtained as an object Invoice </param>
        /// <returns>Invoice object</returns>
        Invoice GetInvoice(string invoiceId);
    }
}
