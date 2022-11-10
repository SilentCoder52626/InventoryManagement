using InventoryLibrary.Source.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Inventory.Extensions
{
    public static class GetCurrentUserExtension
    {
        public static string? GetCurrentUserId(this ControllerBase controller)
        {
            return controller.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }

        public static async Task<ApplicationUser> GetCurrentUser(this ControllerBase controller)
        {
            var userId = controller.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            using var serviceScope = ServiceActivator.GetScope();
            var userManager = serviceScope.ServiceProvider.GetService<UserManager<ApplicationUser>>();
            return await userManager.FindByIdAsync(userId).ConfigureAwait(true) ?? throw new System.Exception("User not found");
        }
    }
}
