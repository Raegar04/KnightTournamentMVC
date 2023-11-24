using KnightTournament.DAL;
using KnightTournament.Extensions;
using KnightTournament.Helpers;
using KnightTournament.Models;
using System.Security.Claims;

namespace KnightTournament.BLL.Implementations
{
    public class CombatService:GenericService<Combat>
    {
        private readonly CombatKnightService _combatKnightService;
        private readonly RoundService _roundService;

        public CombatService(UnitOfWork unitOfWork, CombatKnightService combatKnightService, RoundService roundService) 
        {
            _repository = unitOfWork.GetRepository<Combat>();
            _combatKnightService = combatKnightService;
            _roundService = roundService;
        }

        public override async Task<Result<bool>> DeleteAsync(Guid id)
        {
            var combatKnightsResult = await _combatKnightService.GetAllAsync(combUser => combUser.CombatId == id);
            if (combatKnightsResult.IsSuccessful)
            {
                foreach (var item in combatKnightsResult.Data)
                {
                    var result = await _combatKnightService.DeleteAsync(item.Id);
                }
            }

            return await base.DeleteAsync(id);
        }

        public async Task<bool> IsHolder(Guid roundId, ClaimsPrincipal user) 
        {
            var userId = (user.GetUserIdFromPrincipal()).Data;
            var round = (await _roundService.GetByIdAsync(roundId)).Data;
            if (round.Tournament.HolderId == userId)
            {
                return true;
            }
            return false;
        }


    }
}
