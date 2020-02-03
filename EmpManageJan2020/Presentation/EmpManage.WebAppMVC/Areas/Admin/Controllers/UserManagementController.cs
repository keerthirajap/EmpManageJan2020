namespace EmpManage.WebAppMVC.Areas.Admin.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using EmpManage.CrossCutting.Configuration;
    using EmpManage.Domain;
    using EmpManage.Domain.Authentication;
    using EmpManage.ServiceInterface;
    using EmpManage.WebAppMVC.Areas.Admin.Models;
    using EmpManage.WebAppMVC.Areas.Admin.Models.DTOs;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:Elements should be documented", Justification = "Reviewed")]
    [AutoValidateAntiforgeryToken]
    [Area("Admin")]
    [Authorize]
    public class UserManagementController : Controller
    {
        private readonly IMapper _mapper;

        private readonly AppSetting _appSetting;

        private readonly IUserManagementService _userManagementService;

        public UserManagementController(
                        IMapper mapper,
                        AppSetting appSetting,
                        IUserManagementService userManagementService)
        {
            this._mapper = mapper;

            this._appSetting = appSetting;

            this._userManagementService = userManagementService;
        }

        [HttpGet]
        [Route("[controller]/GetUserAccounts")]
        public async Task<IActionResult> GetUserAccountsAsync()
        {
            return await Task.Run(() => this.View());
        }

        [HttpGet]
        [Route("[controller]/GetAllUserAccounts")]
        public async Task<IActionResult> GetAllUserAccountsAsync()
        {
            List<User> users = new List<User>();
            List<UserAccountViewModel> userAccountsViewModel = new List<UserAccountViewModel>();
            users = await this._userManagementService.GetAllUserAccountsAsync();

            userAccountsViewModel = this._mapper.Map<List<UserAccountViewModel>>(users);

            return this.Json(new { data = userAccountsViewModel });
        }

        [HttpGet]
        [Route("[controller]/GetUserAccountDetails")]
        public async Task<IActionResult> GetUserAccountDetailsAsync(long userId)
        {
            User userDetails = new User();
            List<UserLogin> userInCorrectAuthLogs = new List<UserLogin>();
            List<UserLogin> userLoggingLogs = new List<UserLogin>();

            var userAccountDetails = await this._userManagementService.GetUserAccountDetailsAsync(userId);
            UserAccountDetailsDTO userAccountDetailsDTO = new UserAccountDetailsDTO();

            userAccountDetailsDTO.UserDetails = this._mapper.Map<UserAccountViewModel>(userAccountDetails.userDetails);
            userAccountDetailsDTO.UserInCorrectAuthLogs = this._mapper.Map<List<UserLoginViewModel>>(userAccountDetails.userInCorrectAuthLogs);
            userAccountDetailsDTO.UserLoggingLogs = this._mapper.Map<List<UserLoginViewModel>>(userAccountDetails.userLoggingLogs);

            return await Task.Run(() => this.View());
        }
    }
}