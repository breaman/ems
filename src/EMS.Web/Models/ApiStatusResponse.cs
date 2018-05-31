using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMS.Web.Models
{
    public class ApiStatusResponse
    {
        public int StatusCode { get; set; }
        public List<string> Messages { get; set; }
    }
}
