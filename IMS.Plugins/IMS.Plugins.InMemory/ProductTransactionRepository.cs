using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IMS.CoreBusiness;
using IMS.UseCases.PluginInterfaces;

namespace IMS.Plugins.InMemory
{
    public class ProductTransactionRepository : IProductTransactionRepository
    {
        private readonly IProductRepository productRepository;
        private readonly IInventoryTransactionRepository inventoryTransactionRepository;
        private readonly IInventoryRepository inventoryRepository;
        private List<ProductTransaction> _productTransactions = new List<ProductTransaction>();

        public ProductTransactionRepository(IProductRepository ProductRepository, IInventoryTransactionRepository InventoryTransactionRepository,
            IInventoryRepository InventoryRepository)
        {
            productRepository = ProductRepository;
            inventoryTransactionRepository = InventoryTransactionRepository;
            inventoryRepository = InventoryRepository;
        }

        public async Task<IEnumerable<ProductTransaction>> GetProductTransactionAsyn(string productName, DateTime? dateFrom, DateTime? dateTo, ProductTransactionType? transactionType)
        {
            var products = (await productRepository.GetProductsByNameAsync(string.Empty)).ToList();

            // select * from inventoryTransaction it left join inventory inv on it.inventoryId = inv.inventoryId. this sql query is equivalent to the linq query below
            var query = from pt in this._productTransactions
                        join prod in products on pt.ProductId equals prod.ProductId
                        where (string.IsNullOrWhiteSpace(productName) || prod.ProductName.ToLower().IndexOf(productName.ToLower()) >= 0)
                        && (!dateFrom.HasValue || pt.TransactionDate >= dateFrom.Value.Date)
                        && (!dateTo.HasValue || pt.TransactionDate <= dateTo.Value.Date)
                        && (!transactionType.HasValue || pt.ActivityType == transactionType)
                        select new ProductTransaction
                        {
                            Product = prod,
                            ProductTransactionId = pt.ProductTransactionId,
                            SONumber = pt.SONumber,
                            ProductionNumber = pt.ProductionNumber,
                            ProductId = pt.ProductId,
                            QuantityBefore = pt.QuantityBefore,
                            ActivityType = pt.ActivityType,
                            QuantityAfter = pt.QuantityAfter,
                            TransactionDate = pt.TransactionDate,
                            DoneBy = pt.DoneBy,
                            UnitPrice = pt.UnitPrice
                        };
            return query;
        }

        public async Task ProduceAsync(string productionNumber, Product product, int quantity, string doneBy)
        {
            var prod = await this.productRepository.GetProductByIdAsync(product.ProductId);

            if (prod != null)
            {
                foreach (var pi in prod.ProductInventories)
                {
                    if (pi.Inventory != null)
                    {
                        //add inventory transaction
                        this.inventoryTransactionRepository.ProduceAsync(productionNumber, pi.Inventory, pi.InventoryQuantity * quantity, doneBy, -1);

                        // decrease the inventories
                        var inv = await this.inventoryRepository.GetInventoryByIdAsync(pi.InventoryId);
                        inv.Quantity -= pi.InventoryQuantity * quantity;
                        await this.inventoryRepository.UpdateInventoryAsync(inv);
                    }
                }
            }

            //add product transaction
            this._productTransactions.Add(new ProductTransaction()
            {
                ProductionNumber = productionNumber,
                ProductId = product.ProductId,
                QuantityBefore = product.Quantity,
                ActivityType = ProductTransactionType.ProduceProduct,
                QuantityAfter = product.Quantity + quantity,
                DoneBy = doneBy
            });
        }

        public Task SellProductAsync(string salesOrderNumber, Product product, int quantity, double unitPrice, string doneBy)
        {
            this._productTransactions.Add(new ProductTransaction
            {
                ActivityType = ProductTransactionType.SellProduct,
                SONumber = salesOrderNumber,
                ProductId = product.ProductId,
                QuantityBefore = product.Quantity,
                QuantityAfter = product.Quantity - quantity,
                TransactionDate = DateTime.Now,
                DoneBy = doneBy,
                UnitPrice = unitPrice
            });

            return Task.CompletedTask;
        }
    }
}
