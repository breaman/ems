using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMS.Web.ViewModels
{
    public class AccountTransactionViewModel
    {
        public string TransactionId { get; set; }
        public List<TransactionInformationViewModel> Details { get; set; }
    }
}
