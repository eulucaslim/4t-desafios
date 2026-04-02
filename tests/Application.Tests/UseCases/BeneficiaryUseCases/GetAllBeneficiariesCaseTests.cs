using System.Collections;
using Application.UseCases.BeneficiaryUseCases;
using Domain.Entities;
using Domain.Repositories;
using Moq;

namespace Application.Tests.UseCases.BeneficiaryUseCases;

public sealed class GetAllBeneficiariesCaseTests
{
    // Repository
    private static readonly Mock<IBeneficiaryRepository> Repository = new Mock<IBeneficiaryRepository>();
    
    // UseCase
    private readonly GetAllBeneficiariesCase _useCase = new GetAllBeneficiariesCase(Repository.Object);
    
    
    [Fact]
    public async Task Handle_SuccessGetAllBeneficiaries()
    {
        // Arrange
        var beneficiaries = new List<Beneficiary>
        {
            new Beneficiary(),
            new Beneficiary(),
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