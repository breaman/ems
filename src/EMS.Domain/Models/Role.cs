using EMS.Domain.Abstract;
using Microsoft.AspNetCore.Identity;

namespace EMS.Domain.Models
{
    public class Role : IdentityRole<int>, IEntityBase
    {}
}