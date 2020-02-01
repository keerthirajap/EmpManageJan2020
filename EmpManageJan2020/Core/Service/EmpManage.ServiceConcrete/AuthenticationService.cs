namespace EmpManage.ServiceConcrete
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using Autofac.Extras.DynamicProxy;
    using Effortless.Net.Encryption;
    using EmpManage.CrossCutting.Logging;
    using EmpManage.Domain;
    using EmpManage.RepositoryInterface;
    using EmpManage.ServiceInterface;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:Elements should be documented", Justification = "Reviewed")]
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IAuthenticationRepository _authenticationRepository;

        public AuthenticationService(IAuthenticationRepository authenticationRepository)
        {
            this._authenticationRepository = authenticationRepository;
        }

        public async Task<long> RegisterUserAsync(User user)
        {
            user.PasswordSalt = Strings.CreateSalt(20);
            user.Password = user.Password + user.PasswordSalt;
            user.PasswordHash = Hash.Create(HashType.SHA512, user.Password, string.Empty, false);
            user.Password = null;
            return await this._authenticationRepository.RegisterUserAsync(user);
        }
    }
}