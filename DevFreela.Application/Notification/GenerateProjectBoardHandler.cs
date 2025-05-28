using MediatR;

namespace DevFreela.Application.Notification
{
    public class GenerateProjectBoardHandler : INotificationHandler<ProjectCreatedNotification>
    {
        public Task Handle(ProjectCreatedNotification notification, CancellationToken cancellationToken)
        {
            Console.WriteLine($"Gerando o quadro do projeto {notification.Title} com custo total de {notification.TotalCost}.");
            
            return Task.CompletedTask;
        }        
    }
}
