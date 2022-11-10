using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryLibrary.Source.Entity
{
    public class ApplicationUser:IdentityUser
    {
        public const string RoleAdmin = "Admin";
        public const string RoleUser = "User";
    }
}
