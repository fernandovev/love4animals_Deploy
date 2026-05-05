using Love4AnimalsApi.Models;
using Microsoft.EntityFrameworkCore;

namespace Love4AnimalsApi.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    public DbSet<User> Users => Set<User>();
    public DbSet<Campaign> Campaigns => Set<Campaign>();
    public DbSet<Post> Posts => Set<Post>();
    public DbSet<Comment> Comments => Set<Comment>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(u => u.Id);
            entity.Property(u => u.Nombre).IsRequired();
            entity.Property(u => u.Email).IsRequired();
            entity.Property(u => u.Password).IsRequired();
            entity.Property(u => u.Rol).HasConversion<string>();
        });

        modelBuilder.Entity<Campaign>(entity =>
        {
            entity.HasKey(c => c.Id);
            entity.Property(c => c.Titulo).IsRequired();
            entity.Property(c => c.Estado).HasConversion<string>();
            entity.Property(c => c.Descripcion).IsRequired();
        });

        modelBuilder.Entity<Post>(entity =>
        {
            entity.HasKey(p => p.Id);
            entity.Property(p => p.Titulo).IsRequired();
            entity.Property(p => p.Imagen).IsRequired();
            entity.Property(p => p.Descripcion).IsRequired();
            entity.Property(p => p.Estado).HasConversion<string>();
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(c => c.Id);
            entity.Property(c => c.Texto).IsRequired();

            entity.HasOne<Post>()
                .WithMany()
                .HasForeignKey(c => c.PostId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}