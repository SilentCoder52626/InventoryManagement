using InventoryLibrary.AppDbContext;
using InventoryLibrary.Base;
using InventoryLibrary.Source.Entity;
using InventoryLibrary.Source.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryLibrary.Source.Repository
{
    public class UserRepository : BaseRepository<ApplicationUser>, UserRepositoryInterface
    {
        public UserRepository(Testdbcontext context) : base(context)
        {

        }
        public async Task<ApplicationUser> GetByIdString(string id)
        {
            return await GetQueryable().Where(a => a.Id == id).SingleOrDefaultAsync().ConfigureAwait(false);
        }
    }
}
