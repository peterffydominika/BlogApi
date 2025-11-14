using System.ComponentModel.DataAnnotations;

namespace blog.Models
{
    public class Blog{
        [Key]
        public int Id{ get; set; }
        public string Name{ get; set; }
        public string Email{ get; set; }
        public DateTime RegTime{ get; set; }
        public DateTime ModTime{ get; set; }
    }
}
