using System.ComponentModel.DataAnnotations;
using Hydro;
using HydroLearningProject.ApplicationDbContext;
using HydroLearningProject.ISerrvice;
using HydroLearningProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HydroLearningProject.Views.Home.Components
{
    public class EditProduct(IProductSerrvice _productSerrvice) : HydroComponent
    {
        [Required]
        public string IdProduct { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        [MaxLength(50)]
        public string Code { get; set; }
        [Required]
        [Range(0, int.MaxValue)]
        public decimal Price { get; set; }
        [Required]
        [Range(0, 100)]
        public int Tax { get; set; }

        public override void Mount()
        {
            var product =  _productSerrvice.GetProduct(IdProduct);

            
            Name = product.Name;
            Code = product.Code;
            Price = product.Price;
            Tax = product.Tax;
            
        }
        public void Save()
        {
            var product = _productSerrvice.GetProduct(IdProduct);


            product.Name = Name;
            product.Code = Code;
            product.Price = Price;
            product.Tax = Tax;
            Reset();

        }


        public void Reset()
        {
            Location(Url.Page("/Home/Index"));
        }
    }
}
