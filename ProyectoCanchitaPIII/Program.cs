using ProyectoCanchitaPIII;
using Microsoft.EntityFrameworkCore;
using Models.MODELS;
using Services.IServices;
using Services.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Inyectamos el contexto nuestro (clase que hereda de DbContext) con el metodo AddDbContext() y le pasamos por parametro la ConnectionString definida en el archivo appsettings.json
builder.Services.AddDbContext<CANCHITASGOLContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//Jwt configuration starts here
var jwtIssuer = builder.Configuration.GetSection("AppSettings:Issuer").Get<string>();
var jwtKey = builder.Configuration.GetSection("AppSettings:Key").Get<string>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtIssuer,
        ValidAudience = jwtIssuer,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
    };
});

builder.Services.AddAuthorization(option =>
{
    option.AddPolicy("Admin", policy => policy.RequireRole("1"));
    option.AddPolicy("Canchita", policy => policy.RequireRole("2"));
    option.AddPolicy("Usuario", policy => policy.RequireRole("3"));
});
//Jwt configuration ends here

// Hacemos la inyeccion de dependencia de nuestros servicios mediante una clase middleware "CompositeRoot"
CompositeRoot.DependencyInjection(builder);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
