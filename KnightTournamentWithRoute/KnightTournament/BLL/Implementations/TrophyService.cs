using KnightTournament.DAL;
using KnightTournament.Models;

namespace KnightTournament.BLL.Implementations
{
    public class TrophyService:GenericService<Trophy>
    {
        public TrophyService(UnitOfWork unitOfWork)
        {
            _repository = unitOfWork.GetRepository<Trophy>();
        }
    }
}
