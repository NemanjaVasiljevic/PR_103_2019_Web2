using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PR_103_2019.Data;
using PR_103_2019.Interfaces;
using PR_103_2019.MapProfile;
using PR_103_2019.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<PR_103_2019Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("PR_103_2019Context") ?? throw new InvalidOperationException("Connection string 'PR_103_2019Context' not found.")));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IArticleService, ArticleService>();


MapperConfiguration mapperConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new Map());
});

IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

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
