using KnightTournament.BLL.Implementations;
using KnightTournament.Extensions;
using KnightTournament.Models;
using KnightTournament.Models.Enums;
using KnightTournament.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace KnightTournament.Controllers
{
    [Route("[controller]")]
    public class TournamentController : Controller
    {
        private readonly TournamentService _tournamentService;

        public TournamentController(TournamentService tournamentService)
        {
            _tournamentService = tournamentService;
        }

        [HttpGet("Display")]
        public async Task<IActionResult> Display()
        {
            var getResult = await _tournamentService.GetAllAsync();
            var displayTournamentsViewModel = new DisplayViewModel<Tournament>();
            displayTournamentsViewModel.Entities = (ICollection<Tournament>)getResult.Data;

            return View(displayTournamentsViewModel);
        }

        [HttpGet("Details/{id}")]
        public async Task<IActionResult> Details(Guid id)
        {
            var getByIdResult = await _tournamentService.GetByIdAsync(id);
            if (!getByIdResult.IsSuccessful)
            {
                return RedirectToAction("Error");
            }

            var tournamentDetailViewModel = new TournamentDetailsViewModel();
            getByIdResult.Data.MapTo(ref tournamentDetailViewModel);
            ViewBag.TournamentId = id;
            return View(tournamentDetailViewModel);
        }

        [Authorize(Roles = "StakeHolder")]
        [HttpGet("Create")]
        public IActionResult Create()
        {
            var tournamentDetailViewModel = new TournamentDetailsViewModel();
            if (TempData.ContainsKey("Error"))
            {
                tournamentDetailViewModel.ErrorMessage = TempData["Error"].ToString();
            }

            return View(tournamentDetailViewModel);
        }

        [Authorize(Roles = "StakeHolder")]
        [HttpPost("Create")]
        public async Task<IActionResult> Create(TournamentDetailsViewModel tournamentDetailsViewModel)
        {
            var tournament = new Tournament();
            tournamentDetailsViewModel.MapTo(ref tournament);
            tournament.Status = Status.Planned;
            var result = await _tournamentService.AddAsync(tournament);
            if (!result.IsSuccessful)
            {
                TempData["Error"] = result.Message;
                return RedirectToAction("Create", "Tournament");
            }

            return RedirectToAction("Display", "Tournament");
            //return RedirectToAction("Index", "Home");
        }

        [Authorize(Roles = "StakeHolder")]
        [HttpGet("Update/{id}")]
        public async Task<IActionResult> Update(Guid id)
        {
            var getByIdResult = await _tournamentService.GetByIdAsync(id);
            if (!getByIdResult.IsSuccessful)
            {
                return RedirectToAction("Error");
            }

            var tournamentDetailViewModel = new TournamentDetailsViewModel();
            getByIdResult.Data.MapTo(ref tournamentDetailViewModel);
            return View(tournamentDetailViewModel);
        }

        [Authorize(Roles = "StakeHolder")]
        [HttpPost("Update/{id}")]
        public async Task<IActionResult> Update(Guid id, TournamentDetailsViewModel tournamentDetailsViewModel)
        {
            var result = await _tournamentService.GetByIdAsync(id);
            if (!result.IsSuccessful)
            {
                return RedirectToAction("Error");
            }
            var tournament = result.Data;
            tournamentDetailsViewModel.MapTo(ref tournament);
            await _tournamentService.UpdateAsync(id, tournament);

            return RedirectToAction("Display", "Tournament");
        }

        [Authorize(Roles = "StakeHolder")]
        [HttpGet("Delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var delete = await _tournamentService.DeleteAsync(id);
            if (!delete.IsSuccessful)
            {
                return RedirectToAction("Error");
            }

            return RedirectToAction("Display", "Tournament");
        }
    }
}
