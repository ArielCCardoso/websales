using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Globalization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using CCardoso.SalesWeb.Data;
using CCardoso.SalesWeb.Services;

namespace CCardoso.SalesWeb
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            services.Configure<RouteOptions>(options => { options.LowercaseUrls = true; });
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddDbContext<SalesWebContext>(options =>
            {
                options.UseNpgsql(Configuration.GetConnectionString("SalesWebContext"), builder => builder.MigrationsAssembly("SalesWeb"));

            }).AddLogging();

            services.AddScoped<SeedingService>();
            services.AddScoped<SellerService>();
            services.AddScoped<DepartmentService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, SeedingService seedingService)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                seedingService.Seed();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseRequestLocalization(_ =>
            {
                var c = new List<CultureInfo>();
                c.Add(CultureInfo.CurrentCulture);
                c.Add(CultureInfo.InvariantCulture);
                // _.DefaultRequestCulture = new RequestCulture(CultureInfo.CurrentCulture);
                //_.SupportedCultures = new List<CultureInfo> { CultureInfo.CurrentCulture };
                //_.SupportedUICultures = new List<CultureInfo> { CultureInfo.CurrentCulture };
                _.DefaultRequestCulture = new RequestCulture(CultureInfo.CurrentCulture);
                _.SupportedCultures = c;
                _.SupportedUICultures = c;
            });

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
