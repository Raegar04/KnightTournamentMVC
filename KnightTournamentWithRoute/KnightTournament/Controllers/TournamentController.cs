using KnightTournament.BLL.Implementations;
using KnightTournament.Extensions;
using KnightTournament.Helpers;
using KnightTournament.Models;
using KnightTournament.Models.Enums;
using KnightTournament.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;

namespace KnightTournament.Controllers
{
    [Route("[controller]")]
    public class TournamentController : Controller
    {
        private readonly TournamentService _tournamentService;
        private readonly UserManager<AppUser> _userManager;
        private readonly TournamentUserService _tournamentUserService;

        public TournamentController(TournamentService tournamentService, UserManager<AppUser> userManager, TournamentUserService tournamentUserService)
        {
            _tournamentService = tournamentService;
            _userManager = userManager;
            _tournamentUserService = tournamentUserService;
        }

        [Authorize]
        [HttpGet("Display")]
        public async Task<IActionResult> Display()
        {
            var getResult = await _tournamentService.GetAllAsync();
            var displayTournamentsViewModel = new DisplayViewModel<Tournament>()
            {
                Entities = (ICollection<Tournament>)getResult.Data,
                SearchItems = new List<string>()
                {
                    "All", "Name", "Description", "Status", "Location"
                },
                FilterItems = new List<string>()
                {
                    "Scope", "StartDate"
                }
            };

            return View(displayTournamentsViewModel);
        }

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

        [HttpPost("Search")]
        public async Task<IActionResult> Search(DisplayViewModel<Tournament> displayViewModel)
        {
            var res = await _tournamentService.SearchAsync(displayViewModel.SelectedSearchOption, displayViewModel.SearchString);
            displayViewModel.Entities = (ICollection<Tournament>)res.Data;
            displayViewModel.SearchItems = new List<string>()
                {
                    "All", "Name", "Description", "Status", "Location"
                };
            displayViewModel.FilterItems = new List<string>()
                {
                    "Scope", "StartDate"
                };
            return View("Display", displayViewModel);
        }

        [HttpPost("Filter")]
        public async Task<IActionResult> Filter(DisplayViewModel<Tournament> displayViewModel)
        {
            var res = await _tournamentService.FilterAsync(displayViewModel.SelectedSearchOption, displayViewModel.SelectedFrom, displayViewModel.SelectedTo);
            displayViewModel.Entities = (ICollection<Tournament>)res.Data;
            displayViewModel.SearchItems = new List<string>()
                {
                    "All", "Name", "Description", "Status", "Location"
                };
            displayViewModel.FilterItems = new List<string>()
                {
                    "Scope", "StartDate"
                };
            return View("Display", displayViewModel);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(TournamentDetailsViewModel tournamentDetailsViewModel)
        {
            var tournament = new Tournament();
            tournamentDetailsViewModel.MapTo(ref tournament);
            var holderIdresult = User.GetUserIdFromPrincipal();
            if (!holderIdresult.IsSuccessful)
            {
                TempData["Error"] = holderIdresult.Message;
                return RedirectToAction("Create", "Tournament");
            }
            tournament.HolderId = holderIdresult.Data;
            var result = await _tournamentService.AddAsync(tournament);
            if (!result.IsSuccessful)
            {
                TempData["Error"] = result.Message;
                return RedirectToAction("Create", "Tournament");
            }

            return RedirectToAction("Display", "Tournament");
        }

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

        [HttpGet("Delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _tournamentService.DeleteAsync(id);
            return RedirectToAction("Display", "Tournament");
        }
    }
}
