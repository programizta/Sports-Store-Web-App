using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models
{
    public static class SeedClass
    {
        public static void EnsurePopulated(IApplicationBuilder app)
        {
            AppDbContext dbContext = app.ApplicationServices
                .GetRequiredService<AppDbContext>();

            dbContext.Database.Migrate();

            if (!dbContext.Products.Any())
            {
                dbContext.AddRange(
                    new Product
                    {
                        Name = "Kajak",
                        Description = "Mała łódka jednoosobowa",
                        Category = "Sporty wodne",
                        Price = 275
                    },
                    new Product
                    {
                        Name = "Kamizelka ratunkowa",
                        Description = "Kamizelka służąca do przeprowadzania wodnych akcji ratowniczych",
                        Category = "Sporty wodne",
                        Price = 48.95M
                    },
                    new Product
                    {
                        Name = "Rower górski",
                        Description = "Rower do jazdy po szlakach górskich",
                        Category = "Sporty rowerowe",
                        Price = 1999.99M
                    },
                    new Product
                    {
                        Name = "Piłka nożna",
                        Description = "Piłka do gry w piłkę nożną, rozmiar i waga zatwierdzona przez organizację FIFA",
                        Category = "Piłka nożna",
                        Price = 55.95M
                    },
                    new Product
                    {
                        Name = "Flagi narożne",
                        Description = "Flagi do określenia granic boiska",
                        Category = "Piłka nożna",
                        Price = 34.95M
                    },
                    new Product
                    {
                        Name = "Nagolenniki",
                        Description = "Ochraniacze na golenie do gry w piłkę nożną",
                        Category = "Piłka nożna",
                        Price = 69.95M
                    },
                    new Product
                    {
                        Name = "Strój pływacki",
                        Description = "Męski strój pływacki marki Speedo do pływania w maratonach na otwartych akwenach",
                        Category = "Pływanie",
                        Price = 199.99M
                    },
                    new Product
                    {
                        Name = "Oszczep treningowy",
                        Description = "Treningowy oszczep do przygotowań do zwodów w wieloboju lekkoatletycznym",
                        Category = "Lekkoatletyka",
                        Price = 99.95M
                    },
                    new Product
                    {
                        Name = "Trenażer stacjonarny",
                        Description = "Trenażer do przeprowadzania treningów stacjonarnych na własnym rowerze",
                        Category = "Sporty rowerowe",
                        Price = 79.90M
                    }
                    );
                dbContext.SaveChanges();
            }
        }
    }
}
