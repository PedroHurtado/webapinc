using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using webapi.domain.pizza;
namespace webapi.infraestructura.persistence;

public class PizzaEntityfiguration : IEntityTypeConfiguration<Pizza>
{
    public void Configure(EntityTypeBuilder<Pizza> builder)
    {
        builder.ToTable("Pizza");

        builder.HasKey(x => x.Id);

        builder
            .HasMany(x => x.Ingredients)
            .WithMany();

        //PIZZA->INGREDIENT_PIZZA->INGREDIENTS
    }
}
