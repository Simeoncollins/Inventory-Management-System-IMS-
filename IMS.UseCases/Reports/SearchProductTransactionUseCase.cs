using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IMS.CoreBusiness;
using IMS.UseCases.PluginInterfaces;
using IMS.UseCases.Reports.interfaces;

namespace IMS.UseCases.Reports
{
    public class SearchProductTransactionUseCase : ISearchProductTransactionUseCase
    {
        private readonly IProductTransactionRepository productTransactionRepository;

        public SearchProductTransactionUseCase(IProductTransactionRepository ProductTransactionRepository)
        {
            productTransactionRepository = ProductTransactionRepository;
        }
        public async Task<IEnumerable<ProductTransaction>> ExecuteAsync(string productName, DateTime? dateFrom, DateTime? dateTo, ProductTransactionType? transactionType)
        {
            if (dateTo.HasValue) dateTo = dateTo.Value.AddDays(1);
            return await this.productTransactionRepository.GetProductTransactionAsyn(productName, dateFrom, dateTo, transactionType);
        }
    }
}
