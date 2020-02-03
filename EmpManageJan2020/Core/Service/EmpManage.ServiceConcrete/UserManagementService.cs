namespace EmpManage.ServiceConcrete
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
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
    }
}