using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IMS.CoreBusiness;
using IMS.Plugin.EFCoreSqlServer.Migrations;
using IMS.UseCases.PluginInterfaces;
using Microsoft.EntityFrameworkCore;

namespace IMS.Plugin.EFCoreSqlServer
{
    public class InventoryEFCoreRepository:IInventoryRepository
    {
        private readonly IDbContextFactory<IMSContext> contextFactory;

        public InventoryEFCoreRepository(IDbContextFactory<IMSContext> contextFactory)
        {
            this.contextFactory = contextFactory;
        }

        public async Task AddInventoryAsync(Inventory inventory)
        {
            using var db = this.contextFactory.CreateDbContext();
            db.Inventories?.Add(inventory);
            await db.SaveChangesAsync();
        }

        public async Task DeleteInventoryAsync(int inventoryId)
        {
            using var db = this.contextFactory.CreateDbContext();
            var inventroy = db.Inventories?.Find(inventoryId);
            if (inventroy is null) return;

            db.Inventories?.Remove(inventroy);
            await db.SaveChangesAsync();
        }

        public async Task<IEnumerable<Inventory>> GetInventoriesByNameAsync(string name)
        {
            using var db = this.contextFactory.CreateDbContext();
            return await db.Inventories.Where(x => x.InventoryName.ToLower().IndexOf(name.ToLower()) >= 0).ToListAsync();
        }

        public async Task<Inventory> GetInventoryByIdAsync(int invId)
        {
            using var db = this.contextFactory.CreateDbContext();
            var inventory = await db.Inventories.FindAsync(invId);

            if(inventory is not null) return inventory;

            return new Inventory();
        }

        public async Task UpdateInventoryAsync(Inventory inventory)
        {
            using var db = this.contextFactory.CreateDbContext();
            var inv = await db.Inventories.FindAsync(inventory.InventoryId);

            if (inv is not null)
            {
                inv.InventoryName = inventory.InventoryName;
                inv.Price = inventory.Price;
                inv.Quantity = inventory.Quantity;

                await db.SaveChangesAsync();
            }
        }
    }
}
