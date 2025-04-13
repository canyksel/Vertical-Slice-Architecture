using Example.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace Example.API.Database;

public class ExampleApplicationDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
}