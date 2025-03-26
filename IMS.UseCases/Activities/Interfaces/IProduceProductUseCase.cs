﻿using IMS.CoreBusiness;
using IMS.UseCases.PluginInterfaces;

namespace IMS.UseCases.Activities.Interfaces
{
    public interface IProduceProductUseCase
    {
        IProductRepository ProductRepository { get; }

        Task ExecuteAsync(string productionNumber, Product product, int quantity, string doneBy);
    }
}