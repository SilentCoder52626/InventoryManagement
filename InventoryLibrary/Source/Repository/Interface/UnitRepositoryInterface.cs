using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using InventoryLibrary.Base;
using InventoryLibrary.Source.Entity;

namespace InventoryLibrary.Source.Repository.Interface
{
    public interface UnitRepositoryInterface : BaseRepositoryInterface<Unit>
    {
        Task<Unit> GetByName(string name);
    }
}
