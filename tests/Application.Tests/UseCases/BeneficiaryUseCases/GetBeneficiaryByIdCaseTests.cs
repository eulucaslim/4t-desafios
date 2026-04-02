using Application.UseCases.BeneficiaryUseCases;
using Domain.Entities;
using Domain.Enums;
using Domain.Exceptions;
using Domain.Repositories;
using Moq;

namespace Application.Tests.UseCases.BeneficiaryUseCases;

public sealed class GetBeneficiaryByIdCaseTests
{
    // Repository
    private static readonly Mock<IBeneficiaryRepository> Repository = new Mock<IBeneficiaryRepository>();
    
    // UseCase
    private readonly GetBeneficiaryByIdCase _useCase = new GetBeneficiaryByIdCase(Repository.Object);
    
    [Fact]
    public async Task Handle_SuccessGetBeneficiaryById()
    {
        // Arrange
        var plan = new Plan("Plano Bronze", "ANS-100001");
        var id = Guid.CreateVersion7();

        var beneficiary = new Beneficiary(
            "Lucas Lima",
            "98765432100",
            DateOnly.Parse("1995-09-03"),
            Status.Active,
            plan.Id
        )
        {
            Id = id
        };
        
        Repository.Setup(repo => repo.GetByIdAsync(id))
            .ReturnsAsync(beneficiary);
        
        // Act
        var result = await _useCase.Handle(id);
        
        // Assert
        Assert.Equal(result, beneficiary);
        Repository.Verify(repo => repo.GetByIdAsync(id), Times.Once);
    }
    
    [Fact]
    public async Task Handle_ErrorGetBeneficiary_BecauseNotExists()
    {
        // Arrange
        var id = Guid.CreateVersion7();
        
        Repository.Setup(repo => repo.GetByIdAsync(id))
            .ReturnsAsync(null as Beneficiary);
        
        // Act
        var result = async () => await _useCase.Handle(id);
        
        // Assert
        await Assert.ThrowsAsync<EntityNotFound>(result);
        Repository.Verify(repo => repo.GetByIdAsync(id), Times.Once);
    }
}