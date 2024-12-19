using System.ComponentModel.DataAnnotations;
using Hydro;
using HydroLearningProject.ApplicationDbContext;
using HydroLearningProject.ISerrvice;
using HydroLearningProject.Models;
using HydroLearningProject.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HydroLearningProject.Views.Customer.Components
{
    public class EditCustomer(ICustomerService _customerService) : HydroComponent
    {
        [Required]
        public string IdCustomer { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Country { get; set; }

        public override void Mount()
        {
            var customer = _customerService.GetCustomer(IdCustomer);

            
            Name = customer.Name;
            Address = customer.Address;
            City = customer.City;
            Country = customer.Country;
            
        }
        public void Save()
        {
            var customer = _customerService.GetCustomer(IdCustomer);


            customer.Name = Name;
            customer.Address = Address;
            customer.City = City;
            customer.Country = Country;
            Reset();

        }


        public void Reset()
        {
            Location(Url.Action("Index", "Customer"));
        }
    }
}
