using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IMS.UseCases.PluginInterfaces;
using IMS.UseCases.Products.interfaces;

namespace IMS.UseCases.Products
{
    public class DeleteProductUseCase : IDeleteProductUseCase
    {
        private readonly IProductRepository productRepository;

        public DeleteProductUseCase(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public async Task ExecuteAsync(int productId)
        {
            await productRepository.DeleteProductAsync(productId);
        }
    }
}
