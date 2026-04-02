using Application.DTOs.Requests;
using Application.DTOs.Responses;
using Application.Mappers;
using Application.UseCases.BeneficiaryUseCases;
using Domain.Entities;
using Domain.Enums;
using Domain.Exceptions;
using Domain.Repositories;
using Moq;

namespace Application.Tests.UseCases.BeneficiaryUseCases;

public class UpdateBeneficiaryCaseTests
{
    // Repository
    private static readonly Mock<IBeneficiaryRepository> Repository = new Mock<IBeneficiaryRepository>();
    private static readonly Mock<IPlanRepository> PlanRepository = new Mock<IPlanRepository>();
    
    // Mapper
    private static readonly BeneficiaryMapper Mapper = new BeneficiaryMapper();
    
    // UseCase
    private readonly UpdateBeneficiaryCase _useCase = new UpdateBeneficiaryCase(
        Repository.Object, PlanRepository.Object, Mapper);
    
    // Dtos e Entidade
    private static readonly Plan Plan = new Plan("Plano Bronze", "ANS-100001");
    
    private static readonly BeneficiaryRequest Request = new BeneficiaryRequest(
        "Ana Souza",
        "98765432100",
        DateOnly.Parse("1995-09-03"),
        Status.Active,
        Plan.Id
    );
    
    private static readonly BeneficiaryRequest RequestError = new BeneficiaryRequest(
        "Luan Santana",
        "123456789011",
        DateOnly.Parse("2002-12-05"),
        Status.Active,
        Plan.Id
    );

    private static readonly Beneficiary Entity = new Beneficiary(
        Request.FullName,
        Request.Cpf,
        Request.BirthDate,
        Request.Status,
        Plan.Id);
    
    [Fact]
    public async Task Handle_SuccessUpdateBeneficiary()
    {
        // Arrange
        var newBeneficiary = new BeneficiaryRequest(
            "Ana Souza da Silva",
            "98765432100",
            DateOnly.Parse("2002-09-05"),
            Status.Active,
            Plan.Id
        );

        var updatedEntity = new Beneficiary(
            newBeneficiary.FullName,
            newBeneficiary.Cpf,
            newBeneficiary.BirthDate,
            newBeneficiary.Status,
            Plan.Id);
        
        var beneficiaryResponse = new BeneficiaryResponse(
            updatedEntity.Id,
            updatedEntity.FullName,
            updatedEntity.Cpf,
            updatedEntity.BirthDate,
            updatedEntity.RegistrationDate,
            updatedEntity.Status.ToString(),
            Plan.Id
        );
        
        Repository.Setup(repo => repo.GetByIdAsync(Entity.Id))
            .ReturnsAsync(Entity);
        
        PlanRepository.Setup(repo => repo.GetByIdAsync(Plan.Id))
            .ReturnsAsync(Plan);
        
        Repository.Setup(repo => repo.UpdateAsync(Entity.Id, Entity))
            .ReturnsAsync(updatedEntity);
        
        // Act
        var result = await _useCase.Handle(Entity.Id, newBeneficiary);
        
        // Assert
        Assert.Equal(beneficiaryResponse, result);
        Repository.Verify(repo => repo.UpdateAsync(Entity.Id, Entity), Times.Once);

    }
    
    [Fact]
    public async Task Handle_WithErrorBeneficiaryNotFound()
    {
        // Arrange
        Repository.Setup(repo => repo.GetByIdAsync(Entity.Id))
            .ReturnsAsync(null as Beneficiary);
        
        // Act
        var result = async () => await _useCase.Handle(Entity.Id, Request);
        
        // Assert
        await Assert.ThrowsAsync<EntityNotFound>(result);
        Repository.Verify(repo => repo.GetByIdAsync(Entity.Id), Times.Once);
    }
    
    [Fact]
    public async Task Handle_WithErrorInCpfLenght()
    {
        // Arrange
        Repository.Setup(repo => repo.GetByIdAsync(Entity.Id))
            .ReturnsAsync(Entity);
        
        // Act
        var result = async () => await _useCase.Handle(Entity.Id, RequestError);
        
        // Assert
        await Assert.ThrowsAsync<InvalidLengthException>(result);
    }
    
    [Fact]
    public async Task Handle_WithErrorForGetPlan()
    {
        // Arrange
        Repository.Setup(repo => repo.GetByIdAsync(Entity.Id))
            .ReturnsAsync(Entity);

        PlanRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(null as Plan);
        
        // Act
        var result = async () => await _useCase.Handle(Entity.Id, Request);
        
        // Assert
        await Assert.ThrowsAsync<EntityNotFound>(result);
        
        Repository.Verify(repo => repo.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
    }
}