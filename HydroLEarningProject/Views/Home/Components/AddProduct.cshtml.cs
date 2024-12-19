using System.ComponentModel.DataAnnotations;
using Hydro;
using HydroLearningProject.ApplicationDbContext;
using HydroLearningProject.ISerrvice;
using HydroLearningProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HydroLearningProject.Views.Home.Components
{

    public class AddProduct(IProductSerrvice _productSerrvice) : HydroComponent
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        [MaxLength(50)]
        public string Code { get; set; }
        [Required]
        [Range(0, int.MaxValue)]
        public decimal Price { get; set; } = 10;
        [Required]
        [Range(0, 100)]
        public int Tax { get; set; } = 5;


        public void Add()
        {
            if(!Validate())
                return;

            var product = new Product()
            {
                Name = Name,
                Code = Code,
                Price = Price,
                Tax = Tax
            };

            _productSerrvice.AddProduct(product);
            Location(Url.Action("Index", "Customer"));
        }
        public void Reset()
        {
            Location(Url.Action("Index", "Customer"));
        }
    }
}
