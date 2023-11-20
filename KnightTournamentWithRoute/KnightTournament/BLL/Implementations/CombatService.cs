using KnightTournament.DAL;
using KnightTournament.Models;

namespace KnightTournament.BLL.Implementations
{
    public class CombatService:GenericService<Combat>
    {
        public CombatService(UnitOfWork unitOfWork) 
        {
            _repository = unitOfWork.GetRepository<Combat>();
        }
    }
}
