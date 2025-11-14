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

        public DbSet<Blog> blog
        {
            get;set;
        }
      

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("server=localhost;database=blogger;user=root;password=");
        }
    }
}
