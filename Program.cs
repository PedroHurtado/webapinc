using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using webapi.core.IFeaturModule;
using webapi.core.ioc;
using webapi.infraestructura;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddInjectables();

builder.Services.AddDbContext<PizzaDbContext>(options =>
{
    options.UseInMemoryDatabase("pizzas");
});

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("*")
                            .WithMethods("OPTIONS", "GET", "POST", "PUT", "DELETE")
                            .SetPreflightMaxAge(TimeSpan.FromSeconds(2520));
                      });
});
builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    }).AddJwtBearer(o =>
    {
        var secret = builder.Configuration["Jwt:Key"];
        if (secret != null)
        {
            o.TokenValidationParameters = new TokenValidationParameters
            {
                ValidIssuer = builder.Configuration["Jwt:Issuer"],
                ValidAudience = builder.Configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret)),
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = false,
                ValidateIssuerSigningKey = true
            };
        }

    });
builder.Services.AddAuthorization();

var app = builder.Build();


app.UseHttpsRedirection();
app.UseCors(MyAllowSpecificOrigins);
app.MapFeatures();
app.MapControllers();
app.Run();




