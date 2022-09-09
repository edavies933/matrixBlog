namespace Matrix42SimpleBlogProject.Domain
{
    public class Blog
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<BlogPost>? Posts { get; set; }
    }
}