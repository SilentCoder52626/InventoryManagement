using Inventory.ViewModels.User;
using InventoryLibrary.Source.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NToastNotify;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Inventory.Controllers
{
    [Authorize(Roles = ApplicationUser.RoleAdmin)]
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IToastNotification _notify;
        public UserController(ILogger<UserController> logger,
            UserManager<ApplicationUser> userManager,
            IToastNotification notify)
        {
            _logger = logger;
            _userManager = userManager;
            _notify = notify;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.ToListAsync();

            var userIndexViewModel = new List<UserIndexViewModel>();
            foreach (var user in users)
            {

                var userModel = new UserIndexViewModel
                {
                    Id = user.Id,
                    Name = user.NormalizedUserName,
                    Email = user.Email,
                    Username = user.UserName,
                    Status = await _userManager.IsInRoleAsync(user, ApplicationUser.RoleAdmin) ? ApplicationUser.RoleAdmin : ApplicationUser.RoleUser
                };
                userIndexViewModel.Add(userModel);
            }
            return View(userIndexViewModel);


        }

        public IActionResult ChangePassword(string userId)
        {
            var changePasswordViewModel = new ChangePasswordViewModel()
            {
                UserId = userId
            };

            return View(changePasswordViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(model.UserId) ?? throw new Exception("User not found");
                var isPasswordChanged = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
                if (isPasswordChanged.Succeeded)
                {
                    _notify.AddSuccessToastMessage("Password Changed Successfully");
                    return RedirectToAction(nameof(Index));
                }
                foreach (var error in isPasswordChanged.Errors)
                {
                    ModelState.AddModelError("error", error.Description);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _notify.AddErrorToastMessage(ex.Message);
            }
            return View(model);
        }
    }
}
