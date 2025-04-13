namespace Example.API.Entities.EntityBase;

public interface IEntity { }

public interface IEntity<TKey> : IEntity
{
    TKey Id { get; }
}