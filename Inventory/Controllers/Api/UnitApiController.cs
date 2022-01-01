using Inventory.ViewModels.Unit;
using InventoryLibrary.Source.Dto.Unit;
using InventoryLibrary.Source.Entity;
using InventoryLibrary.Source.Repository.Interface;
using InventoryLibrary.Source.Services.ServiceInterface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inventory.Controllers.Api
{
    [Route("api/units")]
    [ApiController]
    public class UnitApiController : ControllerBase
    {
        private readonly UnitServiceInterface _unitService;
        private readonly UnitRepositoryInterface _unitRepo;

        public UnitApiController(UnitServiceInterface _UnitService, UnitRepositoryInterface unitRepo)
        {
            _unitService = _UnitService;
            _unitRepo = unitRepo;
        }
        [HttpGet("")]
        public async Task<ActionResult> GetAllUnitsAsync()
        {
            try
            {
                var Units = await _unitRepo.GetAllAsync();

                return Ok(Units.Select(a => CreateReponseDto(a)).ToList());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(long id)
        {
            try
            {
                var Unit = await _unitRepo.GetById(id) ?? throw new Exception("Unit Not Found.");
                return Ok(CreateReponseDto(Unit));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        private object CreateReponseDto(Unit a)
        {
            return new
            {
                Name = a.Name,
                Status = a.Status,
                Id = a.Id
            };
        }
        [HttpPost("Create")]
        public async Task<IActionResult> Create(UnitCreateIndexView model)
        {
            try
            {

                var UnitDto = new UnitCreateDTO { Name = model.UnitName };


                await _unitService.Create(UnitDto).ConfigureAwait(true);
                var Unit = await _unitRepo.GetByName(UnitDto.Name) ?? throw new Exception("Unit Not Found.");
                return Ok(CreateReponseDto(Unit));

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("Update")]
        public async Task<IActionResult> Update(UnitUpdateViewModel model)
        {
            try
            {

                var UnitDto = new UnitUpdateDTO { UnitId = model.UnitId, Name = model.Name };
                await _unitService.Update(UnitDto).ConfigureAwait(true);

                var Unit = await _unitRepo.GetByName(UnitDto.Name) ?? throw new Exception("Unit Not Found.");
                return Ok(CreateReponseDto(Unit));

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("activate/{id}")]
        public async Task<ActionResult> Activate(long id)
        {
            try
            {
                await _unitService.Activate(id);
                var Unit = await _unitRepo.GetById(id) ?? throw new Exception("Unit Not Found.");
                return Ok(CreateReponseDto(Unit));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("deactivate/{id}")]
        public async Task<ActionResult> Deactivate(long id)
        {
            try
            {
                await _unitService.Deactivate(id);
                var Unit = await _unitRepo.GetById(id) ?? throw new Exception("Unit Not Found.");
                return Ok(CreateReponseDto(Unit));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
