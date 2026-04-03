using Application.UseCases.PlanUseCases;
using Domain.Entities;
using Domain.Repositories;
using Moq;

namespace Application.Tests.UseCases.PlanUseCases;

public sealed class GetAllBeneficiariesCaseTests
{
    // Repository
    private static readonly Mock<IPlanRepository> Repository = new Mock<IPlanRepository>();
    
    // UseCase
    private readonly GetAllPlansCase _useCase = new GetAllPlansCase(Repository.Object);
    
    [Fact]
    public async Task Handle_SuccessGetAllBeneficiaries()
    {
        // Arrange
        var beneficiaries = new List<Plan>
        {
            new Plan(),
            new Plan(),
        };
        Repository.Setup(repo => repo.GetAllAsync())
            .ReturnsAsync(beneficiaries);
        
        // Act
        var result = await _useCase.Handle();
        
        // Assert
        Assert.Equal(beneficiaries, result);
        Repository.Verify(repo => repo.GetAllAsync(), Times.Once);
    }
}