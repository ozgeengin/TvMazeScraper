using Bogus;
using RTL.TvMazeScraper.Domain.Models;

namespace RTL.TvMazeScraper.Application.UnitTests.Helpers
{
    public static class DataGenerator
    {
        public static IEnumerable<ShowModel> GenerateShows(int count) =>
            GenerateShowFaker().Generate(count);

        private static Faker<ShowModel> GenerateShowFaker() => new Faker<ShowModel>()
                                .RuleFor(s => s.Id, f => f.Random.Number(min: 1))
                                .RuleFor(s => s.Name, f => f.Name.FirstName())
                                .RuleFor(s => s.Cast, (f, x) => GeneratePersonFaker().Generate(f.Random.Number(min: 1)));

        private static Faker<PersonModel> GeneratePersonFaker() => new Faker<PersonModel>()
                            .RuleFor(p => p.Id, f => f.Random.Number(min: 1))
                            .RuleFor(p => p.Name, f => f.Name.FirstName())
                            .RuleFor(p => p.Birthday, f => f.Date.Past(18, DateTime.Now));
    }
}
