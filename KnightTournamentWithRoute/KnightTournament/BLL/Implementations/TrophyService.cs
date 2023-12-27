using KnightTournament.DAL;
using KnightTournament.Models;

namespace KnightTournament.BLL.Implementations
{
    public class TrophyService:GenericService<Trophy>
    {
        private readonly RoundService _roundService;
        public TrophyService(UnitOfWork unitOfWork, RoundService roundService)
        {
            _repository = unitOfWork.GetRepository<Trophy>();
            _roundService = roundService;
        }

        public async Task<Guid?> GetTournamentIdFromTrophy(Guid roundId) 
        {
            var round = (await _roundService.GetByIdAsync(roundId)).Data;
            return round.Round_TournamentId;
        }
    }
}
