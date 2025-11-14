namespace blog.Models.Dtos
{
    public class UpdateBlogDto
    {
        public string Name
        {
            get; set;
        }
        public string Email
        {
            get; set;
        }
        public DateTime RegTime
        {
            get; set;
        }
        public DateTime ModTime
        {
            get; set;
        }
    }
}
