using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TooLargeException.Entities;

namespace TooLargeException.Services
{
    public static class DbSeeder
    {
        public static void SeedAll(this IApplicationBuilder app) 
        {
            using (var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope()) 
            {
                EFDataContext context = scope.ServiceProvider.GetRequiredService<EFDataContext>();
                SeedCars(context);
            }
        }

        private static void SeedCars(EFDataContext context) 
        {
            if (!context.Cars.Any()) 
            {
                context.Cars.AddRange(new List<AppCar> { 
                    new AppCar 
                    { 
                        Mark = "Mercedes",
                        Model = "GLA",
                        Fuel = "Бензин",
                        Year = 2021,
                        Capacity = 3.1F,
                        Image = "/uploads/gla.jpg"
                    },
                    new AppCar 
                    {
                        Mark = "Mercedes",
                        Model = "Gelandwagen",
                        Fuel = "Бензин",
                        Year = 2021,
                        Capacity = 5.0F,
                        Image = "/uploads/gelik.jpg"
                    },
                    new AppCar 
                    {
                        Mark = "Mercedes",
                        Model = "Vito",
                        Fuel = "Бензин",
                        Year = 2021,
                        Capacity = 3.5F,
                        Image = "/uploads/vito.jpg"
                    }
                });

                context.SaveChanges();
            }
        }
    }
}
