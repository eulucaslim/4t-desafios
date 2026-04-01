using Application.Abstractions;
using Application.Abstractions.Interfaces.BeneficiaryUseCases;
using Application.Mappers;
using Application.UseCases.BeneficiaryUseCases;
using Application.UseCases.PlanUseCases;
using Domain.Repositories;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
        
        services.AddScoped<IBeneficiaryRepository, BeneficiaryRepository>();
        services.AddScoped<IPlanRepository, PlanRepository>();
        return services;
    }

    public static IServiceCollection AddUseCases(this IServiceCollection services)
    {
        services.AddScoped<ICreateBeneficiaryCase, CreateBeneficiaryCase>();
        services.AddScoped<IGetAllBeneficiariesCase, GetAllBeneficiariesCase>();
        services.AddScoped<IGetBeneficiaryByIdCase, GetBeneficiaryByIdCase>();
        services.AddScoped<IUpdateBeneficiaryCase, UpdateBeneficiaryCase>();
        services.AddScoped<IDeleteBeneficiaryCase, DeleteBeneficiaryCase>();
        
        services.AddScoped<CreatePlanCase>();
        services.AddScoped<GetAllPlansCase>();
        services.AddScoped<GetPlanByIdCase>();
        services.AddScoped<UpdatePlanCase>();
        services.AddScoped<DeletePlanCase>();
        return services;
    }

    public static IServiceCollection AddMappers(this IServiceCollection services)
    {
        services.AddScoped<BeneficiaryMapper>();
        services.AddScoped<PlanMapper>();
        return services;
    }
    
}