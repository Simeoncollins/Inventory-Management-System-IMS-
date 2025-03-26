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
    public class ProduceProductUseCase : IProduceProductUseCase
    {
        private readonly IProductTransactionRepository productTransactionRepository;

        public ProduceProductUseCase(IProductTransactionRepository ProductTransactionRepository, IProductRepository productRepository)
        {
            productTransactionRepository = ProductTransactionRepository;
            ProductRepository = productRepository;
        }

        public IProductRepository ProductRepository { get; }

        public async Task ExecuteAsync(string productionNumber, Product product, int quantity, string doneBy)
        {
            // add transaction record
            await this.productTransactionRepository.ProduceAsync(productionNumber, product, quantity, doneBy);

            // decrease the qantity of inventory

            // update the quantity of product
            product.Quantity += quantity;
            await this.ProductRepository.UpdateProductAsync(product);
        }
    }
}
