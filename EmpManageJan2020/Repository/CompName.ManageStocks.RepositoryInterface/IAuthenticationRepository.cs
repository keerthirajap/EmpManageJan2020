namespace CompName.ManageStocks.RepositoryInterface
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using CompName.ManageStocks.Domain.Authentication;
    using Insight.Database;

    public interface IAuthenticationRepository
    {
        [Sql("[dbo].[P_RegisterUser]")]
        Task<User> RegisterUserAsync(User user);

        [Sql("[dbo].[P_GetUserDetailsForLoginValidation]")]
        Task<Results<User, UserRole>> GetUserDetailsForLoginValidationAsync(string userName);

        [Sql("[dbo].[P_SaveUserLoggingDetails]")]
        Task SaveUserLoggingDetailsAsync(UserLogin userLogin, bool isInCorrectLogging);

        [Sql("SELECT UserName FROM dbo.[User] WHERE UserName = @userName")]
        Task<List<string>> GetUserNameForNewUserValidationAsync(string userName);

        [Sql("SELECT  EmailId FROM dbo.[User] WHERE EmailId = @emailId")]
        Task<List<string>> GetEmailIdForNewUserValidationAsync(string emailId);
    }
}