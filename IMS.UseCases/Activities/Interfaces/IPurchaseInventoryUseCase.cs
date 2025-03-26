using IMS.CoreBusiness;
using IMS.UseCases.PluginInterfaces;

namespace IMS.UseCases.Activities.Interfaces
{
    public interface IPurchaseInventoryUseCase
    {
        IInventoryRepository InventoryRepository { get; }

        Task ExecuteAsync(string poNumber, Inventory inventory, int quantity, string doneBy);
    }
}