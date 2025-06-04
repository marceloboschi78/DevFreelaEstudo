using DevFreela.Application.CQRS.Commands;
using DevFreela.Application.Notification;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using MediatR;
using Moq;
using NSubstitute;

namespace DevFreela.UnitTests.Application
{
    public class ProjectInsertHandlerTests
    {
        [Fact] // utilizando NSubstitute
        public async Task InputDataAreOk_Insert_Success_NSubstitute()
        {
            //arrange
            const int ID = 1;

            var repository = Substitute.For<IProjectRepository>();
            repository.Add(Arg.Any<Project>()).Returns(Task.FromResult(ID));

            var command = new ProjectInsertCommand
            {
                Title = "Projeto A",
                Description = "Descricao do Projeto",
                IdClient = 1,
                IdFreelancer = 2,
                TotalCost = 10000
            };

            var mediator = Substitute.For<IMediator>();
            mediator.Publish(Arg.Any<ProjectCreatedNotification>(), Arg.Any<CancellationToken>()).Returns(Task.CompletedTask);
            
            var handler = new ProjectInsertCommandHandler(mediator, repository);
            
            //act
            var result = await handler.Handle(command, new CancellationToken());
            
            //assert
            Assert.True(result.IsSuccess);
            Assert.Equal(ID, result.Data);
            await repository.Received(1).Add(Arg.Any<Project>());
            await mediator.Received(1).Publish(Arg.Any<ProjectCreatedNotification>(), Arg.Any<CancellationToken>());
        }
    }
}
