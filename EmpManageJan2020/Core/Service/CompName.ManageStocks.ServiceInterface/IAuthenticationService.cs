namespace CompName.ManageStocks.ServiceInterface
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using CompName.ManageStocks.Domain.Authentication;

    public interface IAuthenticationService
    {
        ValueTask<User> RegisterUserAsync(User user);

        ValueTask<(UserLogin userLogin, List<UserRoles> userRoles)> ValidateUserLoginAsync(UserLogin userLogin);

        ValueTask<bool> IsUserNameExistsAsync(string userName);

        ValueTask<bool> IsEmailIdExistsAsync(string emailId);
    }
}