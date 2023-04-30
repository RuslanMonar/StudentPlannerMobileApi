using System.Reflection;
using System.Text;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using StudentPlanner.Infrastructure;
using StudentPlanner.Application;
using StudentPlanner.Shared;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configurations = builder.Configuration;


services.AddControllers();

services.AddApplication(configurations);
services.AddInfrastructure(configurations);
services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

services.AddEndpointsApiExplorer();
services.AddSwaggerGen();


services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = configurations["JwtTokenIssuer"],
            ValidAudience = configurations["JwtTokenAudience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configurations["JwtTokenKey"]))
        };
    });

// services.AddCors(options =>
// {
//     options.AddPolicy(name: "localhostOrigins",
//         policy =>
//         {
//             policy
//                 .WithOrigins("*")
//                 .AllowCredentials()
//                 .AllowAnyHeader()
//                 .AllowAnyMethod();
//         });
// });

services.AddCors(Opt =>
{
    Opt.AddPolicy("localhostOrigins", policy =>
    {
        policy.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin();
    });
});

var app = builder.Build();
app.UseAuthentication();

app.UseCors("localhostOrigins");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
