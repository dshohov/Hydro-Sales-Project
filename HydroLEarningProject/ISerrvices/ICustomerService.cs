using HydroLearningProject.Models;

namespace HydroLearningProject.ISerrvice
{
    public interface ICustomerService
    {
        List<Customer> GetCustomers();
        void AddCustomer(Customer customer);
        void RemoveCustomer(string customerId);
        Customer GetCustomer(string customerId);
    }
}
