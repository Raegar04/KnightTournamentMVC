using KnightTournament.DAL;
using KnightTournament.Helpers;
using KnightTournament.Models;

namespace KnightTournament.BLL.Implementations
{
    public class TournamentService : GenericService<Tournament>
    {
        private readonly TournamentUserService _tournamentUserService;
        private readonly CombatKnightService _combatKnightService;
        public TournamentService(UnitOfWork unitOfWork, TournamentUserService userService, CombatKnightService combatKnightService)
        {
            _repository = unitOfWork.GetRepository<Tournament>();
            _tournamentUserService = userService;
            _combatKnightService = combatKnightService;
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
    }
}
