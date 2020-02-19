namespace CompName.ManageStocks.ServiceInterface
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using CompName.ManageStocks.Domain.Admin;
    using CompName.ManageStocks.Domain.Authentication;

    public interface IUserManagementService
    {
        ValueTask<List<User>> GetAllUserAccountsAsync();

        ValueTask<List<UserGender>> GetAllUserGenderDetailsAsync();

        ValueTask<List<UserTitle>> GetAllUserTitleDetailsAsync();

        #region Manage User

        ValueTask<User> GetUserAccountDetailsAsync(long userId);

        ValueTask<bool> UpdateUserAccountDetailsAsync(User user);

        ValueTask<bool> UpdateUserAccountActiveStatusAsync(long userId, bool isActive, long modifiedBy);

        ValueTask<bool> UpdateUserAccountLockedStatusAsync(long userId, bool isLocked, long modifiedBy);

        ValueTask<bool> ChangeUserAccountPasswordAsync(User user);

        #endregion Manage User

        #region User Login History

        ValueTask<List<UserLogin>> GetUserLoginHistoryAsync(long userId);

        ValueTask<List<UserLogin>> GetUserInCorrectLoginHistoryAsync(long userId);

        #endregion User Login History

        #region Manage User Roles

        ValueTask<List<UserRole>> GetUserRolesAsync(long userId);

        ValueTask<bool> EditUserRolesAsync(List<UserRole> editUserRoles, long modifiedBy);

        #endregion Manage User Roles
    }
}