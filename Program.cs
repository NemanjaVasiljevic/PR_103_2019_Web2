using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PR_103_2019.Data;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<PR_103_2019Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("PR_103_2019Context") ?? throw new InvalidOperationException("Connection string 'PR_103_2019Context' not found.")));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
