using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WebApi;
using WebApi.Application.Services;
using WebApi.Domain.Commands.Handlers;
using WebApi.Domain.Commands.Requests;
using WebApi.Domain.Commands.Responses;
using WebApi.Domain.Interfaces;
using WebApi.Infra.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Injeção de dependência
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddTransient<ITokenService, TokenService>();
builder.Services.AddTransient<IHandler<LoginRequest, LoginResponse>, LoginHandler>();

// Configurar a autenticação
var key = Encoding.ASCII.GetBytes(Settings.PrivateKey);
builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(opt => // Configurar o comportamento do Token
{
    opt.RequireHttpsMetadata = false;
    opt.SaveToken = true;
    opt.TokenValidationParameters = new()
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

// Configurar a autorização
builder.Services.AddAuthorization(opt =>
{   
    // Configurar acesso por Roles
    opt.AddPolicy("User", policy => policy.RequireClaim("Role", "User"));
    opt.AddPolicy("Admin", policy => policy.RequireClaim("Role", "Admin"));

    // Configurar acesso por Policies
    opt.AddPolicy("SuperAdmin", policy =>
        policy.RequireAssertion(context =>
            context.User.Identity is not null &&
            context.User.Identity.IsAuthenticated &&
            context.User.Identity.Name == "Luis"));
});

// Adicionar o Cors a aplicação
builder.Services.AddCors();

// Configurar o Global Policies
builder.Services.AddControllers(config =>
{
    var policy = new AuthorizationPolicyBuilder()
                        .RequireAuthenticatedUser()
                        .Build();
    config.Filters.Add(new AuthorizeFilter(policy)); 
});

var app = builder.Build();

app.UseHttpsRedirection();

// Configurar o Cors
app.UseCors(opt => opt
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

// Utilizar as autenticações configuradas
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
