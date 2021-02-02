using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventoryLibrary.AppDbContext;
using InventoryLibrary.Base;
using InventoryLibrary.Source.Entity;
using InventoryLibrary.Source.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace InventoryLibrary.Source.Repository
{
    public class UnitRepository : BaseRepository<Unit>, UnitRepositoryInterface
    {
        public UnitRepository(Testdbcontext context) : base(context)
        {
        }

        public async Task<Unit> GetByName(string name)
        {
            return await (await this.GetQueryable().ConfigureAwait(false)).Where(a => a.Name == name).SingleOrDefaultAsync();
        }
    }
}
