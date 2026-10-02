using Microsoft.AspNetCore.Mvc;
using TARge25Shop.Core;
using TARge25Shop.Core.Dto;
using TARge25Shop.Data;
using TARge25Shop.Models.RealEstate;

namespace TARge25Shop.Controllers;

public class RealEstateController : Controller
{
    private readonly IRealEstateServices _realEstateServices;
        private readonly TARge25ShopContext _dbContext;

        public RealEstateController (IRealEstateServices realEstateServices, TARge25ShopContext dbContext)
        {
            _realEstateServices = realEstateServices;
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            var result = _dbContext.RealEstates
                .Select(x => new RealEstateIndexViewModel
                {
                    Id = x.Id,
                    AreaCode = x.AreaCode,
                    Location = x.Location,
                    NrOfRooms = x.NrOfRooms,
                    BuildingType = x.BuildingType
                });

            return View(result);
        }

        [HttpGet]
        public IActionResult Create()
        {
            RealEstateCreateUpdateViewModel result = new();

            return View("CreateUpdate", result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(RealEstateCreateUpdateViewModel vm)
        {
            var dto = new RealEstateDto
            {
                AreaCode = vm.AreaCode,
                Location = vm.Location,
                NrOfRooms = vm.NrOfRooms,
                BuildingType = vm.BuildingType
            };

            var result = await _realEstateServices.Create(dto);

            if (result == null) {
                ModelState.AddModelError(
                    string.Empty, "Could not create real estate! Check your fields and try again."
                );
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            var realEstate = await _realEstateServices.DetailAsync(id);

            if (realEstate == null)
            {
                return NotFound();
            }

            var viewModel = new RealEstateCreateUpdateViewModel();

            viewModel.Id = realEstate.Id;
            viewModel.AreaCode = realEstate.AreaCode;
            viewModel.Location = realEstate.Location;
            viewModel.NrOfRooms = realEstate.NrOfRooms;
            viewModel.BuildingType = realEstate.BuildingType;
            viewModel.CreatedAt = realEstate.CreatedAt;
            viewModel.ModifiedAt = realEstate.ModifiedAt;

            return View("CreateUpdate", viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Update(RealEstateCreateUpdateViewModel viewModel)
        {
            var dto = new RealEstateDto()
            {
                Id = viewModel.Id,
                AreaCode = viewModel.AreaCode,
                Location = viewModel.Location,
                NrOfRooms = viewModel.NrOfRooms,
                BuildingType = viewModel.BuildingType,
                CreatedAt = viewModel.CreatedAt,
                ModifiedAt = viewModel.ModifiedAt
            };

            var result = await _realEstateServices.Update(dto);

            if (result == null)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var realEstate = await _realEstateServices.DetailAsync(id);

            if (realEstate == null)
            {
                return NotFound();
            }

            var viewModel = new RealEstateDeleteViewModel();

            viewModel.Id = realEstate.Id;
            viewModel.AreaCode = realEstate.AreaCode;
            viewModel.Location = realEstate.Location;
            viewModel.NrOfRooms = realEstate.NrOfRooms;
            viewModel.BuildingType = realEstate.BuildingType;
            viewModel.CreatedAt = realEstate.CreatedAt;
            viewModel.ModifiedAt = realEstate.ModifiedAt;

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmation(Guid id)
        {
            var realEstate = await _realEstateServices.Delete(id);

            if (realEstate == null)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var realEstate = await _realEstateServices.DetailAsync(id);

            if (realEstate == null)
            {
                return NotFound();
            }

            var viewModel = new RealEstateDetailsViewModel();

            viewModel.Id = realEstate.Id;
            viewModel.AreaCode = realEstate.AreaCode;
            viewModel.Location = realEstate.Location;
            viewModel.NrOfRooms = realEstate.NrOfRooms;
            viewModel.BuildingType = realEstate.BuildingType;
            viewModel.CreatedAt = realEstate.CreatedAt;
            viewModel.ModifiedAt = realEstate.ModifiedAt;

            return View(viewModel);
        }
}