using Application.DTOs.Requests;
using Application.DTOs.Responses;
using Application.UseCases.PlanUseCases;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Repositories;
using Moq;

namespace Application.Tests.UseCases.PlanUseCases;

public sealed class CreatePlanCaseTests
{
    // Repository
    private static readonly Mock<IPlanRepository> Repository = new Mock<IPlanRepository>();
    
    // UseCase
    private readonly CreatePlanCase _useCase = new CreatePlanCase(Repository.Object);
    
    // Dtos e Entidade
    private static readonly PlanRequest Request = new PlanRequest("Plano Normal", "ANS-103333");
    private static readonly Plan Entity = new Plan(Request.Name, Request.AnsRegistrationCode);
    private static readonly PlanResponse Response = new PlanResponse(Entity.Id, Entity.Name, Entity.AnsRegistrationCode);
    
    private static readonly PlanRequest RequestError = new PlanRequest(
        "Plano Prata",
        "ANS-1212313"
    );
    
    [Fact]
    public async Task Handle_SuccessForCreatePlanCase()
    {
        // Arrange
        Repository.Setup(repo => repo.GetByAnsCode(Request.AnsRegistrationCode))
            .ReturnsAsync(null as Plan);
        
        Repository.Setup(repo => repo.CreateAsync(It.IsAny<Plan>()))
            .ReturnsAsync(Entity);
        
        // Act
        var result = await _useCase.Handle(Request);
        
        // Assert
        Assert.Equal(Response, result);
        Repository.Verify(repo => repo.CreateAsync(It.IsAny<Plan>()), Times.Once);
    }
    
    [Fact]
    public async Task Handle_WithAnsCodeInvalid()
    {
        // Arrange / Act
        var result = async () => await _useCase.Handle(RequestError);
        
        // Assert
        await Assert.ThrowsAsync<InvalidAnsCode>(result);
        
        Repository.Verify(repo => repo.GetByAnsCode(It.IsAny<string>()), Times.Once);
    }
    
    [Fact]
    public async Task Handle_WithErrorDuplicatedAnsCode()
    {
        // Arrange
        Repository.Setup(repo => repo.GetByAnsCode(It.IsAny<string>()))
            .ReturnsAsync(Entity);
        
        // Act
        var result = async () => await _useCase.Handle(Request);
        
        // Assert
        await Assert.ThrowsAsync<EntityAlreadyExists>(result);
    }

    [Fact]
    public async Task Handle_WithAnsCodeValid()
    {
        // Arrange
        Repository.Setup(repo => repo.GetByAnsCode(It.IsAny<string>()))
            .ReturnsAsync(null as Plan);

        // Act
        var result = async () => await _useCase.Handle(RequestError);
        
        // Assert
        await Assert.ThrowsAsync<InvalidAnsCode>(result);
    }
}