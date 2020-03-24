using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using OficioMVC.Libraries.Sessao;
using OficioMVC.Models;
using OficioMVC.Service;
using OficioMVC.Libraries.Login;
using OficioMVC.Service.Seed;
using Microsoft.Extensions.FileProviders;
using System.IO;

namespace OficioMVC
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
            services.AddScoped<SeedingService>();
            services.AddHttpContextAccessor();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddDbContext<OficioMVCContext>(options =>
            options.UseMySql(Configuration.GetConnectionString("OficioMVCContext"), builder =>
            builder.MigrationsAssembly("OficioMVC")));
            services.AddScoped<Siga_profsService>();
            services.AddScoped<DocumentoService>();
            services.AddDistributedMemoryCache(); // Adds a default in-memory implementation of IDistributedCache
            services.AddSession();
            services.AddScoped<HashPass>();
            services.AddScoped<Sessao>();
            services.AddScoped<LoginUser>();
            services.AddSingleton<IFileProvider>(
                 new PhysicalFileProvider(
             Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")));





        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, SeedingService SeedingService)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                SeedingService.Seed();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseSession();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Login}/{id?}");
            });
        }
    }
}
