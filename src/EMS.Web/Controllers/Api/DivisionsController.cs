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
    public class DivisionsController : Controller
    {
        public IDivisionRepository DivisionRepository { get; }
        public DivisionsController(IDivisionRepository divisionRepository)
        {
            DivisionRepository = divisionRepository;
        }
        // GET: api/values
        [HttpGet]
        [ResponseCache(NoStore = true)]
        public IEnumerable<DivisionViewModel> Get()
        {
            return DivisionRepository.All.Where(d => d.DisplayOrder > -1).OrderBy(d => d.DisplayOrder).Select(d => new DivisionViewModel { Id = d.Id, Name = d.Name }).ToList();
        }
    }
}
