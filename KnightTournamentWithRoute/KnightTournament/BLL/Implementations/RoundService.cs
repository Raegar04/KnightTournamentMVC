using KnightTournament.DAL;
using KnightTournament.Models;

namespace KnightTournament.BLL.Implementations
{
    public class RoundService:GenericService<Round>
    {
        public RoundService(UnitOfWork unitOfWork)
        {
            _repository = unitOfWork.GetRepository<Round>();
        }
    }
}
