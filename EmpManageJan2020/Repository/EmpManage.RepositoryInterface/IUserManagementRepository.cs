﻿namespace EmpManage.RepositoryInterface
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Autofac.Extras.DynamicProxy;
    using EmpManage.CrossCutting.Logging;
    using EmpManage.Domain;
    using EmpManage.Domain.Authentication;
    using Insight.Database;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:Elements should be documented", Justification = "Reviewed")]
    public interface IUserManagementRepository
    {
        [Sql("[dbo].[P_GetAllUserAccounts]")]
        Task<List<User>> GetAllUserAccountsAsync();
    }
}