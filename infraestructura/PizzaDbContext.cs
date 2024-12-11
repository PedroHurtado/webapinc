using Microsoft.EntityFrameworkCore;
using webapi.domain.pizza;
using webapi.infraestructura.persistence;
namespace webapi.infraestructura;

public class PizzaDbContext:DbContext{   
    public DbSet<Pizza> Pizzas => Set<Pizza>();
    public DbSet<Ingredient> Ingredients => Set<Ingredient>();
    public PizzaDbContext(DbContextOptions<PizzaDbContext> options):base(options){
        /*Pizzas.Add()
        Pizzas.Remove()
        Pizzas.Update()*/

        //this.SaveChanges() UOW
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new PizzaEntityfiguration());
        modelBuilder.ApplyConfiguration(new IngredientEntityConfiguration());
    }
}