using KnightTournament.BLL.Implementations;
using KnightTournament.Extensions;
using KnightTournament.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace KnightTournament.Controllers
{
    [Route("[controller]")]
    public class TournamentKnightController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        private readonly TournamentUserService _tournamentUserService;

        public TournamentKnightController(UserManager<AppUser> user, TournamentUserService tournamentUserService)
        {
            _userManager = user;
            _tournamentUserService = tournamentUserService;
        }

        [HttpGet("Apply")]
        public async Task<IActionResult> Apply([FromQuery] Guid tournamentId)
        {
            var getUserIdResult = User.GetUserIdFromPrincipal();
            var userId = getUserIdResult.Data;
            await _tournamentUserService.AddAsync(new TournamentUsers() { TournamentId = tournamentId, KnightId = userId });
            return RedirectToAction("Display", "Tournament");
        }
    }
}
