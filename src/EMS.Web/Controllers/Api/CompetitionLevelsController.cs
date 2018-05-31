using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EMS.Domain.Abstract;
using EMS.Domain.Models;
using EMS.Web.ViewModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EMS.Web.Controllers.Api
{
    [Route("api/[controller]")]
    public class CompetitionLevelsController : Controller
    {
        public ICompetitionLevelRepository CompetitionLevelRepository { get; }
        public CompetitionLevelsController(ICompetitionLevelRepository competitionLevelRepository)
        {
            CompetitionLevelRepository = competitionLevelRepository;
        }
        // GET api/values/5
        [HttpGet("{divisionId}/{genderId}")]
        [ResponseCache(NoStore = true)]
        public IEnumerable<CompetitionLevelViewModel> Get(int divisionId, int genderId)
        {
            return CompetitionLevelRepository.All.Where(cl => cl.DivisionId == divisionId && cl.GenderId == genderId).OrderBy(cl => cl.DisplayOrder).Select(cl => new CompetitionLevelViewModel { Id = cl.Id, Name = cl.Name }).ToList();
        }
    }
}
