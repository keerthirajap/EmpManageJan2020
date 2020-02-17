﻿namespace CompName.ManageStocks.ServiceConcrete
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
    using CompName.ManageStocks.Domain.Authentication;
    using CompName.ManageStocks.RepositoryInterface;
    using CompName.ManageStocks.ServiceInterface;

    public class AuthenticationService : IAuthenticationService
    {
        private readonly AppSetting _appSetting;

        private readonly IAuthenticationRepository _authenticationRepository;

        public AuthenticationService(
                        AppSetting appSetting,
                        IAuthenticationRepository authenticationRepository)
        {
            this._appSetting = appSetting;

            this._authenticationRepository = authenticationRepository;
        }

        public async Task<User> RegisterUserAsync(User user)
        {
            user.PasswordSalt = Strings.CreateSalt(this._appSetting.AuthenticationSetting.PasswordSaltLength);
            user.Password = user.Password + user.PasswordSalt;
            user.PasswordHash = Hash.Create(HashType.SHA512, user.Password, string.Empty, false);
            user.Password = null;
            return await this._authenticationRepository.RegisterUserAsync(user);
        }

        public async Task<(UserLogin userLogin, List<UserRoles> userRoles)> ValidateUserLoginAsync(UserLogin userLogin)
        {
            string passwordHash = string.Empty;
            bool isInCorrectLogging = false;

            User userDetailsForLoginValidation = new User();
            List<UserRoles> userRoles = new List<UserRoles>();

            var resultSets = await this._authenticationRepository.GetUserDetailsForLoginValidationAsync(userLogin.UserName);
            userDetailsForLoginValidation = resultSets.Set1.FirstOrDefault();
            userRoles = resultSets.Set2.ToList();

            if (userDetailsForLoginValidation != null)
            {
                userLogin.Password = userLogin.Password + userDetailsForLoginValidation.PasswordSalt;
                userLogin.UserName = userDetailsForLoginValidation.UserName;
                userLogin.UserId = userDetailsForLoginValidation.UserId;

                passwordHash = Hash.Create(HashType.SHA512, userLogin.Password, string.Empty, false);

                if (!userDetailsForLoginValidation.IsActive)
                {
                    userLogin.IsUserAccountDisabled = true;
                }
                else if (userDetailsForLoginValidation.IsLocked)
                {
                    isInCorrectLogging = true;
                    userLogin.IsUserAccountLocked = true;
                }
                else if (passwordHash == userDetailsForLoginValidation.PasswordHash)
                {
                    userLogin.IsUserAuthenticated = true;
                }
                else
                {
                    isInCorrectLogging = true;
                }
            }
            else
            {
                userLogin.IsUserAccountNotFound = true;
            }

            await this._authenticationRepository.SaveUserLoggingDetailsAsync(userLogin, isInCorrectLogging);

            return (userLogin, userRoles);
        }

        public async Task<bool> IsUserNameExistsAsync(string userName)
        {
            var userNames = await this._authenticationRepository.GetUserNameForNewUserValidationAsync(userName);

            if (userNames != null && userNames.Any(x => x == userName))
            {
                return true;
            }

            return false;
        }

        public async Task<bool> IsEmailIdExistsAsync(string emailId)
        {
            var emailIds = await this._authenticationRepository.GetEmailIdForNewUserValidationAsync(emailId);

            if (emailIds != null && emailIds.Any(x => x == emailId))
            {
                return true;
            }

            return false;
        }
    }
}