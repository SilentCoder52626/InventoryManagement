
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
    public class ItemController : Controller
    {
        private readonly IToastNotification _toastNotification;
        private readonly ItemServiceInterface _itemService;
        private readonly ItemRepositoryInterface _itemRepo;
        private readonly UnitRepositoryInterface _unitRepo;

        public ItemController(IToastNotification toastNotification, ItemServiceInterface _itemService,
            ItemRepositoryInterface itemRepo, UnitRepositoryInterface unitRepo)
        {
            _toastNotification = toastNotification;
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

        public async Task<IActionResult> Create()
        {
            ItemCreateIndexView model = new ItemCreateIndexView
            {
                Units = (await _unitRepo.GetAllAsync().ConfigureAwait(true)).Where(a => a.IsActive()).ToList()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ItemCreateIndexView model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var Unit = await _unitRepo.GetById(model.UnitId).ConfigureAwait(true);
                    var item = new UnitCreateDTO { ItemName = model.ItemName, Price = model.Price, Unit = Unit };


                    await _itemService.Create(item).ConfigureAwait(true);
                    _toastNotification.AddSuccessToastMessage("Successfully Created Item :- " + item.ItemName);

                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                model.Units = (await _unitRepo.GetAllAsync().ConfigureAwait(true)).Where(a => a.IsActive()).ToList();
                _toastNotification.AddErrorToastMessage(ex.Message);
            }
            return View(model);
        }

        public async Task<IActionResult> Deactivate(long id)
        {
            await _itemService.Deactivate(id);
            _toastNotification.AddSuccessToastMessage("Deactivated Successfully!");
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Index()
        {

            var Item = await _itemRepo.GetAllAsync().ConfigureAwait(true);
            var indexViewModel = new List<ItemIndexViewModel>();

            foreach (var data in Item)
            {
                var model = new ItemIndexViewModel
                {
                    ItemId = data.Id, Status = data.Status, Name = data.Name, UnitName = data.Unit.Name,
                    Rate = data.Rate, AvailableQty = data.AvailableQty
                };
                indexViewModel.Add(model);
            }

            return View(indexViewModel);
        }



        public async Task<IActionResult> Update(long id)
        {
            var Item = (await _itemRepo.GetById(id).ConfigureAwait(true));

            var model = new ItemUpdateViewModel
            {
                ItemId = Item.Id,
                Name = Item.Name,
                UnitId = Item.UnitId,
                Rate = Item.Rate,
                Units = (await _unitRepo.GetAllAsync().ConfigureAwait(true)).Where(a => a.IsActive()).ToList()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Update(ItemUpdateViewModel model)
        {
            try
            {
                var Unit = await _unitRepo.GetById(model.UnitId).ConfigureAwait(true);
                var item = new ItemUpdateDTO { ItemId = model.ItemId, Name = model.Name, Price = model.Rate, Unit = Unit };


                await _itemService.Update(item).ConfigureAwait(true);

                _toastNotification.AddInfoToastMessage("Successfully Updated to :- " + item.Name);

            }
            catch (Exception ex)
            {
                model.Units = (await _unitRepo.GetAllAsync().ConfigureAwait(true)).Where(a => a.IsActive()).ToList();

                _toastNotification.AddErrorToastMessage("Error while in execution of update statement!");
            }

            return RedirectToAction("Index");

        }
    }
}
