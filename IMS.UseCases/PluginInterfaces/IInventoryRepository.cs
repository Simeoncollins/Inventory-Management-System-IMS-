using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IMS.CoreBusiness;

namespace IMS.UseCases.PluginInterfaces
{
    public interface IInventoryRepository
    {
        Task AddInventoryAsync(Inventory inventory);
        Task UpdateInventoryAsync(Inventory inventory);
        Task<IEnumerable<Inventory>> GetInventoriesByNameAsync(string name);
        Task<Inventory> GetInventoryByIdAsync(int invId);
        Task DeleteInventoryAsync(int inventoryId);
    }
}
