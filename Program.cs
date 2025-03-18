using Microsoft.EntityFrameworkCore;
using ShiftsLoggerAPI.Models;

var builder = WebApplication.CreateBuilder(args);
var connString = builder.Configuration.GetConnectionString("ShiftsLoggerDbConnection");
builder.Services.AddDbContext<ShiftsContext>(opt => opt.UseSqlServer(connString));
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
