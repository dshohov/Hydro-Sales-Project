using System.ComponentModel.DataAnnotations;
using Hydro;
using HydroLearningProject.ISerrvice;
using Microsoft.AspNetCore.Mvc;

namespace HydroLearningProject.Views.Customer.Components
{
    /// <summary>
    /// Component for creating new customers.
    /// </summary>
    public class AddCustomer(ICustomerService _customerService) : HydroComponent
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Country { get; set; }

        /// <summary>
        /// Method for preparing and creating a new model Customers 
        /// </summary>
        public void Add()
        {
            if(!Validate())
                return;
            var customer = new Models.Customer()
            {
                Name = Name,
                Address = Address,
                City = City,
                Country = Country
            };
            _customerService.AddCustomer(customer);
            Redirect(Url.Page("/Customer/Index"));
        }

        /// <summary>
        /// Method for Redirect to the Customers Home Page
        /// </summary>
        public void Reset() => 
            Location(Url.Page("/Customer/Index"));

    }
}
