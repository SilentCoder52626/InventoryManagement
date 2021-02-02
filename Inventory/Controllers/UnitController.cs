using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Inventory.ViewModels.Unit;
using InventoryLibrary.AppDbContext;
using InventoryLibrary.Source.Dto.Unit;
using InventoryLibrary.Source.Repository.Interface;
using InventoryLibrary.Source.Services.ServiceInterface;
using NToastNotify;
using UnitCreateDTO = InventoryLibrary.Source.DTO.Item.UnitCreateDTO;

namespace Inventory.Controllers
{
    public class UnitController : Controller
    {
        private readonly Testdbcontext _context;
        private readonly IToastNotification _toastNotification;
        private readonly UnitServiceInterface _unitService;
        private readonly UnitRepositoryInterface _unitRepo;

        public UnitController(Testdbcontext context, IToastNotification toastNotification, UnitServiceInterface _UnitService, UnitRepositoryInterface unitRepo)
        {
            _context = context;
            _toastNotification = toastNotification;
            this._unitService = _UnitService;
            _unitRepo = unitRepo;
        }




        public async Task<IActionResult> Activate(long id)
        {
            await _unitService.Activate(id);
            _toastNotification.AddSuccessToastMessage("Activated Successfully!");
            return RedirectToAction("Index");
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(UnitCreateIndexView model)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    var Unit = new UnitCreateDTO {ItemName = model.UnitName};


                    await _unitService.Create(Unit).ConfigureAwait(true);
                    _toastNotification.AddSuccessToastMessage("Successfully Created Unit :- " + Unit.ItemName);

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
            await _unitService.Deactivate(id);
            _toastNotification.AddSuccessToastMessage("Deactivated Successfully!");
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Index()
        {

            var Unit = await _unitRepo.GetAllAsync().ConfigureAwait(true);
            var indexViewModel = new List<UnitIndexViewModel>();

            foreach (var data in Unit)
            {
                var model = new UnitIndexViewModel();
                model.UnitId = data.Id;
                model.Status = data.Status;
                model.Name = data.Name;
                indexViewModel.Add(model);
            }

            return View(indexViewModel);
        }



        public async Task<IActionResult> Update(long id)
        {
            var Unit = await _unitRepo.GetById(id).ConfigureAwait(true);

            var model = new UnitUpdateViewModel();

            model.UnitId = Unit.Id;
            model.Name = Unit.Name;

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Update(UnitUpdateViewModel model)
        {
            try
            {
                // var Unit = _context.Units.Find(model.UnitId);

                var Unit = new UnitUpdateDTO {UnitId = model.UnitId, Name = model.Name};


                await _unitService.Update(Unit).ConfigureAwait(true);

                _toastNotification.AddInfoToastMessage("Updated to :- " + Unit.Name);

            }
            catch (Exception ex)
            {
                _toastNotification.AddErrorToastMessage("Error while in execution of update statement!");
            }

            return RedirectToAction("Index");

        }
    }
}

