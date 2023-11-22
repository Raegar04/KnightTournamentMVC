using KnightTournament.DAL;
using KnightTournament.Models;

namespace KnightTournament.BLL.Implementations
{
    public class TournamentUserService:GenericService<TournamentUsers>
    {
        public TournamentUserService(UnitOfWork unitOfWork)
        {
            _repository = unitOfWork.GetRepository<TournamentUsers>();
        }
    }
}
