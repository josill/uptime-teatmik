using Hangfire;
using Hangfire.Dashboard.BasicAuthorization;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using UptimeTeatmik.Api.Controllers;
using UptimeTeatmik.Application;
using UptimeTeatmik.Application.Common.Interfaces.BusinessRegisterService;
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
    
    builder.Services.AddCors(options =>
    {
        options.AddPolicy(name: "origins",
            policy =>
            {
                policy.WithOrigins("http://localhost:5173")
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
            });
    });

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
    RecurringJob.AddOrUpdate<IBusinessRegisterService>(
        "daily-business-update",
        job => job.RunBusinessUpdateJob(),
        Cron.Daily);
    
    app.UseHttpsRedirection();
    app.MapControllers();
    app.UseCors("origins");

    app.Run();
}


