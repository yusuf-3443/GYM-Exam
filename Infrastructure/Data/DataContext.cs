using Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class DataContext: IdentityDbContext
{
    public DataContext(DbContextOptions<DataContext> options) :base(options)
    {
        
    }
    public DbSet<User> Users { get; set; }
    public DbSet<WorkOut> Workouts { get; set; }
    public DbSet<Trainer> Trainers { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<Membership> Memberships { get; set; }
    public DbSet<ClassSchedule> ClassShedules { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}
