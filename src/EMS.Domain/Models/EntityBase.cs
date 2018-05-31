using EMS.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMS.Domain.Models
{
    public class EntityBase : IEntityBase
    {
        public int Id { get; set; }
    }
}
