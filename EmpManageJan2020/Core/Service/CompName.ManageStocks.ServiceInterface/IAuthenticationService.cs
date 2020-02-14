namespace CompName.ManageStocks.ServiceInterface
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using CompName.ManageStocks.Domain.Authentication;

    public interface IAuthenticationService
    {
        Task<User> RegisterUserAsync(User user);

        Task<UserLogin> ValidateUserLoginAsync(UserLogin userLogin);

        Task<bool> IsUserNameExistsAsync(string userName);

        Task<bool> IsEmailIdExistsAsync(string emailId);
    }
}