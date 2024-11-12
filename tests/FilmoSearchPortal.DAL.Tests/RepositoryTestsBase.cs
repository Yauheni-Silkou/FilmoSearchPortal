using FilmoSearchPortal.DAL.Data;
using FilmoSearchPortal.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace FilmoSearchPortal.DAL.Tests;

public class RepositoryTestsBase
{
    protected DbContextOptions<ApplicationDbContext> Options;

    public RepositoryTestsBase()
    {
        Options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
    }

    protected ApplicationDbContext GetContextWithData()
    {
        var context = new ApplicationDbContext(Options);

        if (!context.Actors.Any() && !context.Films.Any() && !context.Reviews.Any())
        {
            SeedData(context);
        }

        return context;
    }

    private static void SeedData(ApplicationDbContext context)
    {
        var actor1 = new Actor { Id = 1, FirstName = "Robert", LastName = "Downey Jr." };
        var actor2 = new Actor { Id = 2, FirstName = "Chris", LastName = "Hemsworth" };

        var film1 = new Film { Id = 1, Title = "Iron Man" };
        var film2 = new Film { Id = 2, Title = "Thor" };

        actor1.Films.Add(film1);
        actor2.Films.Add(film2);

        var review1 = new Review { Id = 1, Title = "Amazing!", Description = "Loved it!", Stars = 5, FilmId = 1 };
        var review2 = new Review { Id = 2, Title = "Good", Description = "Pretty entertaining.", Stars = 4, FilmId = 2 };

        film1.Actors.Add(actor1);
        film2.Actors.Add(actor2);
        film1.Reviews.Add(review1);
        film2.Reviews.Add(review2);

        context.Actors.AddRange(actor1, actor2);
        context.Films.AddRange(film1, film2);
        context.Reviews.AddRange(review1, review2);

        context.SaveChanges();
    }
}