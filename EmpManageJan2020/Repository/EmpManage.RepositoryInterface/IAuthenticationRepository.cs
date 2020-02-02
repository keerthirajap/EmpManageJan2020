namespace EmpManage.RepositoryInterface
{
    using System;
    using System.Collections.Generic;
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
        Task SaveUserLoggingDetails(UserLogin userLogin, bool isInCorrectLogging);

        [Sql("SELECT UserName FROM dbo.[User] WHERE UserName = @userName")]
        Task<List<string>> GetUserNameForNewUserValidation(string userName);

        [Sql("SELECT  EmailId FROM dbo.[User] WHERE EmailId = @emailId")]
        Task<List<string>> GetEmailIdForNewUserValidation(string emailId);
    }
}