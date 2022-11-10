using InventoryLibrary.Source.Dto.User;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace InventoryLibrary.Source.Services.ServiceInterface
{
    public interface UserServiceInterface
    {
        Task Create(UserDto dto);
    }
}
