using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SportsStore.Models;
using System;

namespace SportsStore
{
    public class Startup
    {
        // obiekt implementujący ten interfejs pozwala na dostęp do danych z pliku
        // konfiguracyjnego appsettings.json
        public IConfiguration Configuration { get; }

        // wczytanie ustawień konfiguracyjnych z pliku appsettings.json
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            // każde wywołanie metody interfejsu IProductRepository tworzy nowy obiekt
            // klasy SqlProductRepository
            services.AddTransient<IProductRepository, SqlProductRepository>();

            // rejestracja serwisu odpowiedzialnego za wzorzec MVC
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            // środowisko producyjne
            if (env.IsDevelopment())
            {
                // uruchomienie widoków w celu diagnozy wyjątków
                app.UseDeveloperExceptionPage();

                // uruchomienie obsługi plików statycznych
                app.UseStaticFiles();

                // włączenie obsługi wzorca MVC
                app.UseMvc(routes =>
                {
                    routes.MapRoute(
                        name: null,
                        template: "{category}/Strona{productPage:int}",
                        defaults: new { Controller = "Product", Action = "List" });

                    routes.MapRoute(
                        name: null,
                        template: "Strona{productPage:int}",
                        defaults: new { Controller = "Product", Action = "List", productPage = 1 });

                    routes.MapRoute(
                        name: null,
                        template: "{category}",
                        defaults: new { Controller = "Product", Action = "List", productPage = 1 });

                    routes.MapRoute(
                        name: null,
                        template: "",
                        defaults: new { Controller = "Product", Action = "List", productPage = 1 });

                    routes.MapRoute(name: null, template: "{controller}/{action}/{id?}");
                });

                // wypełnienie bazy danych przykładowymi danymi
                SeedClass.EnsurePopulated(app);
            }
        }
    }
}
