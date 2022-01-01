
using Inventory.ViewModels.Item;
using InventoryLibrary.AppDbContext;
using InventoryLibrary.Source.DTO.Item;
using InventoryLibrary.Source.Services.ServiceInterface;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InventoryLibrary.Repository.Interface;
using InventoryLibrary.Source.Repository.Interface;

namespace Inventory.Controllers
{
    [Route("api/items")]
    [ApiController]
    public class ItemApiController : ControllerBase
    {
        private readonly ItemServiceInterface _itemService;
        private readonly ItemRepositoryInterface _itemRepo;
        private readonly UnitRepositoryInterface _unitRepo;

        public ItemApiController( ItemServiceInterface _itemService,
            ItemRepositoryInterface itemRepo, UnitRepositoryInterface unitRepo)
        {
            this._itemService = _itemService;
            _itemRepo = itemRepo;
            _unitRepo = unitRepo;
        }




        public async Task<IActionResult> Activate(long id)
        {
            await _itemService.Activate(id);
            _toastNotification.AddSuccessToastMessage("Activated Successfully!");
            return RedirectToAction("Index");
        }

       

        [HttpPost]
        public async Task<IActionResult> Create(ItemCreateIndexView model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var Unit = await _unitRepo.GetById(model.UnitId).ConfigureAwait(true);
                    var item = new ItemCreateDTO { ItemName = model.ItemName, Price = model.Price, Unit = Unit };


                    await _itemService.Create(item).ConfigureAwait(true);
                    return Ok();

                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public async Task<IActionResult> Deactivate(long id)
        {
            await _itemService.Deactivate(id);
            _toastNotification.AddSuccessToastMessage("Deactivated Successfully!");
            return RedirectToAction("Index");
        }

        [HttpPost("Update")]
        public async Task<ActionResult> Update(ItemUpdateViewModel model)
        {
            try
            {
                var Unit = await _unitRepo.GetById(model.UnitId).ConfigureAwait(true);
                var item = new ItemUpdateDTO { ItemId = model.ItemId, Name = model.Name, Price = model.Rate, Unit = Unit };

                await _itemService.Update(item).ConfigureAwait(true);
                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


        }
    }
}
