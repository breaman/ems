using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMS.Web.Models
{
    public class PaymentConfig
    {
        public string ServiceName { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string BaseUrl { get; set; }
    }
}
