using Hangfire;
using Hangfire.Dashboard.BasicAuthorization;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using UptimeTeatmik.Api.Controllers;
using UptimeTeatmik.Application;
using UptimeTeatmik.Application.Common.Interfaces.BusinessRegisterService;
using UptimeTeatmik.Infrastructure;
using UptimeTeatmik.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddSingleton<ProblemDetailsFactory, UptimeTeatmikProblemDetailsFactory>();
    builder.Services.AddInfrastructure(builder.Configuration);
    builder.Services.AddApplication();
    builder.Services.AddControllers();
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
}

// Configure the HTTP request pipeline.

var app = builder.Build();
{
    using (var scope = app.Services.CreateScope())
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        var isDbCreated = dbContext.Database.EnsureCreated();
        if (isDbCreated) dbContext.Database.Migrate();
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
    RecurringJob.AddOrUpdate<IBusinessRegisterService>(
        "daily-business-update",
        job => job.RunBusinessUpdateJob(),
        Cron.Daily);
    
    app.UseHttpsRedirection();
    app.MapControllers();
    app.UseCors("origins");

    app.Run();
}


