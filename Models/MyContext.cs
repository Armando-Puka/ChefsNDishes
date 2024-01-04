#pragma warning disable CS8618
using Microsoft.EntityFrameworkCore;
namespace ChefsNDishes.Models;

public class MyContext : DbContext 
{   

    public MyContext(DbContextOptions options) : base(options) { }      
    public DbSet<Chef> Chefs { get; set; }
    public DbSet<Dish> Dishes { get; set; }

    // protected override void OnModelCreating(ModelBuilder modelBuilder)
    // {
    //     modelBuilder.Entity<Dish>()
    //         .HasOne(d => d.Chef)
    //         .WithMany(c => c.Dishes)
    //         .HasForeignKey(d => d.ChefId);
    // }
}