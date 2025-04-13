using System.ComponentModel.DataAnnotations;

namespace Example.API.Entities.EntityBase;

public class BaseEntity<TKey> : IEntity<TKey>
{
    [Key]
    public TKey Id { get; protected set; }
}