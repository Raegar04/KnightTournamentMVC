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
        private readonly TournamentService _tournamentService;
        private readonly TournamentUserService _tournamentUserService;

        private readonly CombatKnightService _combatKnightService;

        public TournamentKnightController(TournamentUserService tournamentUserService, CombatKnightService combatKnightService, TournamentService tournamentService)
        {
            _tournamentUserService = tournamentUserService;
            _combatKnightService = combatKnightService;
            _tournamentService = tournamentService;
        }

        [HttpGet("Apply")]
        public async Task<IActionResult> Apply([FromQuery] Guid tournamentId)
        {
            var tournament = (await _tournamentService.GetByIdAsync(tournamentId)).Data;
            var getUserIdResult = User.GetUserIdFromPrincipal();
            var userId = getUserIdResult.Data;
            await _tournamentUserService.AddAsync(new TournamentUsers() { TournamentId = tournamentId, KnightId = userId });
            await _combatKnightService.AddUserToCombats(tournament, userId);
            return RedirectToAction("Display", "Tournament");
        }
    }
}
