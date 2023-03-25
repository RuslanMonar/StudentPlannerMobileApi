using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using StudentPlanner.Infrastructure;

using StudentPlanner.Application;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configurations = builder.Configuration;


services.AddControllers();
services.AddApplication(configurations);
services.AddInfrastructure(configurations);
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();


services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "StudentPlanner-issuer",
            ValidAudience = "StudentPlanner-audience",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("StudentPlanner-secret-key"))
        };
    });

var app = builder.Build();
app.UseAuthentication();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
