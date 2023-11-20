using KnightTournament.BLL.Implementations;
using KnightTournament.Extensions;
using KnightTournament.Models;
using KnightTournament.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace KnightTournament.Controllers
{
    [Route("[controller]")]
    public class LocationController : Controller
    {
        private readonly LocationService _service;

        public LocationController(LocationService locationService)
        {
            _service = locationService;
        }

        [HttpGet("Create")]
        public IActionResult Create()
        {
            var locationDetailsViewModel = new LocationDetailsViewModel();
            ViewBag.LocationAddingError = TempData["LocationAddingError"];
            return View(locationDetailsViewModel);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(LocationDetailsViewModel locationDetailsViewModel)
        {
            var location = new Location();
            locationDetailsViewModel.MapTo(ref location);
            var result = await _service.AddAsync(location);

            if (!result.IsSuccessful)
            {
                TempData["LocationAddingError"] = result.Message;
                return RedirectToAction("Create");
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
