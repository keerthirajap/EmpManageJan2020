namespace CompName.ManageStocks.WebAppMVC.Areas.Admin.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using CompName.ManageStocks.CrossCutting.Configuration;
    using CompName.ManageStocks.Domain;
    using CompName.ManageStocks.Domain.Admin;
    using CompName.ManageStocks.Domain.Authentication;
    using CompName.ManageStocks.ServiceInterface;
    using CompName.ManageStocks.WebAppMVC.Areas.Admin.Models.UserManagement;
    using CompName.ManageStocks.WebAppMVC.Areas.Authentication.Models.Auth;
    using CompName.ManageStocks.WebAppMVC.Infrastructure.Extensions;
    using CompName.ManageStocks.WebAppMVC.Infrastructure.Security;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json.Linq;

    [AutoValidateAntiforgeryToken]
    [Area("Admin")]
    [ApplicationAuthorize]
    public class UserManagementController : Controller
    {
        #region Private Variables

        private readonly IMapper _mapper;

        private readonly AppSetting _appSetting;

        private readonly IUserManagementService _userManagementService;

        #endregion Private Variables

        #region Constructor

        public UserManagementController(
                          IMapper mapper,
                          AppSetting appSetting,
                          IUserManagementService userManagementService)
        {
            this._mapper = mapper;

            this._appSetting = appSetting;

            this._userManagementService = userManagementService;
        }

        #endregion Constructor

        #region Public Methods

        #region Get All Users

        [HttpGet]
        [Route("[controller]/GetAllUserAccounts")]
        public async ValueTask<IActionResult> GetAllUserAccountsViewAsync()
        {
            return await Task.Run(() => this.View("GetAllUserAccounts"));
        }

        [HttpGet]
        [Route("[controller]/GetAllUserAccountsData")]
        public async ValueTask<IActionResult> GetAllUserAccountsDataAsync()
        {
            List<User> users = new List<User>();
            List<UserAccountViewModel> userAccountsViewModel = new List<UserAccountViewModel>();
            users = await this._userManagementService.GetAllUserAccountsAsync();

            userAccountsViewModel = this._mapper.Map<List<UserAccountViewModel>>(users);

            return this.Json(new { data = userAccountsViewModel });
        }

        #endregion Get All Users

        #region Manage User

        [HttpGet]
        [Route("[controller]/EditUserAccountDetails")]
        public async ValueTask<IActionResult> EditUserAccountDetailsAsync(long userId)
        {
            User user = new User();
            UpdateUserAccountViewModel updateUserAccountViewModel = new UpdateUserAccountViewModel();

            List<UserGender> userGenders = new List<UserGender>();
            List<UserGenderViewModel> userGendersViewModel = new List<UserGenderViewModel>();

            List<UserTitle> userTitles = new List<UserTitle>();
            List<UserTitleViewModel> userTitlesViewModel = new List<UserTitleViewModel>();

            var task1 = this._userManagementService.GetAllUserGenderDetailsAsync();
            var task2 = this._userManagementService.GetAllUserTitleDetailsAsync();
            var task3 = this._userManagementService.GetUserAccountDetailsAsync(userId);

            await Task.WhenAll(task1.AsTask(), task2.AsTask(), task3.AsTask());

            userGenders = await task1;
            userTitles = await task2;
            user = await task3;

            userGendersViewModel = this._mapper.Map<List<UserGenderViewModel>>(userGenders);
            userTitlesViewModel = this._mapper.Map<List<UserTitleViewModel>>(userTitles);

            updateUserAccountViewModel = this._mapper.Map<UpdateUserAccountViewModel>(user);
            updateUserAccountViewModel.UserTitles = userTitlesViewModel;
            updateUserAccountViewModel.UserGenders = userGendersViewModel;

            return await Task.Run(() => this.View("EditUserAccountDetails", updateUserAccountViewModel));
        }

        [HttpPost]
        [Route("[controller]/UpdateUserAccountDetails")]
        public async ValueTask<IActionResult> UpdateUserAccountDetailsAsync(UpdateUserAccountViewModel updateUserAccountViewModel)
        {
            dynamic ajaxReturn = new JObject();
            User user = new User();
            var userAuthenticationModel = WebAppMVCExtensions.GetLoggedInUserDetails(this.User);

            user = this._mapper.Map<User>(updateUserAccountViewModel);
            user.ModifiedBy = userAuthenticationModel.UserId;

            var isUpdateSuccess = await this._userManagementService.UpdateUserAccountDetailsAsync(user);

            if (isUpdateSuccess)
            {
                ajaxReturn.Status = "Success";
                ajaxReturn.Message = "User details saved successfully";
            }
            else
            {
                ajaxReturn.Status = "Error";
                ajaxReturn.Message = "Error occured";
            }

            return this.Json(ajaxReturn);
        }

        [HttpPost]
        [Route("[controller]/UpdateUserAccountActiveStatus")]
        public async ValueTask<IActionResult> UpdateUserAccountActiveStatusAsync(long userId, bool isActive)
        {
            dynamic ajaxReturn = new JObject();

            UserAuthentication userAuthenticationModel = new UserAuthentication();
            userAuthenticationModel = WebAppMVCExtensions.GetLoggedInUserDetails(this.User);

            var isUpdateSuccess = await this._userManagementService.UpdateUserAccountActiveStatusAsync(userId, isActive, userAuthenticationModel.UserId);

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

        [HttpPost]
        [Route("[controller]/UpdateUserAccountLockedStatus")]
        public async ValueTask<IActionResult> UpdateUserAccountLockedStatusAsync(long userId, bool isLocked)
        {
            dynamic ajaxReturn = new JObject();

            UserAuthentication userAuthenticationModel = new UserAuthentication();
            userAuthenticationModel = WebAppMVCExtensions.GetLoggedInUserDetails(this.User);

            var isUpdateSuccess = await this._userManagementService.UpdateUserAccountLockedStatusAsync(userId, isLocked, userAuthenticationModel.UserId);

            if (isUpdateSuccess && isLocked)
            {
                ajaxReturn.Status = "Success";
                ajaxReturn.Message = "User account locked successfully";
            }
            else if (isUpdateSuccess && !isLocked)
            {
                ajaxReturn.Status = "Success";
                ajaxReturn.Message = "User account un-locked successfully";
            }
            else
            {
                ajaxReturn.Status = "Error";
                ajaxReturn.Message = "Error occured";
            }

            return this.Json(ajaxReturn);
        }

        [HttpGet]
        [Route("[controller]/LoadChangePasswordPartialView")]

        public async ValueTask<IActionResult> LoadChangePasswordPartialViewAsync(long userId, string userName)
        {
            ChangeUserAccountPasswordViewModel changeUserAccountPasswordVM = new ChangeUserAccountPasswordViewModel();
            changeUserAccountPasswordVM.UserId = userId;
            changeUserAccountPasswordVM.UserName = userName;

            return await Task.Run(() => this.PartialView("_ChangeUserAccountPassword", changeUserAccountPasswordVM));
        }

        [HttpPost]
        [Route("[controller]/ChangeUserAccountPassword")]
        public async ValueTask<IActionResult> ChangeUserAccountPasswordAsync([FromForm] ChangeUserAccountPasswordViewModel changeUserAccountPasswordVM)
        {
            dynamic ajaxReturn = new JObject();
            User user = new User();
            user = this._mapper.Map<User>(changeUserAccountPasswordVM);

            var isUpdateSuccess = await this._userManagementService.ChangeUserAccountPasswordAsync(user);

            if (isUpdateSuccess)
            {
                ajaxReturn.Status = "Success";
                ajaxReturn.Message = "Password changed successfully";
            }
            else
            {
                ajaxReturn.Status = "Error";
                ajaxReturn.Message = "Error occured";
            }

            return this.Json(ajaxReturn);
        }

        #endregion Manage User

        #region User Login History

        [HttpGet]
        [Route("[controller]/UserLoginHistory")]
        public async ValueTask<IActionResult> GetUserLoginHistoryViewAsync(long userId)
        {
            User user = new User();
            UserAccountViewModel userAccountViewModel = new UserAccountViewModel();

            List<UserLoginViewModel> userLoginsViewModel = new List<UserLoginViewModel>();
            List<UserLogin> userLogins = new List<UserLogin>();

            var task1 = this._userManagementService.GetUserAccountDetailsAsync(userId);
            var task2 = this._userManagementService.GetUserLoginHistoryAsync(userId);

            await Task.WhenAll(task1.AsTask(), task2.AsTask());

            user = await task1;
            userLogins = await task2;

            userAccountViewModel = this._mapper.Map<UserAccountViewModel>(user);
            userLoginsViewModel = this._mapper.Map<List<UserLoginViewModel>>(userLogins);

            return await Task.Run(() => this.View("UserLoginHistory", (userAccountViewModel, userLoginsViewModel)));
        }

        [HttpGet]
        [Route("[controller]/UserInCorrectLoginHistory")]
        public async ValueTask<IActionResult> GetUserInCorrectLoginHistoryViewAsync(long userId)
        {
            User user = new User();
            UserAccountViewModel userAccountViewModel = new UserAccountViewModel();

            List<UserLoginViewModel> userLoginsViewModel = new List<UserLoginViewModel>();
            List<UserLogin> userLogins = new List<UserLogin>();

            var task1 = this._userManagementService.GetUserAccountDetailsAsync(userId);
            var task2 = this._userManagementService.GetUserInCorrectLoginHistoryAsync(userId);

            await Task.WhenAll(task1.AsTask(), task2.AsTask());

            user = await task1;
            userLogins = await task2;

            userAccountViewModel = this._mapper.Map<UserAccountViewModel>(user);
            userLoginsViewModel = this._mapper.Map<List<UserLoginViewModel>>(userLogins);

            return await Task.Run(() => this.View("UserInCorrectLoginHistory", (userAccountViewModel, userLoginsViewModel)));
        }

        #endregion User Login History

        #region Manage User Roles

        [HttpGet]
        [Route("[controller]/EditUserRoles")]
        public async ValueTask<IActionResult> GetEditUserRolesViewAsync(long userId)
        {
            User user = new User();
            UserAccountViewModel userAccountViewModel = new UserAccountViewModel();

            List<UserRoleViewModel> userRolesViewModel = new List<UserRoleViewModel>();
            List<UserRole> userRoles = new List<UserRole>();

            var task1 = this._userManagementService.GetUserAccountDetailsAsync(userId);
            var task2 = this._userManagementService.GetUserRolesAsync(userId);

            await Task.WhenAll(task1.AsTask(), task2.AsTask());

            user = await task1;
            userRoles = await task2;

            userAccountViewModel = this._mapper.Map<UserAccountViewModel>(user);
            userRolesViewModel = this._mapper.Map<List<UserRoleViewModel>>(userRoles);

            return await Task.Run(() => this.View("EditUserRoles", (userAccountViewModel, userRolesViewModel)));
        }

        [HttpPost]
        [Route("[controller]/EditUserRoles")]
        public async ValueTask<IActionResult> EditUserRolesAsync(List<UserRoleViewModel> userRolesViewModel)
        {
            dynamic ajaxReturn = new JObject();

            List<UserRole> userRoles = new List<UserRole>();
            userRoles = this._mapper.Map<List<UserRole>>(userRolesViewModel);

            var userAuthenticationModel = WebAppMVCExtensions.GetLoggedInUserDetails(this.User);

            var isEditUserRoleSuccess = await this._userManagementService.EditUserRolesAsync(userRoles, userAuthenticationModel.UserId);

            ajaxReturn.Status = "Success";
            ajaxReturn.Message = "User role changed sucessfully.";

            return this.Json(ajaxReturn);
        }

        #endregion Manage User Roles

        #endregion Public Methods

        #region Private Methods

        #endregion Private Methods
    }
}