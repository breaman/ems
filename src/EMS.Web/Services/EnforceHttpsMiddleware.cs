﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace EMS.Web.Services
{
    public class EnforceHttpsMiddleware
    {
        private readonly RequestDelegate _next;

        public EnforceHttpsMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            HttpRequest req = context.Request;
            if (req.IsHttps == false)
            {
                string url = "https://" + req.Host + req.Path + req.QueryString;
                context.Response.Redirect(url, permanent: true);
            }
            else
            {
                await _next(context);
            }
        }
    }

    public static class AppBuilderExtensions
    {
        public static IApplicationBuilder UseHttpsEnforcement(this IApplicationBuilder app)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }
            return app.UseMiddleware<EnforceHttpsMiddleware>();
        }
    }
}
