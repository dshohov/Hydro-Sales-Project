using System.ComponentModel.DataAnnotations;
using Hydro;
using HydroLearningProject.ISerrvice;
using Microsoft.AspNetCore.Mvc;

namespace HydroLearningProject.Views.Customer.Components
{
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
        public void Reset() => 
            Location(Url.Page("/Customer/Index"));

    }
}
