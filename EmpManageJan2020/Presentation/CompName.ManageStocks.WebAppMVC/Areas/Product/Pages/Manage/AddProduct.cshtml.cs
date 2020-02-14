namespace CompName.ManageStocks.WebAppMVC.Areas.Product.Pages.Manage
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using CompName.ManageStocks.CrossCutting.Configuration;
    using CompName.ManageStocks.ServiceInterface;
    using CompName.ManageStocks.WebAppMVC.Areas.Product.Models.Manage;
    using CompName.ManageStocks.WebAppMVC.Infrastructure.Security;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;

    [ApplicationAuthorize]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1649:File name should match first type name", Justification = "Reviewed")]
    public class AddProductModel : PageModel
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        private readonly AppSetting _appSetting;

        private readonly IProductManagementService _productManagementService;

        public AddProductModel(
                               IMapper mapper,
                               IHttpContextAccessor httpContextAccessor,
                               AppSetting appSetting,
                               IProductManagementService productManagementService)
        {
            this._httpContextAccessor = httpContextAccessor;
            this._mapper = mapper;
            this._appSetting = appSetting;
            this._productManagementService = productManagementService;
        }

        public void OnGet()
        {
        }

        [BindProperty]
        public AddProductViewModel AddProductVM { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!this.ModelState.IsValid)
            {
                return this.Page();
            }

            var product = this._mapper.Map<CompName.ManageStocks.Domain.Product.Product>(this.AddProductVM);

            var productId = await this._productManagementService.CreateProductAsync(product);
            return this.RedirectToPage("/");
        }
    }
}