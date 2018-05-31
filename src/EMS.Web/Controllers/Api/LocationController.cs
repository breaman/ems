using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EMS.Web.Controllers.Api
{
    [Route("api/[controller]")]
    public class LocationController : Controller
    {
        [HttpGet()]
        [ResponseCache(NoStore = true)]
        public List<SelectListItem> GetStatesProvinces(string country)
        {
            List<SelectListItem> stateProvinceItems = new List<SelectListItem>();

            if (country == "US")
            {
                foreach (string state in EMS.Domain.Models.Constants.States)
                {
                    stateProvinceItems.Add(new SelectListItem()
                    {
                        Value = state,
                        Text = state
                    });
                }
            }
            else
            {
                foreach (string province in EMS.Domain.Models.Constants.Provinces)
                {
                    stateProvinceItems.Add(new SelectListItem()
                    {
                        Value = province,
                        Text = province
                    });
                }
            }

            return stateProvinceItems;
        }
    }
}
