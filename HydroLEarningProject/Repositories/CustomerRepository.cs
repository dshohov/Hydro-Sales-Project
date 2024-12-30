using HydroLearningProject.ApplicationDbContext;
using HydroLearningProject.IRepositories;
using HydroLearningProject.Models;

namespace HydroLearningProject.Repositories
{
    public class CustomerRepository(DBContext _context) : ICustomerRepository
    {
        public void AddCustomer(Customer customer)
        {
            _context.Customers.Add(customer);
        }
        public void RemoveCustomer(string customerId)
        {
            _context.Customers.Remove(GetCustomer(customerId));
        }
        public List<Customer> GetCustomers()
        {
            return _context.Customers;
        }
        public Customer GetCustomer(string customerId)
        {
            return _context.Customers.FirstOrDefault(x => x.Id == customerId);
        }
    }
}
