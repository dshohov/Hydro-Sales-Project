using Hydro;
using HydroLearningProject.ISerrvice;

namespace HydroLearningProject.Views.Product.Components
{
    /// <summary>
    /// Component for managing a list of products.
    /// </summary>
    public class ProductsDiagram(IProductService _productService) : HydroComponent
    {
        /// <summary>
        /// Local storage of the product list.
        /// Initialized on the first request via the <see cref="GetProducts"/> method.
        /// </summary>
        private List<Models.Product> _products;

        /// <summary>
        /// List of products downloaded from the service <see cref="IProductService"/>.
        /// </summary>
        public List<Models.Product> Products => _products ??= _productService.GetProducts();

        /// <summary>
        /// The object stores settings and data for creating a chart.
        /// </summary>
        public object GetChartDataPriceProduct { get; set; }

        /// <summary>
        /// Method for creating a chart settings and data object when the page loads
        /// </summary>
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
