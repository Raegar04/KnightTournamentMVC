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
            var tournamentKnightsResult = await _tournamentUserService.GetAllAsync(tourUser => tourUser.TournamentUsers_TournamentId == id);
            if (tournamentKnightsResult.IsSuccessful)
            {
                foreach (var item in tournamentKnightsResult.Data)
                {
                    var result = await _tournamentUserService.DeleteAsync(item.TournamentUsers_Id);
                }
            }

            var tournament = (await _repository.GetByIdAsync(id)).Data;
            foreach (var round in tournament.Tournament_Rounds)
            {
                foreach (var combat in round.Round_Combats)
                {
                    var combatKnightResult = await _combatKnightService.GetAllAsync(combKnight => combKnight.CombatsKnight_CombatId == combat.Combat_Id);
                    if (combatKnightResult.IsSuccessful)
                    {
                        foreach (var item in combatKnightResult.Data)
                        {
                            await _combatKnightService.DeleteAsync(item.CombatsKnight_Id);
                        }
                    }

                }
            }

            return await base.DeleteAsync(id);
        }

        public async Task Finish(Tournament tournament)
        {
            foreach (var round in tournament.Tournament_Rounds)
            {
                var dict = new Dictionary<Guid?, int>();
                foreach (var combat in round.Round_Combats)
                {
                    var combatKnights = (await _combatKnightService.GetAllAsync(item => item.CombatsKnight_CombatId == combat.Combat_Id)).Data;

                    foreach (var combatKnight in combatKnights)
                    {
                        if (dict.ContainsKey(combatKnight.CombatsKnight_AppUserId))
                        {
                            dict[combatKnight.CombatsKnight_AppUserId] += combatKnight.CombatsKnight_Points;
                        }
                        else
                        {
                            dict.Add(combatKnight.CombatsKnight_AppUserId, combatKnight.CombatsKnight_Points);
                        }
                    }
                }
                if (dict.Count != 0)
                {
                    var max = dict.Values.Max();
                    var key = dict.FirstOrDefault(i => i.Value == max).Key;
                    var user = await _userManager.FindByIdAsync(key.ToString());
                    round.Round_Trophy.Trophy_KnightId = user.Id;
                    user.User_Rating += Convert.ToInt32(round.Round_Trophy.Trophy_Value);

                    await trophyService.UpdateAsync(round.Round_Trophy.Trophy_Id, round.Round_Trophy);
                    await _userManager.UpdateAsync(user);
                }

            }

            tournament.Tournament_Status = Models.Enums.Status.Ended;
            _repository.UpdateItemAsync(tournament.Tournament_Id, tournament);
        }
    }
}
