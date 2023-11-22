using KnightTournament.DAL;
using KnightTournament.Helpers;
using KnightTournament.Models;

namespace KnightTournament.BLL.Implementations
{
    public class TournamentService:GenericService<Tournament>
    {
        private readonly TournamentUserService _tournamentUserService;
        public TournamentService(UnitOfWork unitOfWork, TournamentUserService userService)
        {
            _repository = unitOfWork.GetRepository<Tournament>();
            _tournamentUserService = userService;
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

            return await base.DeleteAsync(id);
        }
    }
}
