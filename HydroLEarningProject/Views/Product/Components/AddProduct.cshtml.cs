using System.ComponentModel.DataAnnotations;
using Hydro;
using HydroLearningProject.ISerrvice;
using Microsoft.AspNetCore.Mvc;

namespace HydroLearningProject.Views.Product.Components
{
    /// <summary>
    /// Component for creating new Product.
    /// </summary
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

        /// <summary>
        /// Method for preparing and creating a new model products 
        /// </summary>
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
            Redirect(Url.Action("Index", "Product"));
        }

        /// <summary>
        /// Method for Redirect to the Products Home Page
        /// </summary>
        public void Reset() =>
            Redirect(Url.Action("Index", "Product"));

    }
}
