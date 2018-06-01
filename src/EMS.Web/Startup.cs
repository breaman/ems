using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using EMS.Domain.Abstract;
using EMS.Domain.Concrete;
using EMS.Domain.Models;
using EMS.Web.Models;
using EMS.Web.Services;
using IntelliTect.PaymentGateway.Concrete;
using IntelliTect.PaymentGateway.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyModel;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace EMS.Web
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            string emailService = Configuration.GetSection("AppSettings").GetValue<string>("Email:ServiceName");
            string paymentService = Configuration.GetSection("AppSettings").GetValue<string>("Payment:ServiceName");

            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
            
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<User, Role>(o =>
            {
                o.Password.RequireDigit = false;
                o.Password.RequiredLength = 6;
                o.Password.RequireLowercase = false;
                o.Password.RequireNonAlphanumeric = false;
                o.Password.RequireUppercase = false;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

            services.AddScoped<IUserClaimsPrincipalFactory<User>, EmsUserClaimsPrincipalFactory<User, Role>>();

            services.Configure<RazorViewEngineOptions>(options =>
            {
                options.ViewLocationExpanders.Add(new EmsViewLocationExpander());
            });
            
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            switch (emailService.ToLower())
            {
                case "elasticemail":
                    services.AddTransient<IEmailSender, ElasticEmailEmailSender>();
                    break;
                case "sendgrid":
                    services.AddTransient<IEmailSender, SendGridEmailSender>();
                    break;
                default:
                    break;
            }

            switch (paymentService.ToLower())
            {
                case "paypal":
                    services.AddTransient<IPaymentService, PayPalPaymentService>();
                    break;
                default:
                    break;
            }

            services.AddScoped<IEmailViewRenderer, EmailViewRenderer>();
            services.AddScoped<IDivisionRepository, DivisionRepository>();
            services.AddScoped<IGenderRepository, GenderRepository>();
            services.AddScoped<ICompetitionLevelRepository, CompetitionLevelRepository>();
            services.AddScoped<ITeamRepository, TeamRepository>();
            services.AddScoped<IParticipantRepository, ParticipantRepository>();
            services.AddScoped<ITransactionHistoryRepository, TransactionHistoryRepository>();
            services.AddScoped<ITournamentParameterRepository, TournamentParameterRepository>();
            services.AddScoped<IPromotionalCodeRepository, PromotionalCodeRepository>();

            var dependencyContext = DependencyContext.Default;
            var assemblies = dependencyContext.RuntimeLibraries.SelectMany(lib => lib.GetDefaultAssemblyNames(dependencyContext).Where(a => a.Name.Contains("EMS")).Select(Assembly.Load)).ToArray();
            services.AddAutoMapper(assemblies);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IOptions<AppSettings> appSettings,
                              IServiceProvider serviceProvider, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseStaticFiles();

            if (appSettings.Value.ThemeOptions.ThemeName.ToLower() == "see3slam")
            {
                app.Map("/.well-known/acme-challenge/f7zuskQNCT9DJm3UBVp2Aku0xvK4_JA2oojt1GGOcXo", (myApp) =>
                {
                    myApp.Run(async context =>
                    {
                        await context.Response.WriteAsync("f7zuskQNCT9DJm3UBVp2Aku0xvK4_JA2oojt1GGOcXo.P7i0MAfwh8h4Er9lLDatf5hwA9ifJwWhU4FFuCYO-xU");
                    });
                });

                app.UseMvc(routes =>
                {
                    routes.MapRoute(
                        name: "default",
                        template: "{controller}/{action}/{id?}",
                        defaults: new { controller = "Team", action = "Index" });
                });
            }
            else
            {
                app.UseMvcWithDefaultRoute();
            }

            InitializeData.Initialize(serviceProvider, loggerFactory, appSettings.Value.ThemeOptions.ThemeName.ToLower());
        }
    }
}
