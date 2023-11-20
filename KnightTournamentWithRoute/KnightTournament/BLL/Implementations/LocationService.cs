using KnightTournament.DAL;
using KnightTournament.Models;

namespace KnightTournament.BLL.Implementations
{
    public class LocationService:GenericService<Location>
    {
        public LocationService(UnitOfWork unitOfWork)
        {
            _repository = unitOfWork.GetRepository<Location>();
        }
    }
}
