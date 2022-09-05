using Balta.CourseManager.App.Domain.Courses;
using Flunt.Notifications;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Balta.CourseManager.App.Infrastructure;

public class DataContext : IdentityDbContext<IdentityUser>
{
    public DataContext(DbContextOptions options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        //Chamada de classe pai para que a modelagem seja adicionada no Identity
        base.OnModelCreating(builder);

        builder.Ignore<Notification>();
    }

    //Todos os tipos STRING, precisam ter acima de 100 Caracteres
    protected override void ConfigureConventions(ModelConfigurationBuilder configuration)
    {
        configuration.Properties<string>()
            .HaveMaxLength(100);
    }

    public DbSet<Course> Courses { get; set; }
    public DbSet<Learn> Learns { get; set; }
    public DbSet<Module> Modules { get; set; }
}