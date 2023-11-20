using KnightTournament.DAL;
using KnightTournament.Models;

namespace KnightTournament.BLL.Implementations
{
    public class TournamentService:GenericService<Tournament>
    {
        public TournamentService(UnitOfWork unitOfWork) 
        {
            _repository = unitOfWork.GetRepository<Tournament>();
        }
    }
}
