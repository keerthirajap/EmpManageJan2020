namespace CompName.ManageStocks.ServiceInterface
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using CompName.ManageStocks.Domain.Product;

    public interface IProductManagementService
    {
        ValueTask<long> CreateProductAsync(Product createProduct);
    }
}