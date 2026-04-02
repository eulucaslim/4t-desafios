using Application.DTOs.Requests;
using Application.DTOs.Responses;
using Application.UseCases.BeneficiaryUseCases;
using Domain.Entities;
using Domain.Enums;
using Domain.Exceptions;
using Domain.Repositories;
using Moq;

namespace Application.Tests.UseCases.BeneficiaryUseCases;

public sealed class CreateBeneficiaryCaseTests
{
    // Repository
    private static readonly Mock<IBeneficiaryRepository> Repository = new Mock<IBeneficiaryRepository>();
    private static readonly Mock<IPlanRepository> PlanRepository = new Mock<IPlanRepository>();
    
    
    // UseCase
    private readonly CreateBeneficiaryCase _useCase = new CreateBeneficiaryCase(
        Repository.Object,
        PlanRepository.Object);
    
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
    
    private static readonly BeneficiaryResponse Response = new BeneficiaryResponse(
        Entity.Id,
        Entity.FullName,
        Entity.Cpf,
        Entity.BirthDate,
        Entity.RegistrationDate,
        Entity.Status.ToString(),
        Plan.Id
    );

    [Fact]
    public async Task Handle_SuccessForCreateBeneficiaryCase()
    {
        // Arrange
        Repository.Setup(repo => repo.GetByCpfAsync(It.IsAny<string>()))
            .ReturnsAsync(null as Beneficiary);

        PlanRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(Plan);
        
        Repository.Setup(
            repo => repo.CreateAsync(It.IsAny<Beneficiary>()))
            .ReturnsAsync(Entity);
        
        // Act
        var result = await _useCase.Handle(Request);
        
        // Assert
        Assert.Equal(Response, result);
        
        Repository.Verify(repo => repo.CreateAsync(It.IsAny<Beneficiary>()), Times.Once);
    }
    
    [Fact]
    public async Task Handle_WithErrorForGetPlan()
    {
        // Arrange
        Repository.Setup(repo => repo.GetByCpfAsync(It.IsAny<string>()))
            .ReturnsAsync(null as Beneficiary);

        PlanRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(null as Plan);
        
        // Act
        var result = async () => await _useCase.Handle(Request);
        
        // Assert
        await Assert.ThrowsAsync<EntityNotFound>(result);
        
        Repository.Verify(repo => repo.GetByCpfAsync(It.IsAny<string>()), Times.Once);
        PlanRepository.Verify(repo => repo.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
    }
    
    [Fact]
    public async Task Handle_WithErrorDuplicatedCpf()
    {
        // Arrange
        Repository.Setup(repo => repo.GetByCpfAsync(It.IsAny<string>()))
            .ReturnsAsync(Entity);
        
        // Act
        var result = async () => await _useCase.Handle(Request);
        
        // Assert
        await Assert.ThrowsAsync<EntityAlreadyExists>(result);
    }
    
    [Fact]
    public async Task Handle_WithErrorInCpfLenght()
    {
        // Arrange / Act
        var result = async () => await _useCase.Handle(RequestError);
        
        // Assert
        await Assert.ThrowsAsync<InvalidLengthException>(result);
    }
    
    [Fact]
    public async Task Handle_WithErrorInCpfInvalid()
    {
        // Arrange
        
        var request = new BeneficiaryRequest(
            "Luan Santana",
            "11111111111",
            DateOnly.Parse("2002-12-05"),
            Status.Active,
            Plan.Id
        );
        
        // Act
        var result = async () => await _useCase.Handle(request);
        
        // Assert
        await Assert.ThrowsAsync<InvalidCpfException>(result);
    }
}