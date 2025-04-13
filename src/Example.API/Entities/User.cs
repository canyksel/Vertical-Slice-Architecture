using Example.API.Entities.EntityBase;
using Example.API.Entities.Enums;

namespace Example.API.Entities;

public class User : BaseEntity<int>
{
    public string FirstName { get; protected set; }
    public string LastName { get; protected set; }
    public int Age { get; protected set; }
    public string Email { get; protected set; }
    public Gender Gender { get; protected set; }
    public bool IsDeleted { get; protected set; }
    public DateTime CreateDateUtc { get; protected set; }
    public DateTime UpdateDateUtc { get; protected set; }
    public DateTime DeleteDateUtc { get; protected set; }

    public User()
    {
    }

    public User(string firstName, string lastName, int age, string email, Gender gender)
    {
        FirstName = firstName;
        LastName = lastName;
        Age = age;
        Email = email;
        Gender = gender;
        CreateDateUtc = DateTime.UtcNow;
    }

    public void UpdateUser(string firstName, string lastName, int age, string email, Gender gender)
    {
        FirstName = firstName;
        LastName = lastName;
        Age = age;
        Email = email;
        Gender = Gender;
        UpdateDateUtc = DateTime.UtcNow;
    }

    public void DeleteUser()
    {
        IsDeleted = true;
        DeleteDateUtc = DateTime.UtcNow;
    }
}