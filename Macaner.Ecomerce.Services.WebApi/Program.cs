using AutoMapper;
using Macaner.Ecomerce.Application.DTO;
using Macaner.Ecomerce.Application.Interface;
using Macaner.Ecomerce.Application.Main;
using Macaner.Ecomerce.Domain.Core;
using Macaner.Ecomerce.Domain.Entity;
using Macaner.Ecomerce.Domain.Interface;
using Macaner.Ecomerce.Infrastructure.Data;
using Macaner.Ecomerce.Infrastructure.Interface;
using Macaner.Ecomerce.Infrastructure.Repository;
using Macaner.Ecomerce.Services.WebApi;
using Macaner.Ecomerce.Services.WebApi.Helpers;
using Macaner.Ecomerce.Transversal.Common;
using Macaner.Ecomerce.Transversal.Logging;
using Macaner.Ecomerce.Transversal.Mapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var appSettingsSection = builder.Configuration.GetSection("Config");

builder.Services.Configure<AppSettings>(appSettingsSection);
var appSettings = appSettingsSection.Get<AppSettings>();

builder.Services.AddControllers();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddAutoMapper(typeof(MappingsProfile));


//builder.Services.AddSingleton<IConfiguration>(Configuration);
builder.Services.AddSingleton<IConnectionFactory, ConnectionFactory>();
        
builder.Services.AddScoped<ICustomerApplication, CustomerApplication>();
builder.Services.AddScoped<ICustomerDomain, CustomerDomain>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IUsersApplication, UsersApplication>();
builder.Services.AddScoped<IUserDomain, UserDomain>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped(typeof(IAppLogger<>),typeof(LoggerAdapter<>));
//builder.Services.AddScoped<Macaner.Ecomerce.Transversal.Common.IAppLogger, LoggerAdapter>();

var key = Encoding.ASCII.GetBytes(appSettings.Secret);
var Issuer = appSettings.Issuer;
var Audience = appSettings.Audience;

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.Events = new JwtBearerEvents
    {
        OnTokenValidated = context =>
        {
            var userId = int.Parse(context.Principal.Identity.Name);
            return Task.CompletedTask;
        },

        OnAuthenticationFailed = context =>
        {
            if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
            {
                context.Response.Headers.Add("Token-Expired", "true");
            }
            return Task.CompletedTask;
        }
    };
    x.RequireHttpsMetadata = false;
    x.SaveToken = false;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = true,
        ValidIssuer = Issuer,
        ValidateAudience = true,
        ValidAudience = Audience,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero
    };
});


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "API de Ejemplo",
        Description = "Ejemplo de API con Swagger en ASP.NET Core",
        //Contact = new OpenApiContact
        //{
        //    Name = "Tu Nombre",
        //    Email = "tuemail@ejemplo.com",
        //    Url = new Uri("http://localhost:5058/api/customers")
        //}
    });
    // Set the comments path for the Swagger JSON and UI.
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    //c.IncludeXmlComments(xmlPath);

    //c.AddSecurityDefinition("Authorization", new ApiKeyScheme
    //{
    //    Description = "Authorization by API key.",
    //    In = "header",
    //    Type = "apiKey",
    //    Name = "Authorization"
    //});
    //c.AddSecurityRequirement(new OpenApiSecurityRequirement
    //  {
    //    {
    //      new OpenApiSecurityScheme
    //      {
    //        Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "bearer" }
    //      },
    //      new string[] {}
    //    }
    //  });

});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});


var app = builder.Build();

app.UseCors("AllowAll");
app.UseStaticFiles();

//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API de Ejemplo v1");
        c.RoutePrefix = "";  // Dejarlo vacío para usar la raíz
    });
//}

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseAuthentication();

app.Run();
