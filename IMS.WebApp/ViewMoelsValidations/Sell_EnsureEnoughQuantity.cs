using System.ComponentModel.DataAnnotations;
using IMS.WebApp.ViewModels;

namespace IMS.WebApp.ViewMoelsValidations
{
    public class Sell_EnsureEnoughQuantity : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var sellViewModel = validationContext.ObjectInstance as SellViewModel;
            if (sellViewModel != null)
            {
                if (sellViewModel.product != null)
                {
                    if (sellViewModel.QuantityToSell > sellViewModel.product.Quantity)
                    {
                        return new ValidationResult($"You do not have enough ({sellViewModel.product.ProductName}) to sell, The current quantity is {sellViewModel.product.Quantity}.",
                        new[] {validationContext.MemberName});
                    }
                }
            }
            return ValidationResult.Success;
        }
    }
}
