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

        ValueTask<User> GetUserAccountDetailsAsync(long userId);

        ValueTask<List<UserGender>> GetAllUserGenderDetailsAsync();

        ValueTask<List<UserTitle>> GetAllUserTitleDetailsAsync();

        ValueTask<List<UserLogin>> GetUserLoginHistoryAsync(long userId);

        ValueTask<List<UserLogin>> GetUserInCorrectLoginHistoryAsync(long userId);

        ValueTask<bool> UpdateUserAccountDetailsAsync(User user);

        ValueTask<bool> UpdateUserAccountActiveStatusAsync(long userId, bool isActive, long modifiedBy);

        ValueTask<bool> UpdateUserAccountLockedStatusAsync(long userId, bool isLocked, long modifiedBy);

        ValueTask<bool> ChangeUserAccountPasswordAsync(User user);
    }
}