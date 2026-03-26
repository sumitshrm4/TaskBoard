using Microsoft.EntityFrameworkCore;
using TaskBoard.Api.Data;
using TaskBoard.Api.Repositories;
using TaskBoard.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("TaskBoardDb")); // swap to UseSqlServer for Azure SQL

// Generic repository registration
builder.Services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));

// Repositories
builder.Services.AddScoped<ITaskRepository, TaskRepository>();
builder.Services.AddScoped<IColumnRepository, ColumnRepository>();

// Services (register by interface)
builder.Services.AddScoped<ITaskService, TaskService>();
builder.Services.AddScoped<IColumnService, ColumnService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
