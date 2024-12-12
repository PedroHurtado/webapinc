using Microsoft.EntityFrameworkCore;
using webapi.core.IFeaturModule;
using webapi.core.ioc;
using webapi.infraestructura;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddInjectables();

builder.Services.AddDbContext<PizzaDbContext>(options=>{
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


var app = builder.Build();


app.UseHttpsRedirection();
app.UseCors(MyAllowSpecificOrigins);
app.MapFeatures();
app.MapControllers();
app.Run();




