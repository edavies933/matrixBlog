namespace Matrix42SimpleBlogProject.Application.Features.Blog.Queries.GetBlogDetail
{
    public class BlogDetailVm
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
