using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMS.Domain.Models
{
    public class LogEntry : EntityBase
    {
        public DateTimeOffset DateTime { get; set; }
        public string Message { get; set; }
        public string Severity { get; set; }
    }
}
