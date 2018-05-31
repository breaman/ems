using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EMS.Domain.Abstract;
using AutoMapper;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using EMS.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EMS.Web.Controllers.Api
{
    [Authorize]
    [Route("api/[controller]")]
    public class TeamsController : Controller
    {
        public ITeamRepository TeamRepository { get; }
        public IMapper Mapper { get; }
        public TeamsController(ITeamRepository teamRepository, IMapper mapper)
        {
            TeamRepository = teamRepository;
            Mapper = mapper;
        }
        // GET: api/values
        [HttpGet]
        [ResponseCache(NoStore = true)]
        public async Task<IActionResult> Get()
        {
            var managerId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var teams = await TeamRepository.All.Include(t => t.Division).Include(t => t.Gender).Include(t => t.CompetitionLevel).Include(t => t.PromotionalCode).Include(t => t.Members).Where(t => t.ManagerId == managerId).ToListAsync();

            IEnumerable<TeamViewModel> mappedTeams = Mapper.Map<IEnumerable<TeamViewModel>>(teams);

            foreach (TeamViewModel team in mappedTeams)
            {
                team.Validate(teams.Single(t => t.Id == team.Id));
            }

            return Json(mappedTeams);
        }
    }
}
