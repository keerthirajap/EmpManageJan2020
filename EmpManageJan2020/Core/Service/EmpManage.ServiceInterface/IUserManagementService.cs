namespace EmpManage.ServiceInterface
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using Autofac.Extras.DynamicProxy;
    using EmpManage.CrossCutting.Logging;
    using EmpManage.Domain;
    using EmpManage.Domain.Authentication;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:Elements should be documented", Justification = "Reviewed")]
    public interface IUserManagementService
    {
        Task<List<User>> GetAllUserAccountsAsync();

        Task<(User userDetails, List<UserLogin> userInCorrectAuthLogs, List<UserLogin> userLoggingLogs)> GetUserAccountDetailsAsync(long userId);
    }
}