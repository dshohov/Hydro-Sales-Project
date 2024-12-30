using System.ComponentModel.DataAnnotations;
using Hydro;
using HydroLearningProject.ISerrvice;
using Microsoft.AspNetCore.Mvc;

namespace HydroLearningProject.Views.Product.Components
{

    public class AddProduct(IProductService _productService) : HydroComponent
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

            var product = new Models.Product()
            {
                Name = Name,
                Code = Code,
                Price = Price,
                Tax = Tax
            };

            _productService.AddProduct(product);
            Location(Url.Action("Index", "Product"));
        }
        public void Reset() =>
            Location(Url.Action("Index", "Product"));

    }
}
