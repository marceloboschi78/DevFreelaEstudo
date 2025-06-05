using DevFreela.Application.CQRS.Commands;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using FluentAssertions;
using Moq;
using NSubstitute;

namespace DevFreela.UnitTests.Application
{
    public class ProjectDeleteHandlerTests
    {
        [Fact]
        public async Task ProjectExists_Delete_Success_NSubstitute()
        {
            //arrange            
            var repository = Substitute.For<IProjectRepository>();

            var project = new Project("Projeto A", "Projeto A descrição", 1, 2, 10000);
            
            repository.GetById(Arg.Any<int>()).Returns(Task.FromResult((Project?) project));
            repository.Update(Arg.Any<Project>()).Returns(Task.CompletedTask);
            
            var handler = new ProjectDeleteCommandHandler(repository);

            var command = new ProjectDeleteCommand(1);

            //act
            var result = await handler.Handle(command, new CancellationToken());

            //assert
            Assert.True(result.IsSuccess);
            result.IsSuccess.Should().BeTrue(); //fluent Assertions

            await repository.Received(1).GetById(Arg.Any<int>());
            await repository.Received(1).Update(Arg.Any<Project>());
        }

        [Fact]
        public async Task ProjectDoesNotExist_Delete_Error_NSubstitute()
        {
            //arrange            
            var repository = Substitute.For<IProjectRepository>();
            
            repository.GetById(Arg.Any<int>()).Returns(Task.FromResult((Project?)null));
            
            var handler = new ProjectDeleteCommandHandler(repository);

            var command = new ProjectDeleteCommand(1);

            //act
            var result = await handler.Handle(command, new CancellationToken());

            //assert
            Assert.False(result.IsSuccess);
            Assert.True(result.Message == ProjectDeleteCommandHandler.PROJECT_NOT_FOUND_MESSAGE);

            result.IsSuccess.Should().BeFalse(); //fluent Assertions
            result.Message.Should().Be(ProjectDeleteCommandHandler.PROJECT_NOT_FOUND_MESSAGE);

            await repository.Received(1).GetById(Arg.Any<int>());
            await repository.DidNotReceive().Update(Arg.Any<Project>());
        }

        [Fact]
        public async Task ProjectExists_Delete_Success_Moq()
        {
            //arrange            
            var project = new Project("Projeto A", "Projeto A descrição", 1, 2, 10000);

            var repository = Mock.Of<IProjectRepository>(r =>
                r.GetById(It.IsAny<int>()) == Task.FromResult((Project)project) &&
                r.Update(It.IsAny<Project>()) == Task.CompletedTask);                        

            var handler = new ProjectDeleteCommandHandler(repository);

            var command = new ProjectDeleteCommand(1);

            //act
            var result = await handler.Handle(command, new CancellationToken());

            //assert
            Assert.True(result.IsSuccess);
            Mock.Get(repository).Verify(r => r.GetById(It.IsAny<int>()), Times.Once);
            Mock.Get(repository).Verify(r => r.Update(It.IsAny<Project>()), Times.Once);            
        }

        [Fact]
        public async Task ProjectDoesNotExist_Delete_Error_Moq()
        {
            //arrange            
            var repository = Mock.Of<IProjectRepository>(r =>
                r.GetById(It.IsAny<int>()) == Task.FromResult((Project?)null) &&
                r.Update(It.IsAny<Project>()) == Task.CompletedTask);

            var handler = new ProjectDeleteCommandHandler(repository);

            var command = new ProjectDeleteCommand(1);

            //act
            var result = await handler.Handle(command, new CancellationToken());

            //assert
            Assert.False(result.IsSuccess);
            Assert.True(result.Message == ProjectDeleteCommandHandler.PROJECT_NOT_FOUND_MESSAGE);
            Mock.Get(repository).Verify(r => r.GetById(It.IsAny<int>()), Times.Once);
            Mock.Get(repository).Verify(r => r.Update(It.IsAny<Project>()), Times.Never);            
        }
    }
}
