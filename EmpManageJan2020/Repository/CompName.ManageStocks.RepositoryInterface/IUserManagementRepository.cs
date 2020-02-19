namespace CompName.ManageStocks.RepositoryInterface
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using CompName.ManageStocks.Domain.Admin;
    using CompName.ManageStocks.Domain.Authentication;
    using Insight.Database;

    public interface IUserManagementRepository
    {
        [Sql("[dbo].[P_GetAllUserAccounts]")]
        Task<List<User>> GetAllUserAccountsAsync();

        [Sql("[dbo].[P_GetUserAccountDetails]")]
        Task<User> GetUserAccountDetailsAsync(long userId);

        [Sql("SELECT [UserGenderId],[UserGenderDesc] FROM [dbo].[UserGender]")]
        Task<List<UserGender>> GetAllUserGenderDetailsAsync();

        [Sql("SELECT [UserTitleId], [UserTitleDesc]  FROM [dbo].[UserTitle]")]
        Task<List<UserTitle>> GetAllUserTitleDetailsAsync();

        [Sql("[dbo].[P_UpdateUserAccountDetails]")]
        Task<bool> UpdateUserAccountDetailsAsync(User user);

        [Sql("[dbo].[P_UpdateUserAccountActiveStatus]")]
        Task<bool> UpdateUserAccountActiveStatusAsync(long userId, bool isActive, long modifiedBy);

        [Sql("[dbo].[P_UpdateUserAccountLockedStatus]")]
        Task<bool> UpdateUserAccountLockedStatusAsync(long userId, bool isLocked, long modifiedBy);

        [Sql("[dbo].[P_UpdateUserAccountPassword]")]
        Task<bool> ChangeUserAccountPasswordAsync(User user);
    }
}