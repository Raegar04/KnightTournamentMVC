using KnightTournament.BLL.Abstractions;
using KnightTournament.Extensions;
using KnightTournament.Helpers;
using KnightTournament.Models;
using KnightTournament.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections;

namespace KnightTournament.Controllers
{
    [Route("[controller]")]
    public class DisplaySqlController : Controller
    {
        private readonly ISqlService<Tournament> _sqlTournamentService;
        private readonly ISqlService<Round> _sqlRoundService;
        private readonly ISqlService<Combat> _sqlCombatService;

        public DisplaySqlController(ISqlService<Tournament> tournamentService, ISqlService<Round> roundService, ISqlService<Combat> combatService)
        {
            _sqlCombatService = combatService;
            _sqlRoundService = roundService;
            _sqlTournamentService = tournamentService;
        }

        [HttpGet("Display")]
        public async Task<IActionResult> Display()
        {
            SelectEntityViewModel selectEntityViewModel = new SelectEntityViewModel();
            ViewBag.Items = new List<Type>() { typeof(Tournament), typeof(Combat), typeof(Round) };

            if (TempData.ContainsKey("Query"))
            {
                var query = TempData["Query"].ToString();
                var type = TempData["Type"];
                switch (type)
                {
                    case "Tournament":
                        var tourResult = await _sqlTournamentService.GetByQuery(query);
                        if (tourResult.IsSuccessful)
                        {
                            ViewBag.Entities = tourResult.Data;
                            ViewBag.Type = typeof(Tournament);
                        }
                        break;
                    case "Combat":
                        var combResult = await _sqlCombatService.GetByQuery(query);
                        if (combResult.IsSuccessful)
                        {
                            ViewBag.Entities = combResult.Data;
                            ViewBag.Type = typeof(Combat);
                        }
                        break;
                    default:
                        var roundResult = await _sqlRoundService.GetByQuery(query);
                        if (roundResult.IsSuccessful)
                        {
                            ViewBag.Entities = roundResult.Data;
                            ViewBag.Type = typeof(Round);
                        }
                        break;
                }
                selectEntityViewModel.Query = query;
            }
            return View(selectEntityViewModel);
        }

        [HttpPost("Search")]
        public async Task<IActionResult> Search(SelectEntityViewModel selectEntityViewModel)
        {
            TempData["Query"] = selectEntityViewModel.Query;
            TempData["Type"] = selectEntityViewModel.Query.GetSelectedEntityType();
            return RedirectToAction("Display");


        }
    }
}
