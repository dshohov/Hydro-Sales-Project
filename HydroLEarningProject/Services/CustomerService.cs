using HydroLearningProject.IRepositories;
using HydroLearningProject.ISerrvice;
using HydroLearningProject.Models;

namespace HydroLearningProject.Services
{
    public class CustomerService(ICustomerRepository _customerRepository) : ICustomerService
    {
        public void AddCustomer(Customer customer)
        {
            _customerRepository.AddCustomer(customer);
        }
        public void RemoveCustomer(string customerId)
        {
            _customerRepository.RemoveCustomer(customerId);
        }
        public List<Customer> GetCustomers()
        {
            return _customerRepository.GetCustomers();
        }
        public Customer GetCustomer(string customerId)
        {
            return _customerRepository.GetCustomer(customerId);
        }
    }
}
