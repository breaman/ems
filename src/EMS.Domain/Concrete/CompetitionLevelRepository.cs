using EMS.Domain.Abstract;
using EMS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMS.Domain.Concrete
{
    public class CompetitionLevelRepository : RepositoryBase<CompetitionLevel>, ICompetitionLevelRepository
    {
        public CompetitionLevelRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }
    }
}
