using webapi.core.entitybase;

namespace webapi.domain.pizza;

public class Ingredient : EntityBase
{
    

    public string Name { get; private set; }
    public decimal Cost { get; private set; }
    protected Ingredient(Guid id, string name, decimal cost) : base(id)
    {
        Name = name;
        Cost = cost;
    }
    public void Update(string name, decimal cost)
    {
        Name = name;
        Cost = cost;
    }
    public static Ingredient Create(string name, decimal cost)
    {
        return new Ingredient(Guid.NewGuid(), name, cost);
    }

}