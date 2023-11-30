using KnightTournament.DAL;
using KnightTournament.Helpers;
using KnightTournament.Models;
using Microsoft.AspNetCore.Identity;

namespace KnightTournament.BLL.Implementations
{
    public class TournamentService : GenericService<Tournament>
    {
        private readonly TournamentUserService _tournamentUserService;
        private readonly CombatKnightService _combatKnightService;
        private readonly UserManager<AppUser> _userManager;
        private readonly TrophyService trophyService;
        public TournamentService(UnitOfWork unitOfWork, TournamentUserService userService, CombatKnightService combatKnightService, UserManager<AppUser> userManager, TrophyService trophyService)
        {
            _repository = unitOfWork.GetRepository<Tournament>();
            _tournamentUserService = userService;
            _combatKnightService = combatKnightService;
            _userManager = userManager;
            this.trophyService = trophyService;
        }

        public override async Task<Result<bool>> DeleteAsync(Guid id)
        {
            var tournamentKnightsResult = await _tournamentUserService.GetAllAsync(tourUser => tourUser.TournamentId == id);
            if (tournamentKnightsResult.IsSuccessful)
            {
                foreach (var item in tournamentKnightsResult.Data)
                {
                    var result = await _tournamentUserService.DeleteAsync(item.Id);
                }
            }

            var tournament = (await _repository.GetByIdAsync(id)).Data;
            foreach (var round in tournament.Rounds)
            {
                foreach (var combat in round.Combats)
                {
                    var combatKnightResult = await _combatKnightService.GetAllAsync(combKnight => combKnight.CombatId == combat.Id);
                    if (combatKnightResult.IsSuccessful)
                    {
                        foreach (var item in combatKnightResult.Data)
                        {
                            await _combatKnightService.DeleteAsync(item.Id);
                        }
                    }

                }
            }

            return await base.DeleteAsync(id);
        }

        public async Task Finish(Tournament tournament) 
        {
            foreach (var round in tournament.Rounds)
            {
                foreach (var combat in round.Combats)
                {
                    var combatKnights = (await _combatKnightService.GetAllAsync()).Data.Where(item => item.CombatId == combat.Id);
                    var dict = new Dictionary<Guid?, int>();
                    foreach (var combatKnight in combatKnights)
                    {
                        if (dict.ContainsKey(combatKnight.AppUserId))
                        {
                            dict[combatKnight.AppUserId] += combatKnight.Points;
                        }
                    }
                    var user = _userManager.Users.FirstOrDefault(user=>user.Id == dict.FirstOrDefault(i=>i.Value == dict.Values.Max()).Key);
                    round.Trophy.KnightId = user.Id;
                    user.Rating += Convert.ToInt32(round.Trophy.Value);

                    await trophyService.UpdateAsync(round.Trophy.Id, round.Trophy);
                    await _userManager.UpdateAsync(user);
                }
            }
            tournament.IsFinished = true;
            _repository.UpdateItemAsync(tournament.Id, tournament);
        }
    }
}
