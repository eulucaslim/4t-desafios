using Application.DTOs.Requests;
using Application.UseCases.BeneficiaryUseCases;
using Domain.Entities;
using Domain.Enums;
using Domain.Exceptions;
using Domain.Repositories;
using Moq;

namespace Application.Tests.UseCases.BeneficiaryUseCases;

public sealed class DeleteBeneficiaryCaseTests
{
    // Repository
    private static readonly Mock<IBeneficiaryRepository> Repository = new Mock<IBeneficiaryRepository>();
    
    // UseCase
    private readonly DeleteBeneficiaryCase _useCase = new DeleteBeneficiaryCase(Repository.Object);
    
    // Dtos e Entidade
    private static readonly Plan Plan = new Plan("Plano Bronze", "ANS-100001");
    
    private static readonly BeneficiaryRequest Request = new BeneficiaryRequest(
        "Ana Souza",
        "98765432100",
        DateOnly.Parse("1995-09-03"),
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
    public async Task Handle_SuccessDeleteBeneficiary()
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
    public async Task Handle_NotExistsBeneficiary()
    {
        // Arrange
        Repository.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(null as Beneficiary);
        
        // Act
        var response = async () => await _useCase.Handle(Entity.Id);
        
        // Assert
        await Assert.ThrowsAsync<EntityNotFound>(response);
    }
    
}