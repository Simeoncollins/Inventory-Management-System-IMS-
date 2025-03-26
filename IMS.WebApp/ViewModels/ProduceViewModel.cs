using System.ComponentModel.DataAnnotations;
using IMS.CoreBusiness;
using IMS.WebApp.ViewMoelsValidations;

namespace IMS.WebApp.ViewModels
{
    public class ProduceViewModel
    {
        [Required]
        public string ProductionNumber { get; set; } = string.Empty;

        [Range(minimum: 1, maximum: int.MaxValue, ErrorMessage = "You have to select a Product.")]
        public int ProductId { get; set; }

        [Range(minimum: 1, maximum: int.MaxValue, ErrorMessage = "Quantity has to be greater or equal to 1.")]
        [Produce_EnsureEnoughQuantity]
        public int QuantityToProduce { get; set; }

        public Product? product { get; set; } = null;
    }
}
