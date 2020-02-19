namespace CompName.ManageStocks.ServiceConcrete
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using CompName.ManageStocks.CrossCutting.Configuration;
    using CompName.ManageStocks.RepositoryInterface;
    using CompName.ManageStocks.ServiceInterface;
    using CompName.ManageStocks.Domain.Product;

    public class ProductManagementService : IProductManagementService
    {
        private readonly AppSetting _appSetting;

        private readonly IProductManagementRepository _productManagementRepository;

        public ProductManagementService(
                        AppSetting appSetting,
                        IProductManagementRepository productManagementRepository)
        {
            this._appSetting = appSetting;

            this._productManagementRepository = productManagementRepository;
        }

        public async ValueTask<long> CreateProductAsync(Product createProduct)
        {
            return await this._productManagementRepository.CreateProductAsync(createProduct);
        }
    }
}