using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMS.Web.Services
{
    public interface IEmailViewRenderer
    {
        Task<string> RenderAsync(string viewName, ViewDataDictionary viewData);
    }
}
