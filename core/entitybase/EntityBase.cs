namespace webapi.core.entitybase
{
    public abstract class EntityBase    
    {    
        public Guid Id { get; init; }

        public EntityBase(Guid id) => Id = id;

        public override bool Equals(object? obj)
        {
            if (obj is EntityBase entityBase)
            {
                return entityBase.Id == Id;
            }
            return false;
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

    }
}