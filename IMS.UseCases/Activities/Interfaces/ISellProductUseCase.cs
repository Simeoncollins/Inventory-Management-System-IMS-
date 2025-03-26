using IMS.CoreBusiness;
using IMS.UseCases.PluginInterfaces;

namespace IMS.UseCases.Activities.Interfaces
{
    public interface ISellProductUseCase
    {
        IProductRepository ProductRepository { get; }

        Task ExecuteAsync(string salesOrderNumber, Product product, int quantity, double unitPrice, string doneBy);
    }
}