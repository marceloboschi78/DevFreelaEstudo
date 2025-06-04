using DevFreela.Core.Entities;
using DevFreela.Core.Enums;

namespace DevFreela.UnitTests.Core
{
    public class ProjectTests
    {
        [Fact]
        public void ProjectIsCreated_Start_Success()
        {
            //Arrange
            var project = new Project("Projeto A", "Descricao do Projeto", 1, 2, 10000);

            //Act
            project.Start();

            //Assert
            Assert.Equal(ProjectStatusEnum.InProgress, project.Status);
            Assert.NotNull(project.StartedAt);
            Assert.True(project.Status == ProjectStatusEnum.InProgress);
        }

        [Fact]
        public void ProjectIsInvalidState_Start_ThrowsException()
        {
            //arrange
            var project = new Project("Projeto A", "Descricao do Projeto", 1, 2, 10000);
            project.Start(); // Mudar o estado do projeto para InProgress

            //act + Assert
            Action start = project.Start;

            var exception = Assert.Throws<InvalidOperationException>(start);
            Assert.Equal(Project.INVALID_STATE_MESSAGE, exception.Message);
        }

        [Fact]
        public void ProjectIsInvalidState_Complete_ThrowsException()
        {
            //Arrange
            var project = new Project("Projeto A", "Descricao do Projeto", 1, 2, 10000);
            project.Start(); // Mudar o estado do projeto para InProgress
            project.Complete(); // mudar o estado do projeto para Completed
            //Act
            Action complete = project.Complete;
            //Assert
            var exception = Assert.Throws<InvalidOperationException>(complete); 
            Assert.Equal(Project.INVALID_STATE_MESSAGE, exception.Message);
        }
        [Fact]
        public void ProjectIsInProgress_Complete_Success()
        {
            //Arrange
            var project = new Project("Projeto A", "Descricao do Projeto", 1, 2, 10000);
            project.Start(); // Mudar o estado do projeto para InProgress
            //Act
            project.Complete();
            //Assert
            Assert.Equal(ProjectStatusEnum.Completed, project.Status);
            Assert.NotNull(project.CompletedAt);
        }
        [Fact]
        public void ProjectPaymentPending_Complete_Success()
        {
            //Arrange
            var project = new Project("Projeto A", "Descricao do Projeto", 1, 2, 10000);
            project.Start(); // Mudar o estado do projeto para InProgress
            project.SetPaymentPending();
            //Act
            project.Complete();
            //Assert
            Assert.Equal(ProjectStatusEnum.Completed, project.Status);
            Assert.NotNull(project.CompletedAt);
        }
        [Fact]
        public void ProjectIsInProgress_PaymentPending_Success()
        {
            //Arrange
            var project = new Project("Projeto A", "Descricao do Projeto", 1, 2, 10000);
            project.Start(); // Mudar o estado do projeto para InProgress
            //Act
            project.SetPaymentPending();
            //Assert
            Assert.Equal(ProjectStatusEnum.PaymentPending, project.Status);
        }
        [Fact]
        public void ProjectIsInInvalidState_PaymentPending_ThrowException()
        {
            //Arrange
            var project = new Project("Projeto A", "Descricao do Projeto", 1, 2, 10000);
            project.Start(); // Mudar o estado do projeto para InProgress
            project.SetPaymentPending();
            //Act
            Action pending = project.SetPaymentPending;
            //Assert
            var exception = Assert.Throws<InvalidOperationException>(pending);
            Assert.Equal(Project.INVALID_STATE_MESSAGE, exception.Message);
        }
        [Fact]
        public void ProjectUpdated_Update_Success()
        {
            //Arrange
            var project = new Project("Projeto A", "Descricao do Projeto", 1, 2, 10000);
            //Act
            project.Update("Projeto A - Atualizado", "Descricao do Projeto Atualizado", 11000);
            //Assert
            Assert.Equal("Projeto A - Atualizado", project.Title);
            Assert.Equal("Descricao do Projeto Atualizado", project.Description);
            Assert.Equal(11000, project.TotalCost);

        }
        [Fact]
        public void ProjectIsInProgress_Cancel_Success()
        {
            //Arrange
            var project = new Project("Projeto A", "Descricao do Projeto", 1, 2, 10000);
            project.Start(); // Mudar o estado do projeto para InProgress
            //Act
            project.Cancel();
            //Assert
            Assert.Equal(ProjectStatusEnum.Cancelled, project.Status);
        }       
    }
}
