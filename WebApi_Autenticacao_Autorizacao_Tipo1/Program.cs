using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WebApi_Autenticacao_Autorizacao_Tipo1;

var builder = WebApplication.CreateBuilder(args);

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
    // Configurar as Policies e Claims
    opt.AddPolicy("User", policy => policy.RequireClaim("Store", "User"));
    opt.AddPolicy("Admin", policy => policy.RequireClaim("Store", "Admin"));
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
