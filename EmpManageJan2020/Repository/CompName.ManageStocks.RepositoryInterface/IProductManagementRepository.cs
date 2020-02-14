namespace CompName.ManageStocks.RepositoryInterface
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using CompName.ManageStocks.Domain.Product;
    using Insight.Database;

    public interface IProductManagementRepository
    {
        [Sql("[dbo].[P_CreateProduct]")]
        Task<long> CreateProductAsync(Product createProduct);
    }
}