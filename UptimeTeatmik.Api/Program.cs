using Hangfire;
using Hangfire.Dashboard.BasicAuthorization;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using UptimeTeatmik.Api.Controllers;
using UptimeTeatmik.Application;
using UptimeTeatmik.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddSingleton<ProblemDetailsFactory, UptimeTeatmikProblemDetailsFactory>();
    builder.Services.AddInfrastructure(builder.Configuration);
    builder.Services.AddApplication();
    builder.Services.AddControllers();
    // builder.Services.AddEndpointsApiExplorer();
    // builder.Services.AddSwaggerGen();
}

// Configure the HTTP request pipeline.

var app = builder.Build();
{
    if (app.Environment.IsDevelopment())
    {
        // app.UseSwagger();
        // app.UseSwaggerUI();
    }

    app.UseHangfireDashboard("/hangfire", new DashboardOptions
    {
        Authorization = new[]
        {
            new BasicAuthAuthorizationFilter(new BasicAuthAuthorizationFilterOptions
            {
                RequireSsl = false,
                SslRedirect = false,
                LoginCaseSensitive = false,
                Users = new[]
                {
                    new BasicAuthAuthorizationUser
                    {
                        Login = "admin",
                        PasswordClear = "password"
                    }
                }

            })
        }
    });
    
    app.UseHttpsRedirection();
    app.MapControllers();

    app.Run();
}


