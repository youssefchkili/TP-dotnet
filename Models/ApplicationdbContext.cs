using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace MyFirstApp.Models
{
    public class ApplicationdbContext : DbContext
    {
        public ApplicationdbContext(DbContextOptions<ApplicationdbContext> options)
            : base(options)
        {
        }

        public DbSet<Movie>? movies { get; set; }
        public DbSet<Genre> genres { get; set; }
        public DbSet<Customer> customers { get; set; }
        public DbSet<MembershipType> membershipTypes { get; set; }
        public DbSet<AuditLog> auditLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed Data depuis le fichier JSON
            string movJson = System.IO.File.ReadAllText("Movies.json");
            List<Movie>? movies = JsonSerializer.Deserialize<List<Movie>>(movJson);

            // Seed to Movie
            if (movies != null)
            {
                foreach (Movie c in movies)
                {
                    modelBuilder.Entity<Movie>().HasData(c);
                }
            }
        }
    }
}