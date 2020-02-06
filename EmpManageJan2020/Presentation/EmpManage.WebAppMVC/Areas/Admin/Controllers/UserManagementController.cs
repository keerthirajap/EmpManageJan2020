namespace EmpManage.WebAppMVC.Areas.Admin.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using EmpManage.CrossCutting.Configuration;
    using EmpManage.Domain;
    using EmpManage.Domain.Admin;
    using EmpManage.Domain.Authentication;
    using EmpManage.ServiceInterface;
    using EmpManage.WebAppMVC.Areas.Admin.Models;
    using EmpManage.WebAppMVC.Areas.Admin.Models.DTOs;
    using EmpManage.WebAppMVC.Infrastructure.Extensions;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json.Linq;

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

            List<UserGender> userGenders = new List<UserGender>();
            List<UserGenderViewModel> userGendersViewModel = new List<UserGenderViewModel>();

            List<UserTitle> userTitles = new List<UserTitle>();
            List<UserTitleViewModel> userTitlesViewModel = new List<UserTitleViewModel>();

            userGenders = await this._userManagementService.GetAllUserGenderDetailsAsync();
            userGendersViewModel = this._mapper.Map<List<UserGenderViewModel>>(userGenders);

            userTitlesViewModel = this._mapper.Map<List<UserTitleViewModel>>(await this._userManagementService.GetAllUserTitleDetailsAsync());

            var userAccountDetails = await this._userManagementService.GetUserAccountDetailsAsync(userId);
            UserAccountDetailsDTO userAccountDetailsDTO = new UserAccountDetailsDTO();

            userAccountDetailsDTO.UserDetails = this._mapper.Map<SaveUserAccountViewModel>(userAccountDetails.userDetails);
            userAccountDetailsDTO.UserDetails.UserGenders = userGendersViewModel;
            userAccountDetailsDTO.UserDetails.UserTitles = userTitlesViewModel;

            userAccountDetailsDTO.UserInCorrectAuthLogs = this._mapper.Map<List<UserLoginViewModel>>(userAccountDetails.userInCorrectAuthLogs);
            userAccountDetailsDTO.UserLoggingLogs = this._mapper.Map<List<UserLoginViewModel>>(userAccountDetails.userLoggingLogs);

            return await Task.Run(() => this.View(userAccountDetailsDTO));
        }

        [HttpPost]
        [Route("[controller]/SaveUserAccountDetails")]
        public async Task<IActionResult> SaveUserAccountDetailsAsync(SaveUserAccountViewModel saveUserAccountViewModel)
        {
            dynamic ajaxReturn = new JObject();

            UserAuthentication userAuthenticationModel = new UserAuthentication();
            userAuthenticationModel = WebAppMVCExtensions.GetLoggedInUserDetails(this.User);
            return this.Json(ajaxReturn);
        }

        [HttpPost]
        [Route("[controller]/UpdateUserAccountActiveStatus")]
        public async Task<IActionResult> UpdateUserAccountActiveStatus(long userId, bool isActive)
        {
            dynamic ajaxReturn = new JObject();

            UserAuthentication userAuthenticationModel = new UserAuthentication();
            userAuthenticationModel = WebAppMVCExtensions.GetLoggedInUserDetails(this.User);

            var isUpdateSuccess = await this._userManagementService.UpdateUserAccountActiveStatus(userId, isActive, userAuthenticationModel.UserId);

            if (isUpdateSuccess && isActive)
            {
                ajaxReturn.Status = "Success";
                ajaxReturn.Message = "User account enabled successfully";
            }
            else if (isUpdateSuccess && !isActive)
            {
                ajaxReturn.Status = "Success";
                ajaxReturn.Message = "User account disabled successfully";
            }
            else
            {
                ajaxReturn.Status = "Error";
                ajaxReturn.Message = "Error occured";
            }
            return this.Json(ajaxReturn);
        }
    }
}