namespace CompName.ManageStocks.ServiceConcrete
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Effortless.Net.Encryption;
    using CompName.ManageStocks.CrossCutting.Configuration;
    using CompName.ManageStocks.Domain.Admin;
    using CompName.ManageStocks.Domain.Authentication;
    using CompName.ManageStocks.RepositoryInterface;
    using CompName.ManageStocks.ServiceInterface;

    public class UserManagementService : IUserManagementService
    {
        #region Private Variables

        private readonly AppSetting _appSetting;

        private readonly IUserManagementRepository _userManagementRepository;

        #endregion Private Variables

        #region Constructor

        public UserManagementService(
                       AppSetting appSetting,
                       IUserManagementRepository userManagementRepository)
        {
            this._appSetting = appSetting;
            this._userManagementRepository = userManagementRepository;
        }

        #endregion Constructor

        #region Public Methods

        public async ValueTask<List<User>> GetAllUserAccountsAsync()
        {
            return await this._userManagementRepository.GetAllUserAccountsAsync();
        }

        public async ValueTask<List<UserGender>> GetAllUserGenderDetailsAsync()
        {
            List<UserGender> userGenders = new List<UserGender>();

            userGenders = await this._userManagementRepository.GetAllUserGenderDetailsAsync();

            return userGenders;
        }

        public async ValueTask<List<UserTitle>> GetAllUserTitleDetailsAsync()
        {
            List<UserTitle> userTitles = new List<UserTitle>();

            userTitles = await this._userManagementRepository.GetAllUserTitleDetailsAsync();

            return userTitles;
        }

        #region Manage User

        public async ValueTask<User> GetUserAccountDetailsAsync(long userId)
        {
            User userDetails = new User();

            userDetails = await this._userManagementRepository.GetUserAccountDetailsAsync(userId);

            return userDetails;
        }

        public async ValueTask<bool> UpdateUserAccountDetailsAsync(User user)
        {
            return await this._userManagementRepository.UpdateUserAccountDetailsAsync(user);
        }

        public async ValueTask<bool> UpdateUserAccountActiveStatusAsync(long userId, bool isActive, long modifiedBy)
        {
            return await this._userManagementRepository.UpdateUserAccountActiveStatusAsync(userId, isActive, modifiedBy);
        }

        public async ValueTask<bool> UpdateUserAccountLockedStatusAsync(long userId, bool isLocked, long modifiedBy)
        {
            return await this._userManagementRepository.UpdateUserAccountLockedStatusAsync(userId, isLocked, modifiedBy);
        }

        public async ValueTask<bool> ChangeUserAccountPasswordAsync(User user)
        {
            user.PasswordSalt = Strings.CreateSalt(this._appSetting.AuthenticationSetting.PasswordSaltLength);
            user.Password = user.Password + user.PasswordSalt;
            user.PasswordHash = Hash.Create(HashType.SHA512, user.Password, string.Empty, false);
            user.Password = null;
            return await this._userManagementRepository.ChangeUserAccountPasswordAsync(user);
        }

        #endregion Manage User

        #region User Login History

        public async ValueTask<List<UserLogin>> GetUserLoginHistoryAsync(long userId)
        {
            return await this._userManagementRepository.GetUserLoginHistoryAsync(userId);
        }

        public async ValueTask<List<UserLogin>> GetUserInCorrectLoginHistoryAsync(long userId)
        {
            return await this._userManagementRepository.GetUserInCorrectLoginHistoryAsync(userId);
        }

        #endregion User Login History

        #region Manage User Roles

        public async ValueTask<List<UserRole>> GetUserRolesAsync(long userId)
        {
            return await this._userManagementRepository.GetUserRolesAsync(userId);
        }

        public async ValueTask<bool> EditUserRolesAsync(List<UserRole> editUserRoles, long modifiedBy)
        {
            return await this._userManagementRepository.EditUserRolesAsync(editUserRoles, modifiedBy);
        }

        #endregion Manage User Roles

        #endregion Public Methods

        #region Private Methods

        #endregion Private Methods
    }
}