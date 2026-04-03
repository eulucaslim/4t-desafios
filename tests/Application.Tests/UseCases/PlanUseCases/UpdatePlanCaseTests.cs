using Application.DTOs.Requests;
using Application.DTOs.Responses;
using Application.Mappers;
using Application.UseCases.PlanUseCases;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Repositories;
using Moq;

namespace Application.Tests.UseCases.PlanUseCases;

public class UpdatePlanCaseTests
{
    // Repository
    private static readonly Mock<IPlanRepository> Repository = new Mock<IPlanRepository>();
    
    // Mapper
    private static readonly PlanMapper Mapper = new PlanMapper();
    
    // UseCase
    private readonly UpdatePlanCase _useCase = new UpdatePlanCase(
        Repository.Object, Mapper);
    
    // Dtos e Entidade
    private static readonly PlanRequest Request = new PlanRequest(
        "Plano Bronze",
        "ANS-100001");
    private static readonly Plan Entity = new Plan(Request.Name, Request.AnsRegistrationCode);
    
    private static readonly PlanRequest RequestError = new PlanRequest(
        "Plano Prata",
        "ANS-1212313"
    );
    
    [Fact]
    public async Task Handle_SuccessUpdatePlan()
    {
        // Arrange
        var request = new PlanRequest(
            "Plano Bronze Master",
            "ANS-100001"
        );

        var updatedEntity = new Plan(request.Name, request.AnsRegistrationCode);
        
        var response = new PlanResponse(
            updatedEntity.Id,
            updatedEntity.Name,
            updatedEntity.AnsRegistrationCode
            );
        
        Repository.Setup(repo => repo.GetByIdAsync(Entity.Id))
            .ReturnsAsync(Entity);
        
        Repository.Setup(repo => repo.UpdateAsync(Entity.Id, Entity))
            .ReturnsAsync(updatedEntity);
        
        // Act
        var result = await _useCase.Handle(Entity.Id, request);
        
        // Assert
        Assert.Equal(response, result);
        Repository.Verify(repo => repo.UpdateAsync(Entity.Id, Entity), Times.Once);

    }
    
    [Fact]
    public async Task Handle_WithErrorPlanNotFound()
    {
        // Arrange
        Repository.Setup(repo => repo.GetByIdAsync(Entity.Id))
            .ReturnsAsync(null as Plan);
        
        // Act
        var result = async () => await _useCase.Handle(Entity.Id, Request);
        
        // Assert
        await Assert.ThrowsAsync<EntityNotFound>(result);
        Repository.Verify(repo => repo.GetByIdAsync(Entity.Id), Times.Once);
    }
    
    [Fact]
    public async Task Handle_WithErrorInAnsCode()
    {
        // Arrange
        Repository.Setup(repo => repo.GetByIdAsync(Entity.Id))
            .ReturnsAsync(null as Plan);
        
        // Act
        var result = async () => await _useCase.Handle(Entity.Id, RequestError);
        
        // Assert
        await Assert.ThrowsAsync<InvalidAnsCode>(result);
    }
    
}