using HydroLearningProject.Models;

namespace HydroLearningProject.IRepositories
{
    /// <summary>
    /// Interface for working with Customers from the database
    /// </summary>
    public interface ICustomerRepository
    {
        /// <summary>
        /// Method to get all Customers from the database
        /// </summary>
        /// <returns>Customers list from database</returns>
        List<Customer> GetCustomers();

        /// <summary>
        /// Method to add a new Customer object to the database
        /// </summary>
        /// <param name="customer">Customer object ready to be added to the database</param>
        void AddCustomer(Customer customer);

        /// <summary>
        /// Method to remove a new Customer object to the database
        /// </summary>
        /// <param name="customerId">Id number of the Customer that needs to be removed</param>
        void RemoveCustomer(string customerId);

        /// <summary>
        /// The method searches for a customer object in the database using id
        /// </summary>
        /// <param name="customerId">Id number of the Customer that needs to be obtained as an object Customer </param>
        /// <returns>Customer object</returns>
        Customer GetCustomer(string customerId);
    }
}
