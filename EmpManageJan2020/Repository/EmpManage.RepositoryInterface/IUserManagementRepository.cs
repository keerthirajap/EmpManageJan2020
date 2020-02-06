namespace EmpManage.RepositoryInterface
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Autofac.Extras.DynamicProxy;
    using EmpManage.CrossCutting.Logging;
    using EmpManage.Domain;
    using EmpManage.Domain.Admin;
    using EmpManage.Domain.Authentication;
    using Insight.Database;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:Elements should be documented", Justification = "Reviewed")]
    public interface IUserManagementRepository
    {
        [Sql("[dbo].[P_GetAllUserAccounts]")]
        Task<List<User>> GetAllUserAccountsAsync();

        [Sql("[dbo].[P_GetUserAccountDetails]")]

        Task<Results<User, UserLogin, UserLogin>> GetUserAccountDetailsAsync(long userId);

        [Sql("SELECT [UserGenderId],[UserGenderDesc] FROM [dbo].[UserGender]")]
        Task<List<UserGender>> GetAllUserGenderDetailsAsync();

        [Sql("SELECT [UserTitleId], [UserTitleDesc]  FROM [dbo].[UserTitle]")]
        Task<List<UserTitle>> GetAllUserTitleDetailsAsync();

        [Sql("[dbo].[P_UpdateUserAccountActiveStatus]")]
        Task<bool> UpdateUserAccountActiveStatus(long userId, bool isActive, long modifiedBy);
    }
}