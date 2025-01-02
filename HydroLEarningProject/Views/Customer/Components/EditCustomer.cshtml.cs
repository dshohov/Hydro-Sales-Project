using System.ComponentModel.DataAnnotations;
using Hydro;
using HydroLearningProject.ISerrvice;
using Microsoft.AspNetCore.Mvc;


namespace HydroLearningProject.Views.Customer.Components
{
    /// <summary>
    /// Component for changing customer
    /// </summary>
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

        /// <summary>
        /// Method for filling fields on first page load
        /// </summary>
        public override void Mount()
        {
            var customer = _customerService.GetCustomer(IdCustomer);
            Name = customer.Name;
            Address = customer.Address;
            City = customer.City;
            Country = customer.Country;            
        }

        /// <summary>
        /// Method for saving changes that have been applied to a customer
        /// </summary>
        public void Save()
        {
            var customer = _customerService.GetCustomer(IdCustomer);
            customer.Name = Name;
            customer.Address = Address;
            customer.City = City;
            customer.Country = Country;
            Reset();
        }

        /// <summary>
        /// Method for Redirect to the Customers Home Page
        /// </summary>
        /// </summary>
        public void Reset() =>
            Location(Url.Action("Index", "Customer"));
    }
}
