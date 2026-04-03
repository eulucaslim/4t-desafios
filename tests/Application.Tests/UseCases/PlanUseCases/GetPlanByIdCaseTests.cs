using Application.UseCases.PlanUseCases;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Repositories;
using Moq;

namespace Application.Tests.UseCases.PlanUseCases;

public sealed class GetPlanByIdCaseTests
{
    // Repository
    private static readonly Mock<IPlanRepository> Repository = new Mock<IPlanRepository>();
    
    // UseCase
    private readonly GetPlanByIdCase _useCase = new GetPlanByIdCase(Repository.Object);
    
    [Fact]
    public async Task Handle_SuccessGetPlanById()
    {
        // Arrange
        var id = Guid.CreateVersion7();

        var plan = new Plan(
            "Plano Bronze", 
            "ANS-100001"
        )
        {
            Id = id
        };
        
        Repository.Setup(repo => repo.GetByIdAsync(id))
            .ReturnsAsync(plan);
        
        // Act
        var result = await _useCase.Handle(id);
        
        // Assert
        Assert.Equal(result, plan);
        Repository.Verify(repo => repo.GetByIdAsync(id), Times.Once);
    }
    
    [Fact]
    public async Task Handle_ErrorGetPlan_BecauseNotExists()
    {
        // Arrange
        var id = Guid.CreateVersion7();
        
        Repository.Setup(repo => repo.GetByIdAsync(id))
            .ReturnsAsync(null as Plan);
        
        // Act
        var result = async () => await _useCase.Handle(id);
        
        // Assert
        await Assert.ThrowsAsync<EntityNotFound>(result);
        Repository.Verify(repo => repo.GetByIdAsync(id), Times.Once);
    }
}