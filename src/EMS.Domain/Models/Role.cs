using EMS.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace EMS.Domain.Models
{
    public class Role : IdentityRole<int>, IEntityBase
    {
    }
}
