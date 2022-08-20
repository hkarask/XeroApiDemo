namespace XeroApiDemo.Domain.Common;

public abstract class Entity
{
    public Guid Id { get; set; }

    public DateTime CreatedOnUtc { get; set; }

    public Entity()
    {
        CreatedOnUtc = DateTime.UtcNow;
    }
}
