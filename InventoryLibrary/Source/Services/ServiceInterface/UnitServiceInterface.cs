using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using InventoryLibrary.Source.Dto.Unit;

namespace InventoryLibrary.Source.Services.ServiceInterface
{
    public interface UnitServiceInterface
    {
        Task Create(UnitCreateDTO dto);
        Task Update(UnitUpdateDTO dto);
        Task Activate(long id);
        Task Deactivate(long id);
    }
}
