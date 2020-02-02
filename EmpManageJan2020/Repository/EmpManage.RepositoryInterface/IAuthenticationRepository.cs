namespace EmpManage.RepositoryInterface
{
    using System;
    using System.Threading.Tasks;
    using Autofac.Extras.DynamicProxy;
    using EmpManage.CrossCutting.Logging;
    using EmpManage.Domain;
    using EmpManage.Domain.Authentication;
    using Insight.Database;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:Elements should be documented", Justification = "Reviewed")]
    public interface IAuthenticationRepository
    {
        [Sql("[dbo].[P_RegisterUser]")]
        Task<User> RegisterUserAsync(User user);

        [Sql("[dbo].[P_GetUserDetailsForLoginValidation]")]
        Task<User> GetUserDetailsForLoginValidation(string userName);

        [Sql("[dbo].[P_SaveUserLoggingDetails]")]
        Task SaveUserLoggingDetails(UserLogin userLogin);
    }
}