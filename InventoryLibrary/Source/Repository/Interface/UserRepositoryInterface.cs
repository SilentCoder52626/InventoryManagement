using InventoryLibrary.Base;
using InventoryLibrary.Source.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace InventoryLibrary.Source.Repository.Interface
{
    public interface UserRepositoryInterface : BaseRepositoryInterface<ApplicationUser>
    {
        Task<ApplicationUser> GetByIdString(string id);
    }
}
