using CaseManagementSystem.Models.Entities;
using Microsoft.EntityFrameworkCore;


namespace CaseManagementSystem.Contexts;

internal class DataContext : DbContext
{

    private readonly string _connectionString = @"";

    

    public DataContext()
    {
    }

    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }
 

    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
            optionsBuilder.UseSqlServer(_connectionString);
    }

    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }


    
    
    public DbSet<CommentEntity> Comments { get; set; } = null!;
    public DbSet<CustomerEntity> Customers { get; set; } = null!;
    public DbSet<CustomerServiceEmployeeEntity> CustomerServiceEmployees { get; set; } = null!;
    public DbSet<SituationEntity> Situations { get; set; } = null!;

}
