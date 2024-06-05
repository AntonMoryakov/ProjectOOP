using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using OOP_PROJECT.Server.Models;


public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
    {
            Database.EnsureCreated();
    }
    public DbSet<EventsModel> Events { get; set; }
    public DbSet<UsersModel> Users { get; set; }

}