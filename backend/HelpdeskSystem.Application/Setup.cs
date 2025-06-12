using FluentValidation;
using HelpdeskSystem.Application.Common;
using HelpdeskSystem.Application.Middleware;
using HelpdeskSystem.Application.Services;
using HelpdeskSystem.Application.Validators;
using HelpdeskSystem.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace HelpdeskSystem.Application;

public static class Setup
{
    public static void AddApplicationLogic(this IHostApplicationBuilder builder, IConfiguration configuration)
    {
        
        var jwtOptions = configuration.GetSection("JwtSettings").Get<JwtOptions>();
        if (jwtOptions == null)
        {
            throw new InvalidOperationException("JWTSettings not found in configuration");
        }

        builder.Services.AddSingleton(TimeProvider.System);
        
        //builder.Services.AddProblemDetails();
        //builder.Services.AddExceptionHandler<CustomExceptionHandler>();
        builder.Services.AddScoped<ErrorHandlingMiddleware>();
        builder.Services.AddSingleton<IAuthorizationHandler, ActiveUserAuthorizationHandler>();
        
        builder.Services.AddSingleton(jwtOptions);

        builder.Services.AddValidatorsFromAssemblyContaining<PageQueryFilterDtoValidator>();

        var assembly = typeof(TemplateService).Assembly;
        var directory = Path.GetDirectoryName(assembly.Location)!;
        var templatesPath = Path.Combine(directory, "Templates");
        builder.Services.AddSingleton(new TemplateService(templatesPath));

        builder.Services.AddHttpClient();
        
        builder.Services.AddScoped<IUserContextService, UserContextService>();
        builder.Services.AddScoped<IAccountService, AccountService>();
        builder.Services.AddScoped<IChatBotService, ChatBotService>();
        builder.Services.AddScoped<ITicketService, TicketService>();
        builder.Services.AddScoped<IDataExportService, DataExportService>();
        builder.Services.AddScoped<IAdminUserService, AdminUserService>();
        builder.Services.AddScoped<ISendGridService, SendGridService>();
        builder.Services.AddScoped<IAdminTicketService, AdminTicketService>();
        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddScoped<ITicketMessageService, TicketMessageService>();
    }
}