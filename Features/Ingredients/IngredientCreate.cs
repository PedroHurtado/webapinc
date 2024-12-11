using Microsoft.AspNetCore.Mvc;
using webapi.core.IFeaturModule;
using webapi.core.repository;
using webapi.domain.pizza;

namespace webapi.Features.Ingredients
{

    public readonly record struct IngredientRequest(string Name, decimal Cost)
    {

    }

    public readonly record struct IngredientResponse(Guid Id, string Name, decimal Cost)
    {

    }
    public class IngredientCreate : IFeatureModule
    {

        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/ingredients", (
                IAdd<Ingredient> repository,
                [FromBody] IngredientRequest request
            ) =>
            {
                //servicio->Antes de mapeal request a la entidad
                //validar el request
                var ingredient = Ingredient.Create(request.Name, request.Cost);
                
                //servicio
                repository.Add(ingredient);
                repository.Commit();

;               //servicio
                var response = new IngredientResponse(ingredient.Id, ingredient.Name, ingredient.Cost);
                //controlador
                return Results.Created("", response);
            });
        }
    }
}