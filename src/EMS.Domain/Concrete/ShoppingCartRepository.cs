using EMS.Domain.Abstract;
using EMS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMS.Domain.Concrete
{
    public class ShoppingCartRepository : RepositoryBase<ShoppingCart>, IShoppingCartRepository
    {
        public ShoppingCartRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }
    }
}
