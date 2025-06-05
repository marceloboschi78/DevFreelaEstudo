using DevFreela.Application.CQRS.Commands;
using DevFreela.Application.Notification;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using FluentAssertions;
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

        [Fact] // utilizando Moq
        public async Task InputDataAreOk_Insert_Success_Moq()
        {
            //arrange
            const int ID = 1;

            //var mock = new Mock<IProjectRepository>();
            //mock.Setup(r => r.Add(It.IsAny<Project>())).ReturnsAsync(ID);
            //var repository = mock.Object;

            var repository = Mock.Of<IProjectRepository>(r => r.Add(It.IsAny<Project>()) == Task.FromResult(ID));

            var command = new ProjectInsertCommand
            {
                Title = "Projeto A",
                Description = "Descricao do Projeto",
                IdClient = 1,
                IdFreelancer = 2,
                TotalCost = 10000
            };

            //var mock2 = new Mock<IMediator>();
            //mock2.Setup(m => m.Publish(It.IsAny<ProjectCreatedNotification>(), It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);
            //var mediator = mock2.Object;    

            var mediator = Mock.Of<IMediator>(m => m.Publish(It.IsAny<ProjectCreatedNotification>(), It.IsAny<CancellationToken>()) == Task.CompletedTask);
            
            var handler = new ProjectInsertCommandHandler(mediator, repository);

            //act
            var result = await handler.Handle(command, new CancellationToken());

            //assert
            Assert.True(result.IsSuccess);
            result.IsSuccess.Should().BeTrue();//fluent assertions

            Assert.Equal(ID, result.Data);
            result.Data.Should().Be(ID);//fluent assertions

            //mock.Verify(r => r.Add(It.IsAny<Project>()), Times.Once);
            Mock.Get(repository).Verify(r => r.Add(It.IsAny<Project>()), Times.Once);
            Mock.Get(mediator).Verify(m => m.Publish(It.IsAny<ProjectCreatedNotification>(), It.IsAny<CancellationToken>()), Times.Once);

        }
    }
}
