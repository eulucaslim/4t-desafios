using Application.DTOs.Requests;
using Application.UseCases.PlanUseCases;
using Domain.Entities;
using Domain.Enums;
using Domain.Exceptions;
using Domain.Repositories;
using Moq;

namespace Application.Tests.UseCases.PlanUseCases;

public sealed class DeletePlanCaseTests
{
    // Repository
    private static readonly Mock<IPlanRepository> Repository = new Mock<IPlanRepository>();
    
    // UseCase
    private readonly DeletePlanCase _useCase = new DeletePlanCase(Repository.Object);
    
    // Dtos e Entidade
    private static readonly PlanRequest Request = new PlanRequest("Plano Normal", "ANS-103333");
    private static readonly Plan Entity = new Plan(Request.Name, Request.AnsRegistrationCode);
    
    [Fact]
    public async Task Handle_SuccessDeletePlan()
    {
        // Arrange
        Repository.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(Entity);
        
        Repository.Setup(repo => repo.DeleteAsync(It.IsAny<Guid>()))
            .Returns(Task.CompletedTask);
        
        // Act
        await _useCase.Handle(Entity.Id);
        
        // Assert
        Repository.Verify(repo => repo.DeleteAsync(It.IsAny<Guid>()), Times.Once);
    }
    
    [Fact]
    public async Task Handle_NotExistsPlan()
    {
        // Arrange
        Repository.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(null as Plan);
        
        // Act
        var response = async () => await _useCase.Handle(Entity.Id);
        
        // Assert
        await Assert.ThrowsAsync<EntityNotFound>(response);
    }
    
}