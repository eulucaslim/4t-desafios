using Api.Controllers;
using Application.DTOs.Requests;
using Application.DTOs.Responses;
using Application.UseCases.BeneficiaryUseCases;
using Domain.Entities;
using Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Api.Tests.Controllers;

public class BeneficiaryControllerTests
{
    // UseCases
    private readonly Mock<CreateBeneficiaryCase> _createMock;
    private readonly Mock<GetAllBeneficiariesCase> _getAllMock;
    private readonly Mock<GetBeneficiaryByIdCase> _getByIdMock;
    private readonly Mock<UpdateBeneficiaryCase> _updateMock;
    private readonly BeneficiaryController _controller;
    
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
    
    private static readonly BeneficiaryResponse Response = new BeneficiaryResponse(
        Entity.Id,
        Entity.FullName,
        Entity.Cpf,
        Entity.BirthDate,
        Entity.RegistrationDate,
        Entity.Status.ToString(),
        Plan.Id
        );
    
    public BeneficiaryControllerTests()
    {
        _createMock = new Mock<CreateBeneficiaryCase>();
        _getAllMock = new Mock<GetAllBeneficiariesCase>();
        _getByIdMock = new Mock<GetBeneficiaryByIdCase>();
        _updateMock = new Mock<UpdateBeneficiaryCase>();
        
        _controller = new BeneficiaryController(
            _createMock.Object, 
            _getAllMock.Object, 
            _getByIdMock.Object, 
            _updateMock.Object);
    }
    
    [Fact]
    public async Task Create_WhenUseCaseSucceeds_ReturnsCreated()
    {
        // Arrange
        _createMock.Setup(uc => uc.Handle(Request))
            .ReturnsAsync(Response);

        // Act
        var result = await _controller.Create(Request);

        // Assert
        var created = Assert.IsType<CreatedResult>(result.Result);
        Assert.Equal($"api/beneficiaries/{Response.Id}", created.Location);
        Assert.Equal(Response, created.Value);
    }

    [Fact]
    public async Task Create_ShouldCallHandleOnce()
    {
        _createMock.Setup(uc => uc.Handle(Request)).ReturnsAsync(Response);

        await _controller.Create(Request);

        _createMock.Verify(uc => uc.Handle(Request), Times.Once);
    }
}