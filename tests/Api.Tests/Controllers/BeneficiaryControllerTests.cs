using Api.Controllers;
using Application.Abstractions.Interfaces.BeneficiaryUseCases;
using Application.DTOs.Requests;
using Application.DTOs.Responses;
using Application.UseCases.BeneficiaryUseCases;
using Domain.Entities;
using Domain.Enums;
using Domain.Repositories;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Api.Tests.Controllers;

public class BeneficiaryControllerTests
{
    // UseCases
    private readonly Mock<ICreateBeneficiaryCase> _createMock;
    private readonly Mock<IGetAllBeneficiariesCase> _getAllMock;
    private readonly Mock<IGetBeneficiaryByIdCase> _getByIdMock;
    private readonly Mock<IUpdateBeneficiaryCase> _updateMock;
    
    // Controller
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
    
    private static readonly BeneficiaryRequest RequestError = new BeneficiaryRequest(
        "Luan Santana",
        "12343123123",
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
    
    private static readonly Beneficiary EntityError = new Beneficiary(
        RequestError.FullName,
        RequestError.Cpf,
        RequestError.BirthDate,
        RequestError.Status,
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
    
    private static readonly BeneficiaryResponse ResponseError = new BeneficiaryResponse(
        EntityError.Id,
        EntityError.FullName,
        EntityError.Cpf,
        EntityError.BirthDate,
        EntityError.RegistrationDate,
        EntityError.Status.ToString(),
        Plan.Id
    );
    
    public BeneficiaryControllerTests()
    {
        _createMock = new Mock<ICreateBeneficiaryCase>();
        _getAllMock = new Mock<IGetAllBeneficiariesCase>();
        _getByIdMock = new Mock<IGetBeneficiaryByIdCase>();
        _updateMock = new Mock<IUpdateBeneficiaryCase>();
        
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
        _createMock
            .Setup(uc => uc.Handle(It.IsAny<BeneficiaryRequest>()))
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
        // Arrange
        _createMock.Setup(uc => uc.Handle(Request)).ReturnsAsync(Response);

        // Act
        await _controller.Create(Request);
        
        // Assert
        _createMock.Verify(uc => uc.Handle(Request), Times.Once);
    }
    
    [Fact]
    public async Task Create_CpfValidatorError()
    {
        // Arrange
        _createMock.Setup(uc => 
            uc.Handle(RequestError)).ReturnsAsync(ResponseError);

        // Act
        await _controller.Create(Request);
        
        // Assert
        _createMock.Verify(uc => uc.Handle(Request), Times.Once);
    }
}