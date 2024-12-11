using Microsoft.EntityFrameworkCore;
using webapi.core.ioc;
using webapi.core.repository;
using webapi.domain.pizza;


namespace webapi.infraestructura
{
    
    [Injectable]
    public class RepositoryIngredient : IRepository<Ingredient, Guid>
    {
        private readonly PizzaDbContext _DataBase;
        public RepositoryIngredient(PizzaDbContext DataBase){
            _DataBase =DataBase;
        }
        DbSet<Ingredient> IDatabase<Ingredient>.Data => _DataBase.Ingredients;

        public void Commit()
        {
            _DataBase.SaveChanges();
        }
    }
}