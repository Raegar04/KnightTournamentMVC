using KnightTournament.DAL;
using KnightTournament.Helpers;
using KnightTournament.Models;

namespace KnightTournament.BLL.Implementations
{
    public class CombatKnightService : GenericService<CombatsKnight>
    {
        public CombatKnightService(UnitOfWork unitOfWork)
        {
            _repository = unitOfWork.GetRepository<CombatsKnight>();
        }

        public async Task<Result<bool>> AddUserToCombats(Tournament tournament, Guid userId)
        {
            foreach (var round in tournament.Rounds)
            {
                foreach (var combat in round.Combats)
                {
                    await _repository.dbSet.AddAsync(new CombatsKnight() { CombatId = combat.Id, KnightId = userId });
                }
            }

            try
            {
                await _repository.context.SaveChangesAsync();
            }
            catch
            {
                return new Result<bool>(false, "Cannot apply to combat");
            }

            return new Result<bool>(true);
        }
    }
}
