using Hydro;
using HydroLearningProject.ISerrvice;

namespace HydroLearningProject.Views.Product.Components
{
    public class ProductsDiagram(IProductService _productService) : HydroComponent
    {
        private List<Models.Product> _products;

        public List<Models.Product> Products => _products ??= _productService.GetProducts();
        public object GetChartDataPriceProduct { get; set; }
        public override void Mount()
        {
            var productData = Products.Select(product => new
            {
                value = product.Price,
                name = product.Name
            }).ToArray();

            GetChartDataPriceProduct = new
            {
                title = new
                {
                    text = "Price Products",
                    left = "center"
                },
                series = new[]
                {
                    new
                    {
                        name = "Access From",
                        type = "pie",
                        radius = new[] { "30%", "90%" },
                        center = new[] { "50%", "55%" },

                        label = new
                        {
                            position = "outside"
                        },
                        itemStyle = new
                        {
                            borderRadius = 10,
                            borderColor = "#fff",
                            borderWidth = 5
                        },
                        data = productData,
                        emphasis = new {
                            itemStyle = new {
                              shadowBlur = 10,
                              shadowOffsetX = 0,
                              shadowColor = "rgba(0, 0, 0, 0.5)"
                            }
                        }
                    }
                },
                tooltip = new
                {
                    trigger = "item",

                },
                legend = new
                {
                    orient = "vertical",
                    left = "left"
                }
            };

        }
    }
}
