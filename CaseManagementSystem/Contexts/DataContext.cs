using CaseManagementSystem.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace CaseManagementSystem.Contexts
{
    internal class DataContext : DbContext

    {
        private readonly string _connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Dell\source\repos\Database-Assignment\CaseManagementSystem\Contexts\sql_db.mdf;Integrated Security=True;Connect Timeout=30";

        #region constructors

        public DataContext()
        {
        }
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        #endregion


        #region overrides
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured)
                optionsBuilder.UseSqlServer(_connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }


        #endregion


        #region entities



        #endregion


    }
}
