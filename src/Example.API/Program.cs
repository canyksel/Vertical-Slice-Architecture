using Example.API.Database;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
using Example.API.Features.Users;
using Example.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.CustomSchemaIds(type => type.FullName!.Replace("+", "."));
});

builder.Services.AddDbContext<ExampleApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var assembly = typeof(Program).Assembly;

builder.Services.AddMediatR(config =>
    config.RegisterServicesFromAssembly(assembly));

builder.Services.AddValidatorsFromAssembly(assembly);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapAllEndpoints();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
