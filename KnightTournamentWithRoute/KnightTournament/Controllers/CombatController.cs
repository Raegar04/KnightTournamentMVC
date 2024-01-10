using KnightTournament.BLL.Implementations;
using KnightTournament.Extensions;
using KnightTournament.Models;
using KnightTournament.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Rotativa.AspNetCore;

namespace KnightTournament.Controllers
{
    [Route("[controller]")]
    public class CombatController : Controller
    {
        private readonly CombatService _combatService;

        private readonly CombatKnightService _combatKnightService;

        private readonly UserManager<AppUser> _userManager;

        public CombatController(CombatService combatService, CombatKnightService combatKnightService, UserManager<AppUser> userManager)
        {
            _combatService = combatService;
            _combatKnightService = combatKnightService;
            _userManager = userManager;
        }

        [HttpGet("Display")]
        public async Task<IActionResult> Display(Guid roundId)
        {
            //var tournamentId = TempData["TournamentUsers_TournamentId"];
            var getResult = await _combatService.GetAllAsync(round => round.Combat_RoundId.ToString() == roundId.ToString());
            var displayCombatsViewModel = new DisplayViewModel<ResultsCombatPDF>();
            var testCombatVM = new ResultsCombatPDF();
            foreach (var combat in getResult.Data) 
            {
                combat.MapTo(ref testCombatVM);
                testCombatVM.PathToImage = testCombatVM.Combat_Type.SetImgToType();
                displayCombatsViewModel.Entities.Add(testCombatVM);
            }
            ViewBag.roundId = roundId;
            return View(displayCombatsViewModel);
        }

        [HttpGet("Create")]
        public IActionResult Create(Guid roundId)
        {
            var combatDetailsViewModel = new ResultsCombatPDF() { Combat_RoundId = roundId };
            if (TempData.ContainsKey("Error"))
            {
                combatDetailsViewModel.ErrorMessage = TempData["Error"].ToString();
            }

            return View(combatDetailsViewModel);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(ResultsCombatPDF combatDetailsViewModel)
        {
            var combat = new Combat();
            combatDetailsViewModel.MapTo(ref combat);
            var result = await _combatService.AddAsync(combat);
            if (!result.IsSuccessful)
            {
                TempData["Error"] = result.Message;
                return RedirectToAction("Create", "Combat");
            }

            return RedirectToAction("Display", "Combat", routeValues: new { roundId = combat.Combat_RoundId });
        }

        [HttpGet("Update/{id}")]
        public async Task<IActionResult> Update(Guid id)
        {
            var getByIdResult = await _combatService.GetByIdAsync(id);
            if (!getByIdResult.IsSuccessful)
            {
                return RedirectToAction("Error");
            }

            var combatDetailsViewModel = new ResultsCombatPDF();
            getByIdResult.Data.MapTo(ref combatDetailsViewModel);
            return View(combatDetailsViewModel);
        }

        [HttpPost("Update/{id}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, ResultsCombatPDF combatDetailsViewModel)
        {
            var result = await _combatService.GetByIdAsync(id);
            if (!result.IsSuccessful)
            {
                return RedirectToAction("Error");
            }
            var combat = result.Data;
            combatDetailsViewModel.MapTo(ref combat);
            await _combatService.UpdateAsync(id, combat);

            return RedirectToAction("Display", "Combat", routeValues: new { roundId = combat.Combat_RoundId });
        }

        [HttpGet("Delete/{id}")]
        public async Task<IActionResult> Delete(Guid roundId, Guid id)
        {
            //var tourId = (await _roundService.GetByIdAsync(id)).Data.TournamentUsers_TournamentId;
            var delete = await _combatService.DeleteAsync(id);
            if (!delete.IsSuccessful)
            {
                return RedirectToAction("Error");
            }


            return RedirectToAction("Display", "Combat", routeValues: new { roundId = roundId });
        }

        [HttpGet("Finish/{id}")]
        public async Task<IActionResult> Finish(Guid id) 
        {
            var combat = (await _combatService.GetByIdAsync(id)).Data;
            Random random = new Random();   
            var combatKnights = await _combatKnightService.GetAllAsync(combKnight=>combKnight.CombatsKnight_CombatId == id);
            foreach (var item in combatKnights.Data)
            {
                item.CombatsKnight_Points = random.Next(10, 40);
                await _combatKnightService.UpdateAsync(item.CombatsKnight_Id, item);
            }

            combat.Combat_IsFinished = true;
            await _combatService.UpdateAsync(id, combat);

            return RedirectToAction("Display", "Combat", new { roundId = combat.Combat_RoundId});
        }

        [HttpGet("LoadPDF")]
        public async Task<IActionResult> LoadPDF(Guid combatId)
        {
            var combat = (await _combatService.GetByIdAsync(combatId)).Data;
            var vm = new CombatPDF();
            combat.MapTo(ref vm);
            vm.Knights = new Dictionary<string, int>();
            var combatKnights = (await _combatKnightService.GetAllAsync(combKnight => combKnight.CombatsKnight_CombatId == combatId)).Data;
            foreach (var item in combatKnights)
            {
                var user = await _userManager.FindByIdAsync(item.CombatsKnight_AppUserId.ToString());
                vm.Knights.Add(user.UserName, item.CombatsKnight_Points);
            }

            return new ViewAsPdf("ResultsPDF", vm);
        }
    }
}
