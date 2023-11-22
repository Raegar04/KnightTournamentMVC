using KnightTournament.DAL;
using KnightTournament.Helpers;
using KnightTournament.Models;

namespace KnightTournament.BLL.Implementations
{
    public class CombatKnightService : GenericService<CombatsKnight>
    {
        private readonly TournamentService _tournamentService;

        public CombatKnightService(UnitOfWork unitOfWork, TournamentService tournamentService)
        {
            _repository = unitOfWork.GetRepository<CombatsKnight>();
            _tournamentService = tournamentService;
        }

        public async Task<Result<bool>> AddUserToCombats(Guid tournamentId, Guid userId)
        {
            var tournament = (await _tournamentService.GetByIdAsync(tournamentId)).Data;
            foreach (var round in tournament.Rounds)
            {
                foreach (var combat in round.Combats)
                {

                    try
                    {
                        await _repository.context.AddAsync(new CombatsKnight() { CombatId = combat.Id, KnightId = userId });
                        await _repository.context.SaveChangesAsync();
                    }
                    catch
                    {
                        return new Result<bool>(false, "Cannot apply to combat");
                    }
                }
            }

            return new Result<bool>(true);
        }
    }
}
