using HydroLearningProject.Models;

namespace HydroLearningProject.ISerrvice
{
    /// <summary>
    /// An interface for working with Customer objects
    /// before passing them to the database or 
    /// before passing them to the RazorPage
    /// </summary>
    public interface ICustomerService
    {
        /// <summary>
        /// Gets a list of Customers from the repository class
        /// </summary>
        /// <returns>Customer List</returns>
        List<Customer> GetCustomers();

        /// <summary>
        /// Works with the Customer model and sends it to the repository class
        /// </summary>
        /// <param name="customer">object of type Customer</param>
        void AddCustomer(Customer customer);

        /// <summary>
        /// Calls a repository method to delete a Customer using the customer id
        /// </summary>
        /// <param name="customerId">Customer ID number</param>
        void RemoveCustomer(string customerId);

        /// <summary>
        /// Calls a repository method to get a Customer object using the Customer id
        /// </summary>
        /// <param name="customerId">Customer ID number</param>
        /// <returns>Object of type Customer</returns>
        Customer GetCustomer(string customerId);
    }
}
