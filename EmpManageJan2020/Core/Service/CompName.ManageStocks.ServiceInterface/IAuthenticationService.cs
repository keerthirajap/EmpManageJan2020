namespace CompName.ManageStocks.ServiceInterface
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using CompName.ManageStocks.Domain.Authentication;

    public interface IAuthenticationService
    {
        #region Register User

        ValueTask<User> RegisterUserAsync(User user);

        ValueTask<bool> IsUserNameExistsAsync(string userName);

        ValueTask<bool> IsEmailIdExistsAsync(string emailId);

        #endregion Register User

        #region User Login

        ValueTask<(UserLogin userLogin, List<UserRole> userRoles)> ValidateUserLoginAsync(UserLogin userLogin);

        #endregion User Login
    }
}