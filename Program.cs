using Microsoft.EntityFrameworkCore;
using webapi.core.IFeaturModule;
using webapi.core.ioc;
using webapi.infraestructura;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddInjectables();

builder.Services.AddDbContext<PizzaDbContext>(options=>{
    options.UseInMemoryDatabase("pizzas");
});



var app = builder.Build();


app.UseHttpsRedirection();

app.MapFeatures();
app.MapControllers();
app.Run();




