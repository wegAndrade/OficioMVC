using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OficioMVC.Libraries.Sessao;
using OficioMVC.Models;
using OficioMVC.Service;
using OficioMVC.Libraries.Login;
using OficioMVC.Service.Seed;
using Microsoft.Extensions.FileProviders;
using System.IO;
using OficioMVC.Libraries.Autenticacao;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;

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
            
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
           .AddCookie(options =>
           {
               options.LoginPath = "/Home/Login";
               options.SlidingExpiration = true;
               options.AccessDeniedPath = "/Home/AcessoNegado";
               
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
            services.AddScoped<LoginService>();
           services.AddScoped<FileService>();
            services.AddScoped<IAutenticacao, Autenticacao>();


           




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
            app.UseAuthentication();
            

            

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Login}/{id?}");
            });
        }
    }
}
