using webapi.core.IFeaturModule;
using webapi.core.ioc;
using webapi.core.repository;
using webapi.domain.pizza;
using webapi.infraestructura;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddInjectables();

//builder.Services.AddProblemDetails();

//builder.Services.AddTransient

//cada vez que tu solicitas la interface hace un new de la clase

//builder.Services.AddScoped

//cada vez que se crea un request se crea un new de la clase
//y las interfaces están disponibles

//builder.Services.AddSingleton

//se crean las intancias de las clases una sola vez
//y las interfaces están disponibles como singleton

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapFeatures();
app.MapControllers();
app.Run();




