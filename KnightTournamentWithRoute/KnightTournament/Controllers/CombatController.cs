using KnightTournament.BLL.Implementations;
using KnightTournament.Extensions;
using KnightTournament.Models;
using KnightTournament.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KnightTournament.Controllers
{
    [Route("[controller]")]
    public class CombatController : Controller
    {
        private readonly CombatService _combatService;

        public CombatController(CombatService combatService)
        {
            _combatService = combatService;
        }

        [HttpGet("Display")]
        public async Task<IActionResult> Display(Guid roundId)
        {
            //var tournamentId = TempData["TournamentId"];
            var getResult = await _combatService.GetAllAsync(round => round.RoundId.ToString() == roundId.ToString());
            var displayCombatsViewModel = new DisplayViewModel<CombatDetailsViewModel>();
            var testCombatVM = new CombatDetailsViewModel();
            foreach (var combat in getResult.Data) 
            {
                combat.MapTo(ref testCombatVM);
                testCombatVM.PathToImage = testCombatVM.Type.SetImgToType();
                displayCombatsViewModel.Entities.Add(testCombatVM);
            }
            ViewBag.roundId = roundId;
            return View(displayCombatsViewModel);
        }

        [HttpGet("Details/{id}")]
        public async Task<IActionResult> Details(Guid id)
        {
            var getByIdResult = await _combatService.GetByIdAsync(id);
            if (!getByIdResult.IsSuccessful)
            {
                return RedirectToAction("Error");
            }

            var combatDetailsViewModel = new CombatDetailsViewModel();
            getByIdResult.Data.MapTo(ref combatDetailsViewModel);
            combatDetailsViewModel.PathToImage = combatDetailsViewModel.Type.SetImgToType();
            return View(combatDetailsViewModel);
        }

        [HttpGet("Create")]
        public IActionResult Create(Guid roundId)
        {
            var combatDetailsViewModel = new CombatDetailsViewModel() { RoundId = roundId };
            if (TempData.ContainsKey("Error"))
            {
                combatDetailsViewModel.ErrorMessage = TempData["Error"].ToString();
            }

            return View(combatDetailsViewModel);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(CombatDetailsViewModel combatDetailsViewModel)
        {
            var combat = new Combat();
            combatDetailsViewModel.MapTo(ref combat);
            var result = await _combatService.AddAsync(combat);
            if (!result.IsSuccessful)
            {
                TempData["Error"] = result.Message;
                return RedirectToAction("Create", "Combat");
            }

            return RedirectToAction("Details", "Round", routeValues: new { id = combat.RoundId });
        }

        [HttpGet("Update/{id}")]
        public async Task<IActionResult> Update(Guid id)
        {
            var getByIdResult = await _combatService.GetByIdAsync(id);
            if (!getByIdResult.IsSuccessful)
            {
                return RedirectToAction("Error");
            }

            var combatDetailsViewModel = new CombatDetailsViewModel();
            getByIdResult.Data.MapTo(ref combatDetailsViewModel);
            return View(combatDetailsViewModel);
        }

        [HttpPost("Update/{id}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, CombatDetailsViewModel combatDetailsViewModel)
        {
            var result = await _combatService.GetByIdAsync(id);
            if (!result.IsSuccessful)
            {
                return RedirectToAction("Error");
            }
            var combat = result.Data;
            combatDetailsViewModel.MapTo(ref combat);
            await _combatService.UpdateAsync(id, combat);

            return RedirectToAction("Details", "Round", routeValues: new { id = combat.RoundId });
        }

        [HttpGet("Delete/{id}")]
        public async Task<IActionResult> Delete(Guid roundId, Guid id)
        {
            //var tourId = (await _roundService.GetByIdAsync(id)).Data.TournamentId;
            var delete = await _combatService.DeleteAsync(id);
            if (!delete.IsSuccessful)
            {
                return RedirectToAction("Error");
            }


            return RedirectToAction("Details", "Round", new { id = roundId });
        }
    }
}
