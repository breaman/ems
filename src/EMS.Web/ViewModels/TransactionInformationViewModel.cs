using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMS.Web.ViewModels
{
    public class TransactionInformationViewModel
    {
        public string Name { get; set; }
        public string TypeDescription { get; set; }
        public DateTime Date { get; set; }
        public string Type { get; set; }
        public int Id { get; set; }
    }
}
