using Microsoft.EntityFrameworkCore;
using webapi.core.IFeaturModule;
using webapi.core.ioc;
using webapi.infraestructura;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddInjectables();

builder.Services.AddDbContext<PizzaDbContext>(options=>{
    options.UseInMemoryDatabase("pizzas");
});



var app = builder.Build();



if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapFeatures();
app.MapControllers();
app.Run();




