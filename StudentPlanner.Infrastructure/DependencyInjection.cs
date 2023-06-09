﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StudentPlanner.Application.Interfaces.Infrastructure;
using StudentPlanner.Domain;
using StudentPlanner.Infrastructure.Data;

namespace StudentPlanner.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<StudentPlannerContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

        services.AddIdentity<User, IdentityRole<Guid>>()
            .AddEntityFrameworkStores<StudentPlannerContext>()
            .AddDefaultTokenProviders();

        services.AddScoped<IStudentPlannerContext, StudentPlannerContext>();

        return services;
    }
}