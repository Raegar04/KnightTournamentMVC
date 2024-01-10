using KnightTournament.BLL.Implementations;
using KnightTournament.Extensions;
using KnightTournament.Models;
using KnightTournament.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace KnightTournament.Controllers
{
    [Route("[controller]")]
    public class TrophyController : Controller
    {
        private readonly TrophyService _service;

        public TrophyController(TrophyService trophyService)
        {
            _service = trophyService;
        }

        [HttpGet("Create")]
        public IActionResult Create(Guid roundId)
        {
            var trophyViewModel = new TrophyDetailsViewModel() { Trophy_RoundId = roundId};
            ViewBag.TrophyAddingError = TempData["TrophyAddingError"];
            return View(trophyViewModel);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(TrophyDetailsViewModel trophyDetailsViewModel)
        {
            var trophy = new Trophy();
            trophyDetailsViewModel.MapTo(ref trophy);
            var result = await _service.AddAsync(trophy);

            if (!result.IsSuccessful)
            {
                TempData["TrophyAddingError"] = result.Message;
                return RedirectToAction("Create");
            }

            var tournamentId = await _service.GetTournamentIdFromTrophy(trophy.Trophy_RoundId);
            return RedirectToAction("Display", "Round", new { tournamentId = tournamentId });
        }

        [HttpGet("Delete/{id}")]
        public async Task<IActionResult> Delete([FromRoute]Guid id) 
        {
            var roundId = (await _service.GetByIdAsync(id)).Data.Trophy_RoundId;
            var tournamentId = await _service.GetTournamentIdFromTrophy(roundId);
            await _service.DeleteAsync(id);

            return RedirectToAction("Display", "Round", new { tournamentId = tournamentId });
        }
    }
}
