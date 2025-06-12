using Bogus;
using DevFreela.Application.Commands;
using DevFreela.Core.Entities;

namespace DevFreela.UnitTests.Fakes;

    public class FakeDataHelper
    {
        private static readonly Faker _faker = new Faker();

        public static Project FakeProjectCreateV1()
        {
            return new Project(
             _faker.Lorem.Word(),
             _faker.Lorem.Sentence(),
             _faker.Random.Int(1, 100),
             _faker.Random.Int(1, 100),
             _faker.Random.Decimal(1000, 100000));
        }

        private static readonly Faker<Project> _projectFaker = new Faker<Project>()
            .CustomInstantiator(f => new Project(
                f.Lorem.Word(),
                f.Lorem.Sentence(),
                f.Random.Int(1, 100),
                f.Random.Int(1, 100),
                f.Random.Decimal(1000, 100000)));

        private static readonly Faker<ProjectInsertCommand>  _ProjectInsertCommandFaker = new Faker<ProjectInsertCommand>()
            .RuleFor(p => p.Title, f => f.Lorem.Word())
            .RuleFor(p => p.Description, f => f.Lorem.Sentence())
            .RuleFor(p => p.IdClient, f => f.Random.Int(1, 100))
            .RuleFor(p => p.IdFreelancer, f => f.Random.Int(1, 100))
            .RuleFor(p => p.TotalCost, f => f.Random.Decimal(1000, 100000));

        private static readonly Faker<Project> _projectF = new Faker<Project>()
            .RuleFor(p => p.Title, f => f.Lorem.Word())
            .RuleFor(p => p.Description, f => f.Lorem.Sentence())
            .RuleFor(p => p.IdClient, f => f.Random.Int(1, 100))
            .RuleFor(p => p.IdFreelancer, f => f.Random.Int(1, 100))
            .RuleFor(p => p.TotalCost, f => f.Random.Decimal(1000, 100000));

        public static Project FakeProjectCreateV2() => _projectFaker.Generate();
        public static List<Project> FakeProjects() => _projectFaker.Generate(10);

        public static ProjectDeleteCommand FakeProjectDeleteCommand(int id) => new ProjectDeleteCommand(id);
        
    }
