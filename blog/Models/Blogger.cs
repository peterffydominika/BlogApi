using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace blog.Models
{
    public class Blogger{
        [Key]
        public int Id{ get; set; }
        [Required]
        [Column(TypeName = "varchar(50)")]
        public string Name{ get; set; }

        [Required]
        [Column(TypeName = "text")]
        public string? Password{ get; set; }
        [Column(TypeName = "text")]
        public string Email{ get; set; }
        public DateTime RegTime{ get; set; } = DateTime.Now;
        public DateTime ModTime{ get; set; } = DateTime.Now;

        public virtual ICollection<Post> Posts { get; set; } = new List<Post>();
    }
}
