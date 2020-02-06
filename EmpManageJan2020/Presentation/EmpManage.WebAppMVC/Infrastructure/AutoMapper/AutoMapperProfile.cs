namespace EmpManage.WebAppMVC.Infrastructure.AutoMapper
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Threading.Tasks;
    using EmpManage.Domain;
    using EmpManage.Domain.Admin;
    using EmpManage.Domain.Authentication;
    using EmpManage.WebAppMVC.Areas.Admin.Models;
    using EmpManage.WebAppMVC.Areas.Admin.Models.UserManagement;
    using EmpManage.WebAppMVC.Areas.Authentication.Models;
    using EmpManage.WebAppMVC.Areas.Authentication.Models.Auth;
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

            #endregion Authentication

            #region Admin

            CreateMap<UpdateUserAccountViewModel, User>().ReverseMap();
            CreateMap<ChangeUserAccountPasswordViewModel, User>().ReverseMap();

            CreateMap<UserAccountViewModel, User>().ReverseMap();
            CreateMap<UserLoginViewModel, UserLogin>().ReverseMap();

            CreateMap<UserGenderViewModel, UserGender>().ReverseMap();
            CreateMap<UserTitleViewModel, UserTitle>().ReverseMap();

            #endregion Admin
        }
    }
}