using KnightTournament.BLL.Implementations;
using KnightTournament.Models.Enums;
using KnightTournament.Models;
using KnightTournament.ViewModels;
using Microsoft.AspNetCore.Mvc;
using KnightTournament.Extensions;
using Microsoft.AspNetCore.Authorization;

namespace KnightTournament.Controllers
{
    [Route("[controller]")]
    public class RoundController : Controller
    {
        private readonly RoundService _roundService;

        public RoundController(RoundService roundService)
        {
            _roundService = roundService;

        }

        [HttpGet("Display")]
        public async Task<IActionResult> Display(Guid tournamentId)
        {
            //var tournamentId = TempData["TournamentId"];
            var getResult = await _roundService.GetAllAsync(round => round.TournamentId.ToString() == tournamentId.ToString());
            var displayTournamentsViewModel = new DisplayViewModel<Round>();
            displayTournamentsViewModel.Entities = (ICollection<Round>)getResult.Data;
            ViewBag.tournamentId = tournamentId;
            return View(displayTournamentsViewModel);
        }

        [HttpGet("Details/{id}")]
        public async Task<IActionResult> Details(Guid id)
        {
            var getByIdResult = await _roundService.GetByIdAsync(id);
            if (!getByIdResult.IsSuccessful)
            {
                return RedirectToAction("Error");
            }

            var roundDetailsViewModel = new RoundDetailsViewModel();
            getByIdResult.Data.MapTo(ref roundDetailsViewModel);
            ViewBag.roundId = id;
            return View(roundDetailsViewModel);
        }

        [Authorize(Roles = "StakeHolder")]
        [HttpGet("Create")]
        public IActionResult Create(Guid tournamentId)
        {
            var roundDetailViewModel = new RoundDetailsViewModel() { TournamentId = tournamentId };
            if (TempData.ContainsKey("Error"))
            {
                roundDetailViewModel.ErrorMessage = TempData["Error"].ToString();
            }

            return View(roundDetailViewModel);
        }

        [Authorize(Roles = "StakeHolder")]
        [HttpPost("Create")]
        public async Task<IActionResult> Create(RoundDetailsViewModel roundDetailsViewModel)
        {
            var round = new Round();
            roundDetailsViewModel.MapTo(ref round);
            var result = await _roundService.AddAsync(round);
            if (!result.IsSuccessful)
            {
                TempData["Error"] = result.Message;
                return RedirectToAction("Create", "Round");
            }

            return RedirectToAction("Details", "Tournament", routeValues:new { id =  round.TournamentId});
        }

        [Authorize(Roles = "StakeHolder")]
        [HttpGet("Update/{id}")]
        public async Task<IActionResult> Update(Guid id)
        {
            var getByIdResult = await _roundService.GetByIdAsync(id);
            if (!getByIdResult.IsSuccessful)
            {
                return RedirectToAction("Error");
            }

            var roundDetailViewModel = new RoundDetailsViewModel();
            getByIdResult.Data.MapTo(ref roundDetailViewModel);
            return View(roundDetailViewModel);
        }

        [Authorize(Roles = "StakeHolder")]
        [HttpPost("Update/{id}")]
        public async Task<IActionResult> Update(Guid id, RoundDetailsViewModel tournamentDetailsViewModel)
        {
            var result = await _roundService.GetByIdAsync(id);
            if (!result.IsSuccessful)
            {
                return RedirectToAction("Error");
            }
            var round = result.Data;
            tournamentDetailsViewModel.MapTo(ref round);
            await _roundService.UpdateAsync(id, round);

            return RedirectToAction("Details", "Tournament", routeValues: new { id = round.TournamentId });
        }

        [Authorize(Roles = "StakeHolder")]
        [HttpGet("Delete/{id}")]
        public async Task<IActionResult> Delete(Guid tournamentId, Guid id)
        {
            //var tourId = (await _roundService.GetByIdAsync(id)).Data.TournamentId;
            var delete = await _roundService.DeleteAsync(id);
            if (!delete.IsSuccessful)
            {
                return RedirectToAction("Error");
            }


            return RedirectToAction("Details", "Tournament", new { id = tournamentId });
        }
    }
}
