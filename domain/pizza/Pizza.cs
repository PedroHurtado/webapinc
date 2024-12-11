using System.Collections.ObjectModel;
using webapi.core.entitybase;

namespace webapi.domain.pizza;
public class Pizza : EntityBase
{

    private const decimal PROFIT = 1.2M;
    public string Name { get; private set; }
    public string Description { get; private set; }
    public string Url { get; private set; }
    public decimal Price => ingredients.Sum(x => x.Cost) * PROFIT;
    public ReadOnlyCollection<Ingredient> Ingredients => new([.. ingredients]);
    private ISet<Ingredient> ingredients = new HashSet<Ingredient>();
    
    protected Pizza(Guid id, string name, string description, string url) : base(id)
    {
        Name = name;
        Description = description;
        Url = url;       

    }
    public void Update(string name, string description, string url)
    {
        Name = name;
        Description = description;
        Url = url;
    }
    public void AddIngredient(Ingredient ingredient)
    {
        ingredients.Add(ingredient);
    }
    public void RemoveIngredient(Ingredient ingredient)
    {
        ingredients.Remove(ingredient);
    }

    public static Pizza Create(string name, string description, string url, IEnumerable<Ingredient> ingredients)
    {
        var pizza =  new Pizza(
                Guid.NewGuid(),
                name,
                description,
                url               
                );
        pizza.ingredients = ingredients.ToHashSet();
        return pizza;
    }
}