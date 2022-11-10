using InventoryLibrary.Source.Dto.User;
using InventoryLibrary.Source.Entity;
using InventoryLibrary.Source.Services.ServiceInterface;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryLibrary.Source.Services.Implementation
{
    public class UserService : UserServiceInterface
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserService(UserManager<ApplicationUser> userManager
           )
        {
            _userManager = userManager;

        }
        public async Task Create(UserDto dto)
        {
            await ValidateUser(dto);
            var user = new ApplicationUser()
            {
                UserName = dto.UserName,
                Email = dto.Email

            };
            var isSuceeded = await _userManager.CreateAsync(user, dto.Password);
            if (!isSuceeded.Succeeded) throw new Exception(isSuceeded.Errors.First().Description);
            await _userManager.AddToRoleAsync(user, ApplicationUser.RoleUser);
        }
        private async Task ValidateUser(UserDto dto)
        {
            var userWithSameEmail = await _userManager.FindByEmailAsync(dto.Email).ConfigureAwait(false);
            if (userWithSameEmail != null) throw new Exception("User with same email already exists.");
            var userWithSameUserName = await _userManager.FindByNameAsync(dto.UserName).ConfigureAwait(false);
            if (userWithSameUserName != null) throw new Exception("Duplicate User Name.");
        }
    }
}
