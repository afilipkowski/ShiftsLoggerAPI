using Microsoft.EntityFrameworkCore;
using ShiftsLoggerAPI.Models;

var builder = WebApplication.CreateBuilder(args);
var connString = builder.Configuration.GetConnectionString("ShiftsLoggerDbConnection");
builder.Services.AddDbContext<ShiftsContext>(opt => opt.UseSqlServer(connString));
var app = builder.Build();

app.MapGet("/shifts", async (ShiftsContext db) =>
    await db.Shifts.ToListAsync());

app.MapGet("/shifts/employee={id}", async (int id, ShiftsContext db) =>
    await db.Shifts.Where(t => t.EmployeeId == id).ToListAsync());

app.MapPost("/shifts", async (Shift shift, ShiftsContext db) =>
{
    db.Shifts.Add(shift);
    await db.SaveChangesAsync();
    return Results.Created($"/shifts/{shift.Id}", shift);
});

app.Run();