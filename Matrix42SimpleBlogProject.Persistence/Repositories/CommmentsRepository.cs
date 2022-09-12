using Matrix42SimpleBlogProject.Application.Contracts.Persistence;
using Matrix42SimpleBlogProject.Domain.Entities;

namespace Matrix42SimpleBlogProject.Persistence.Repositories
{
    public class CommentsRepository : BaseRepository<Comment>, ICommentsRepository
    {

        public CommentsRepository(BlogProjectDbContext dbContext) : base(dbContext)
        {
        }

        public Task<List<Comment>> GetAllCommentsForBlogPostWithId(Guid blogPostId)
        {
            var blogPostsComments = DbContext.Comments.Where(b => b.BlogPostId.Equals(blogPostId)).ToList();

            return Task.FromResult(blogPostsComments);
        }
    }
}