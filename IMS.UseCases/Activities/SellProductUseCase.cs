using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IMS.CoreBusiness;
using IMS.UseCases.Activities.Interfaces;
using IMS.UseCases.PluginInterfaces;

namespace IMS.UseCases.Activities
{
    public class SellProductUseCase : ISellProductUseCase
    {
        private readonly IProductTransactionRepository productTransactionRepository;

        public SellProductUseCase(IProductTransactionRepository ProductTransactionRepository, IProductRepository ProductRepository)
        {
            productTransactionRepository = ProductTransactionRepository;
            this.ProductRepository = ProductRepository;
        }

        public IProductRepository ProductRepository { get; }

        public async Task ExecuteAsync(string salesOrderNumber, Product product, int quantity, double unitPrice, string doneBy)
        {
            await this.productTransactionRepository.SellProductAsync(salesOrderNumber, product, quantity, unitPrice, doneBy);
            product.Quantity -= quantity;
            await this.ProductRepository.UpdateProductAsync(product);
        }
    }
}
