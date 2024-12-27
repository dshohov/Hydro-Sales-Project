using System.ComponentModel.DataAnnotations;

namespace HydroLearningProject.Models
{
    public class InvoiceLineModel
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string IdProduct { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int Quantity { get; set; }
        public decimal ValueNet { get; set; }
        public decimal ValueGross { get; set; }
        public int Tax { get; set; }

    }
}
