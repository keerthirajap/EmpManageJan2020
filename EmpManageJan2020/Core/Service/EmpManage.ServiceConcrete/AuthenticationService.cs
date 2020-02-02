namespace EmpManage.ServiceConcrete
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using Autofac.Extras.DynamicProxy;
    using Effortless.Net.Encryption;
    using EmpManage.CrossCutting.Configuration;
    using EmpManage.CrossCutting.Logging;
    using EmpManage.Domain;
    using EmpManage.Domain.Authentication;
    using EmpManage.RepositoryInterface;
    using EmpManage.ServiceInterface;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:Elements should be documented", Justification = "Reviewed")]
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

        public async Task<UserLogin> ValidateUserLoginAsync(UserLogin userLogin)
        {
            string passwordHash = string.Empty;
            var userDetailsForLoginValidation = await this._authenticationRepository.GetUserDetailsForLoginValidation(userLogin.UserName);
            userLogin.Password = userLogin.Password + userDetailsForLoginValidation.PasswordSalt;
            passwordHash = Hash.Create(HashType.SHA512, userLogin.Password, string.Empty, false);

            if (passwordHash == userDetailsForLoginValidation.Password)
            {
                userLogin.UserName = userDetailsForLoginValidation.UserName;
                userLogin.UserId = userDetailsForLoginValidation.UserId;
                if (userDetailsForLoginValidation.IsActive)
                {
                    userLogin.IsUserAuthenticated = true;
                }
            }
            else
            {
                userLogin.IsUserAccountNotFound = true;
            }

            return userLogin;
        }
    }
}