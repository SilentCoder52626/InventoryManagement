
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

namespace Inventory.Controllers
{
    public class ItemController : Controller
    {
        private readonly Testdbcontext _context;
        private readonly IToastNotification _toastNotification;
        private readonly ItemServiceInterface _itemService;

        public ItemController(Testdbcontext context, IToastNotification toastNotification, ItemServiceInterface _itemService)
        {
            _context = context;
            _toastNotification = toastNotification;
            this._itemService = _itemService;

        }




        public async Task<IActionResult> Activate(long id)
        {
            await _itemService.Activate(id);
            _toastNotification.AddSuccessToastMessage("Activated Succesfully!s");
            return RedirectToAction("Index");
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ItemCreateIndexView model)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    var item = new ItemCreateDTO();

                    item.ItemName = model.ItemName;
                    item.Price = model.Price;

                    await _itemService.Create(item).ConfigureAwait(true);
                    _toastNotification.AddSuccessToastMessage("Sucessfully Created Item :- " + item.ItemName);

                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {

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

        public IActionResult Index()
        {

            var Item = _context.Items.ToList();
            var indexViewModel = new List<ItemIndexViewModel>();

            foreach (var data in Item)
            {
                var model = new ItemIndexViewModel();
                model.ItemId = data.Id;
                model.Status = data.Status;
                model.Name = data.Name;
                indexViewModel.Add(model);
            }

            return View(indexViewModel);
        }



        public IActionResult Update(long id)
        {
            var Item = _context.Items.Where(a => a.Id == id).SingleOrDefault();

            var model = new ItemUpdateViewModel();

            model.ItemId = Item.Id;
            model.Name = Item.Name;

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Update(ItemUpdateViewModel model)
        {
            try
            {
                // var item = _context.Items.Find(model.ItemId);

                var item = new ItemUpdateDTO();

                item.ItemId = model.ItemId;
                item.Name = model.Name;

                await _itemService.Update(item).ConfigureAwait(true);

                _toastNotification.AddInfoToastMessage("Updated to :- " + item.Name);

            }
            catch (Exception ex)
            {
                _toastNotification.AddErrorToastMessage("Error while in execution of update statement!");
            }

            return RedirectToAction("Index");

        }
        public async Task<IActionResult> Remove(long id)
        {
            // var _item = _context.Items.Find(id);

            await _itemService.Delete(id).ConfigureAwait(true);

            return RedirectToAction("Index");
        }
    }
}
