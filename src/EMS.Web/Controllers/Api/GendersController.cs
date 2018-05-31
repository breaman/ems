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
    public class GendersController : Controller
    {
        public IGenderRepository GenderRepository { get; }
        public GendersController(IGenderRepository genderRepository)
        {
            GenderRepository = genderRepository;
        }

        // GET api/values/5
        [HttpGet("{divisionId}")]
        [ResponseCache(NoStore = true)]
        public IEnumerable<GenderViewModel> Get(int divisionId)
        {
            return GenderRepository.All.Where(g => g.DivisionId == divisionId).OrderBy(g => g.DisplayOrder).Select(g => new GenderViewModel { Id = g.Id, Name = g.Name }).ToList();
        }
    }
}
