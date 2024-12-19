﻿using HydroLearningProject.Models;

namespace HydroLearningProject.IRepositories
{
    public interface ICustomerRepository
    {
        List<Customer> GetCustomers();
        void AddCustomer(Customer customer);
        void RemoveCustomer(string customerId);
        Customer GetCustomer(string customerId);
    }
}
