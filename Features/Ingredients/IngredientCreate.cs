using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using webapi.core.IFeaturModule;
using webapi.core.ioc;
using webapi.core.repository;
using webapi.domain.pizza;

namespace webapi.Features.Ingredients
{
    public class IngredientCreate : IFeatureModule
    {

        private readonly record struct Request(string Name, decimal Cost)
        {

        }

        private readonly record struct Response(Guid Id, string Name, decimal Cost)
        {

        }

        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/ingredients", (
                IService service,
                IValidator<Request> validator,
                [FromBody] Request request
            ) =>
            {
                
                






























































































































































































                
                return Results.Created("",
                    service.Handler(request)
                );
            });
        }

        [Injectable]
        private class Service : IService
        {

            private readonly IAdd<Ingredient> repository;
            public Service(IAdd<Ingredient> repository)
            {
                this.repository = repository; ;
            }
            public Response Handler(Request request)
            {
                //validacion del request
                var ingredient = Ingredient.Create(request.Name, request.Cost);
                repository.Add(ingredient);
                repository.Commit();
                return new Response(ingredient.Id, ingredient.Name, ingredient.Cost);
            }
        }
        private interface IService
        {
            Response Handler(Request request);
        }
        [Injectable]
        private class Validator: AbstractValidator<Request> {

        }
    }
}