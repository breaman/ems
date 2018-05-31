using EMS.Web.Models;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMS.Web.Services
{
    public class EmsViewLocationExpander : IViewLocationExpander
    {
        private const string THEME_KEY = "theme";
        public IEnumerable<string> ExpandViewLocations(ViewLocationExpanderContext context, IEnumerable<string> viewLocations)
        {
            string theme = null;

            if (context.Values.TryGetValue(THEME_KEY, out theme))
            {
                viewLocations = new[]
                {
                    $"/Themes/{theme}/{{1}}/{{0}}.cshtml",
                    $"/Themes/{theme}/Shared/{{0}}.cshtml"
                }
                .Concat(viewLocations);
            }

            return viewLocations;
        }

        public void PopulateValues(ViewLocationExpanderContext context)
        {
            context.Values[THEME_KEY] = context.ActionContext.HttpContext.RequestServices.GetRequiredService<IOptions<AppSettings>>().Value.ThemeOptions.ThemeName;
        }
    }
}
