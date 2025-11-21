using Microsoft.EntityFrameworkCore;

namespace blog.Models
{
    public class BlogDbContext : DbContext
    {
        public BlogDbContext()
        {
        }

        public BlogDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Blogger> blog { get; set; }
        public DbSet<Post> posts { get; set; }
      

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("server=localhost;database=blogger;user=root;password=");
        }
    }
}
