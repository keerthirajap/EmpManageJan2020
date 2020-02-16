namespace CompName.ManageStocks.WebAppMVC.Infrastructure.AutoMapper
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Threading.Tasks;
    using CompName.ManageStocks.Domain;
    using CompName.ManageStocks.Domain.Admin;
    using CompName.ManageStocks.Domain.Authentication;
    using CompName.ManageStocks.Domain.Product;
    using CompName.ManageStocks.WebAppMVC.Areas.Admin.Models;
    using CompName.ManageStocks.WebAppMVC.Areas.Admin.Models.UserManagement;
    using CompName.ManageStocks.WebAppMVC.Areas.Authentication.Models;
    using CompName.ManageStocks.WebAppMVC.Areas.Authentication.Models.Auth;
    using global::AutoMapper;

    [SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1126:PrefixCallsCorrectly", Justification = "Reviewed.")]
    [SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1101:Prefix local calls with this", Justification = "Reviewed")]
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:Elements should be documented", Justification = "Reviewed")]
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            AllowNullDestinationValues = true;

            #region Authentication

            CreateMap<RegisterUserViewModel, User>().ReverseMap();
            CreateMap<LoginViewModel, UserLogin>().ReverseMap();
            CreateMap<UserRolesViewModel, UserRoles>().ReverseMap();

            #endregion Authentication

            #region Admin

            CreateMap<UpdateUserAccountViewModel, User>().ReverseMap();
            CreateMap<ChangeUserAccountPasswordViewModel, User>().ReverseMap();

            CreateMap<UserAccountViewModel, User>().ReverseMap();
            CreateMap<UserLoginViewModel, UserLogin>().ReverseMap();

            CreateMap<UserGenderViewModel, UserGender>().ReverseMap();
            CreateMap<UserTitleViewModel, UserTitle>().ReverseMap();

            #endregion Admin

            #region Product

            #endregion Product
        }
    }
}