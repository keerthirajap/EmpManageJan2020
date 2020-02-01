﻿namespace EmpManage.RepositoryInterface
{
    using System;
    using System.Threading.Tasks;
    using Autofac.Extras.DynamicProxy;
    using EmpManage.CrossCutting.Logging;
    using EmpManage.Domain;
    using Insight.Database;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:Elements should be documented", Justification = "Reviewed")]
    public interface IAuthenticationRepository
    {
        [Sql("[dbo].[P_RegisterUser1]")]
        Task<long> RegisterUserAsync(User user);
    }
}