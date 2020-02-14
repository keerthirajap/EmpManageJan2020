namespace CompName.ManageStocks.ServiceConcrete
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Autofac.Extras.DynamicProxy;
    using Effortless.Net.Encryption;
    using CompName.ManageStocks.CrossCutting.Configuration;
    using CompName.ManageStocks.CrossCutting.Logging;
    using CompName.ManageStocks.Domain;
    using CompName.ManageStocks.Domain.Admin;
    using CompName.ManageStocks.Domain.Authentication;
    using CompName.ManageStocks.RepositoryInterface;
    using CompName.ManageStocks.ServiceInterface;

    public class UserManagementService : IUserManagementService
    {
        private readonly AppSetting _appSetting;

        private readonly IUserManagementRepository _userManagementRepository;

        public UserManagementService(
                        AppSetting appSetting,
                        IUserManagementRepository userManagementRepository)
        {
            this._appSetting = appSetting;

            this._userManagementRepository = userManagementRepository;
        }

        public async Task<List<User>> GetAllUserAccountsAsync()
        {
            return await this._userManagementRepository.GetAllUserAccountsAsync();
        }

        public async Task<(User userDetails, List<UserLogin> userInCorrectAuthLogs, List<UserLogin> userLoggingLogs)> GetUserAccountDetailsAsync(long userId)
        {
            User userDetails = new User();
            List<UserLogin> userInCorrectAuthLogs = new List<UserLogin>();
            List<UserLogin> userLoggingLogs = new List<UserLogin>();

            var resultSets = await this._userManagementRepository.GetUserAccountDetailsAsync(userId);

            userDetails = resultSets.Set1.FirstOrDefault();
            userInCorrectAuthLogs = resultSets.Set2.ToList();
            userLoggingLogs = resultSets.Set3.ToList();

            return (userDetails, userInCorrectAuthLogs, userLoggingLogs);
        }

        public async Task<List<UserGender>> GetAllUserGenderDetailsAsync()
        {
            List<UserGender> userGenders = new List<UserGender>();

            userGenders = await this._userManagementRepository.GetAllUserGenderDetailsAsync();

            return userGenders;
        }

        public async Task<List<UserTitle>> GetAllUserTitleDetailsAsync()
        {
            List<UserTitle> userTitles = new List<UserTitle>();

            userTitles = await this._userManagementRepository.GetAllUserTitleDetailsAsync();

            return userTitles;
        }

        public async Task<bool> UpdateUserAccountDetailsAsync(User user)
        {
            return await this._userManagementRepository.UpdateUserAccountDetailsAsync(user);
        }

        public async Task<bool> UpdateUserAccountActiveStatusAsync(long userId, bool isActive, long modifiedBy)
        {
            return await this._userManagementRepository.UpdateUserAccountActiveStatusAsync(userId, isActive, modifiedBy);
        }

        public async Task<bool> UpdateUserAccountLockedStatusAsync(long userId, bool isLocked, long modifiedBy)
        {
            return await this._userManagementRepository.UpdateUserAccountLockedStatusAsync(userId, isLocked, modifiedBy);
        }

        public async Task<bool> ChangeUserAccountPasswordAsync(User user)
        {
            user.PasswordSalt = Strings.CreateSalt(this._appSetting.AuthenticationSetting.PasswordSaltLength);
            user.Password = user.Password + user.PasswordSalt;
            user.PasswordHash = Hash.Create(HashType.SHA512, user.Password, string.Empty, false);
            user.Password = null;
            return await this._userManagementRepository.ChangeUserAccountPasswordAsync(user);
        }
    }
}