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
        Task<List<User>> GetAllUserAccountsAsync();

        Task<(User userDetails, List<UserLogin> userInCorrectAuthLogs, List<UserLogin> userLoggingLogs)> GetUserAccountDetailsAsync(long userId);

        Task<List<UserGender>> GetAllUserGenderDetailsAsync();

        Task<List<UserTitle>> GetAllUserTitleDetailsAsync();

        Task<bool> UpdateUserAccountDetailsAsync(User user);

        Task<bool> UpdateUserAccountActiveStatusAsync(long userId, bool isActive, long modifiedBy);

        Task<bool> UpdateUserAccountLockedStatusAsync(long userId, bool isLocked, long modifiedBy);

        Task<bool> ChangeUserAccountPasswordAsync(User user);
    }
}