using Bogus;
using RTL.TvMazeScraper.Domain.Models;

namespace RTL.TvMazeScraper.Application.UnitTests.Helpers
{
    public static class DataGenerator
    {
        private static int PersonId = 1;
        private static int ShowId = 1;

        public static IEnumerable<ShowModel> GenerateShowModels(int count)
        {
            for (int i = 0; i < count; i++)
            {
                yield return GenerateShowModel();
            }
        }

        public static PersonModel GeneratePersonModel() => new Faker<PersonModel>()
                            .RuleFor(p => p.Id, f => PersonId++)
                            .RuleFor(p => p.Name, f => f.Name.FirstName())
                            .RuleFor(p => p.Birthday, f => f.Date.Past(18, DateTime.Now))
                            .Generate();

        public static ShowModel GenerateShowModel() => new Faker<ShowModel>()
                                .RuleFor(s => s.Id, f => ShowId++)
                                .RuleFor(s => s.Name, f => f.Name.FirstName())
                                .RuleFor(s => s.Cast, f =>
                                    [
                                        GeneratePersonModel(),
                                        GeneratePersonModel(),
                                        GeneratePersonModel()
                                    ])
                                .Generate();
    }
}
