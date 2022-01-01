using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using InventoryLibrary.Entity;
using InventoryLibrary.Source.Dto.Unit;
using InventoryLibrary.Source.Entity;
using InventoryLibrary.Source.Extension;
using InventoryLibrary.Source.Repository.Interface;
using InventoryLibrary.Source.Services.ServiceInterface;
using ItemCreateDTO = InventoryLibrary.Source.DTO.Item.ItemCreateDTO;

namespace InventoryLibrary.Source.Services.Implementation
{
    public class UnitService : UnitServiceInterface
    {
        private UnitRepositoryInterface _unitRepo;

        public UnitService(UnitRepositoryInterface unitRepo)
        {
            _unitRepo = unitRepo;
        }

        public async Task Create(UnitCreateDTO dto)
        {
            using var tx = TransactionScopeHelper.GetInstance();
            await ValidateUnit(dto.Name);
            var Unit = new Unit(dto.Name);
            await _unitRepo.InsertAsync(Unit).ConfigureAwait(false);

            tx.Complete();
        }

        private async Task ValidateUnit(string dtoItemName, Unit? unit = null)
        {
            var UnitByName = await _unitRepo.GetByName(dtoItemName).ConfigureAwait(false);
            if (UnitByName != null && unit != UnitByName)
            {
                throw new Exception("Unit with same name already exists.");
            }
        }

        public async Task Update(UnitUpdateDTO dto)
        {
            using var tx = TransactionScopeHelper.GetInstance();
            var Unit = await _unitRepo.GetById(dto.UnitId).ConfigureAwait(false);
            await ValidateUnit(dto.Name, Unit);
            Unit.Update(dto.Name);
            await _unitRepo.UpdateAsync(Unit);

            tx.Complete();
        }

        public async Task Activate(long id)
        {
            using var tx = TransactionScopeHelper.GetInstance();

            var item = await _unitRepo.GetById(id).ConfigureAwait(false);

            item.Enable();

            await _unitRepo.UpdateAsync(item).ConfigureAwait(false);


            tx.Complete();
        }

        public async Task Deactivate(long id)
        {
            using var tx = TransactionScopeHelper.GetInstance();

            var item = await _unitRepo.GetById(id).ConfigureAwait(false);

            item.Disable();

            await _unitRepo.UpdateAsync(item).ConfigureAwait(false);

            tx.Complete();
        }
    }
}
