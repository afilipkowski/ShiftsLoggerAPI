using Microsoft.EntityFrameworkCore;

namespace ShiftsLoggerAPI.Models;

public class ShiftsContext : DbContext
{
    public DbSet<Shift> Shifts { get; set; }
    public ShiftsContext(DbContextOptions<ShiftsContext> options) 
        : base(options) { }
}