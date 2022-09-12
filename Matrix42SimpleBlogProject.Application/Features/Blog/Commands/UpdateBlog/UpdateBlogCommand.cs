using MediatR;

namespace Matrix42SimpleBlogProject.Application.Features.Blogs.Commands.UpdateBlog
{
    public class UpdateBlogCommand : IRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
