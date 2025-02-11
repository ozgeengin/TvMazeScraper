using Bogus;
using RTL.TvMazeScraper.Application.Models;
using RTL.TvMazeScraper.Domain.Entities;

namespace RTL.TvMazeScraper.Application.UnitTests.Generators
{
    public static class FakeDataGenerator
    {
        public static CastPersonDto GenerateCastPersonDto() => new Faker<CastPersonDto>()
                            .RuleFor(p => p.Id, f => f.Random.Number(min: 1))
                            .RuleFor(p => p.Name, f => f.Name.FirstName())
                            .RuleFor(p => p.Birthday, f => f.Date.Past(18, DateTime.Now))
                            .Generate();

        public static CastPersonEntity GenerateCastPersonEntity()
        {
            PersonEntity personEntity = GeneratePersonEntity();

            return new Faker<CastPersonEntity>()
                .RuleFor(i => i.Id, f => f.Random.Number(min: 1))
                .RuleFor(i => i.Person, f => personEntity)
                .RuleFor(i => i.PersonId, f => personEntity.Id)
                .Generate();
        }

        public static PersonEntity GeneratePersonEntity() => new Faker<PersonEntity>()
                            .RuleFor(p => p.Id, f => f.Random.Number(min: 1))
                            .RuleFor(p => p.Name, f => f.Name.FirstName())
                            .RuleFor(p => p.Birthday, f => f.Date.Past(18, DateTime.Now))
                            .Generate();

        public static ShowDto GenerateShowPersonDto() => new Faker<ShowDto>()
                                .RuleFor(s => s.Id, f => f.Random.Number(min: 1))
                                .RuleFor(s => s.Name, f => f.Name.FirstName())
                                .RuleFor(s => s.Cast, f =>
                                    [
                                        GenerateCastPersonDto(),
                                        GenerateCastPersonDto(),
                                        GenerateCastPersonDto()
                                    ])
                                .Generate();

        public static ShowEntity GenerateShowEntity() => new Faker<ShowEntity>()
                            .RuleFor(s => s.Id, f => f.Random.Number(min: 1))
                            .RuleFor(s => s.Name, f => f.Name.FirstName())
                            .RuleFor(s => s.Cast, f =>
                                [
                                    GenerateCastPersonEntity(),
                                    GenerateCastPersonEntity(),
                                    GenerateCastPersonEntity()
                                ])
                            .Generate();
    }
}
