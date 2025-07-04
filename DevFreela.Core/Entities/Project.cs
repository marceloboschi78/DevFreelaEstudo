﻿using DevFreela.Core.Enums;

namespace DevFreela.Core.Entities
{
    public class Project : BaseEntity
    {
        public const string INVALID_STATE_MESSAGE = "The project is not in a valid state to perform this operation.";
        protected Project() //construtor vazio para o reflection do EFCore
        {

        }
        public Project(string title, string description, int idClient, int idFreelancer, decimal totalCost)
            : base()
        {
            Title = title;
            Description = description;
            IdClient = idClient;
            IdFreelancer = idFreelancer;
            TotalCost = totalCost;
            Status = ProjectStatusEnum.Created;
            Comments = [];
        }

        public string Title { get; private set; }
        public string Description { get; private set; }
        public int IdClient { get; private set; }
        public User Client { get; private set; }
        public int IdFreelancer { get; private set; }
        public User Freelancer { get; private set; }
        public decimal TotalCost { get; private set; }
        public DateTime? StartedAt { get; private set; }
        public DateTime? CompletedAt { get; private set; }
        public ProjectStatusEnum Status { get; private set; }
        public List<ProjectComment> Comments { get; private set; }


        public void Cancel()
        {
            if (Status == ProjectStatusEnum.InProgress || Status == ProjectStatusEnum.Suspended)
            {
                Status = ProjectStatusEnum.Cancelled;
            }
            else
            {
                throw new InvalidOperationException(INVALID_STATE_MESSAGE);
            }
        }

        public void Start()
        {
            if (Status != ProjectStatusEnum.Created)
            {
                throw new InvalidOperationException(INVALID_STATE_MESSAGE);
            }

            Status = ProjectStatusEnum.InProgress;
            StartedAt = DateTime.Now;
        }

        public void Complete()
        {
            if (Status == ProjectStatusEnum.PaymentPending || Status == ProjectStatusEnum.InProgress)
            {
                Status = ProjectStatusEnum.Completed;
                CompletedAt = DateTime.Now;
            }
            else
            {
                throw new InvalidOperationException(INVALID_STATE_MESSAGE);
            }
        }

        public void SetPaymentPending()
        {
            if (Status != ProjectStatusEnum.InProgress)
            {
                throw new InvalidOperationException(INVALID_STATE_MESSAGE);
            }
                Status = ProjectStatusEnum.PaymentPending;
        }

        public void Update(string title, string description, decimal totalCost)
        {
            Title = title;
            Description = description;
            TotalCost = totalCost;
        }
    }
}

