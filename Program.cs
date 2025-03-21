using Microsoft.EntityFrameworkCore;
using ShiftsLoggerAPI.Models;

var builder = WebApplication.CreateBuilder(args);
var connString = builder.Configuration.GetConnectionString("ShiftsLoggerDbConnection");
builder.Services.AddDbContext<ShiftsContext>(opt => opt.UseSqlServer(connString));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

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

app.MapPut("/shifts/{id}", async (int id, Shift inputShift, ShiftsContext db) =>
{
    var shift = await db.Shifts.FindAsync(id);
    if (shift == null) return Results.NotFound();

    shift.StartTime = inputShift.StartTime;
    shift.EndTime = inputShift.EndTime;
    shift.Date = inputShift.Date;
    shift.EmployeeId = inputShift.EmployeeId;

    await db.SaveChangesAsync();
    return Results.NoContent();
});

app.MapDelete("/shifts/{id}", async (int id, ShiftsContext db) =>
{
    var shift = await db.Shifts.FindAsync(id);
    if (shift == null) return Results.NotFound();
    db.Shifts.Remove(shift);
    await db.SaveChangesAsync();
    return Results.NoContent();
});

app.Run();