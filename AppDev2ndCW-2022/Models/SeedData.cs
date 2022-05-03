using AppDev2ndCW_2022.Models;
using Microsoft.EntityFrameworkCore;
using AppDev2ndCW_2022.Models;

namespace AppDev2ndCW_2022.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new DataBaseContext(
                       serviceProvider.GetRequiredService<
                           DbContextOptions<DataBaseContext>>()))
            {
                if (context == null || context.Actor == null)
                {
                    throw new ArgumentNullException("Null RazorPagesMovieContext");
                }

                // Look for any movies.
                if (context.Actor.Any())
                {
                    return;   // DB has been seeded
                }

                context.Actor.AddRange(
                    new Actor
                    {
                        ActorNumber = 127863,
                        ActorSurname = "Poudyal",
                        ActorFirstName = "Simsim",
                    },
                    new Actor
                    {
                        ActorNumber = 1378964,
                        ActorSurname = "Bhandari",
                        ActorFirstName = "Rimrim",
                    },
                    new Actor
                    {
                        ActorNumber = 2389174,
                        ActorSurname = "Simsim",
                        ActorFirstName = "Galigali",
                    },
                    new Actor
                    {
                        ActorNumber = 1438790,
                        ActorSurname = "Huhu",
                        ActorFirstName = "Hehe",
                    }
                );
                //Seeder throwing exception
                //context.SaveChanges();
            }
        }
    }
}