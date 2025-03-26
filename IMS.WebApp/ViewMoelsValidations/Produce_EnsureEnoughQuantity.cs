using System.ComponentModel.DataAnnotations;
using IMS.WebApp.ViewModels;

namespace IMS.WebApp.ViewMoelsValidations
{
    public class Produce_EnsureEnoughQuantity : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var produceViewModel = validationContext.ObjectInstance as ProduceViewModel;
            if (produceViewModel != null)
            {
                if (produceViewModel.product != null && produceViewModel.product.ProductInventories != null)
                {
                    foreach (var pi in produceViewModel.product.ProductInventories)
                    {
                        if (pi.Inventory != null && pi.InventoryQuantity * produceViewModel.QuantityToProduce > pi.Inventory.Quantity)
                        {
                            return new ValidationResult($"The inventory ({pi.Inventory.InventoryName}) is not enought to produce {produceViewModel.QuantityToProduce} products",
                                new[] {validationContext.MemberName});
                        }
                    }
                }
            }
            return ValidationResult.Success;
        }
    }
}
