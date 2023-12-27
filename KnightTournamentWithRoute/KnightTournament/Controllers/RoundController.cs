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
            var getResult = await _roundService.GetAllAsync(round => round.Round_TournamentId.ToString() == tournamentId.ToString());
            var displayRoundsViewModel = new DisplayViewModel<Round>()
            {
                Entities = (ICollection<Round>)getResult.Data,
                SearchItems = new List<string>()
                {
                    "All", "Name", "Description"
                },
                FilterItems = new List<string>()
                {
                    "StartDate", "EndDate"
                }
            };
            ViewBag.tournamentId = tournamentId;
            return View(displayRoundsViewModel);
        }

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

            return RedirectToAction("Display", "Round", routeValues: new { tournamentId = round.Round_TournamentId });
        }

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

            return RedirectToAction("Display", "Round", routeValues: new { tournamentId = round.Round_TournamentId });
        }

        [HttpGet("Delete/{id}")]
        public async Task<IActionResult> Delete(Guid tournamentId, Guid id)
        {
            var delete = await _roundService.DeleteAsync(id);
            if (!delete.IsSuccessful)
            {
                return RedirectToAction("Error");
            }


            return RedirectToAction("Display", "Round", routeValues: new { tournamentId = tournamentId });
        }

        [HttpPost("Search")]
        public async Task<IActionResult> Search(DisplayViewModel<Round> displayViewModel, [FromQuery] Guid tournamentId)
        {
            var res = await _roundService.SearchAsync(displayViewModel.SelectedSearchOption, displayViewModel.SearchString, (round)=>round.Round_TournamentId == tournamentId);
            displayViewModel.Entities = (ICollection<Round>)res.Data;
            displayViewModel.SearchItems = new List<string>()
                {
                    "All", "Name", "Description"
                };
            displayViewModel.FilterItems = new List<string>()
                {
                    "StartDate", "EndDate"
                };
            ViewBag.tournamentId = tournamentId;
            return View("Display", displayViewModel);
        }

        [HttpPost("Filter")]
        public async Task<IActionResult> Filter(DisplayViewModel<Round> displayViewModel, [FromQuery] Guid tournamentId)
        {
            var res = await _roundService.FilterAsync(displayViewModel.SelectedSearchOption, displayViewModel.SelectedFrom, displayViewModel.SelectedTo, (round) => round.Round_TournamentId == tournamentId);
            displayViewModel.Entities = (ICollection<Round>)res.Data;
            displayViewModel.SearchItems = new List<string>()
                {
                    "All", "Name", "Description"
                };
            displayViewModel.FilterItems = new List<string>()
                {
                    "StartDate", "EndDate"
                };
            ViewBag.tournamentId = tournamentId;
            return View("Display", displayViewModel);
        }
    }
}
