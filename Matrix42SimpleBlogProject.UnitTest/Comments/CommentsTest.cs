using Matrix42SimpleBlogProject.Application.Contracts.Persistence;
using Matrix42SimpleBlogProject.Application.Features.Comment.Command.CreateComment;
using Matrix42SimpleBlogProject.Application.Features.Comment.Command.DeleteComment;
using Matrix42SimpleBlogProject.Application.Features.Comment.Queries.GetAllComments;
using Matrix42SimpleBlogProject.Application.Profile;
using Matrix42SimpleBlogProject.UnitTest.Mocks;

namespace Matrix42SimpleBlogProject.UnitTest.Comments
{
    public class CommentsTest
    {
        private readonly IMapper mapper;
        private readonly Mock<IAsyncRepository<Domain.Entities.Comment>> mockCommentRepository;
        private readonly Mock<IAsyncRepository<Domain.Entities.BlogPost>> mockBlogPostRepository;

        public CommentsTest()
        {
            mockCommentRepository = RepositoryMocks.GetCommentBaseRepository();
            mockBlogPostRepository = RepositoryMocks.GetBlogPostRepository();
            var configurationProvider = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });
            mapper = configurationProvider.CreateMapper();
        }

        [Fact]
        public async Task Test_That_All_Comments_Can_Be_Retrieved_From_CommentRepository()
        {
            var createBlogCommandHandler = new CommentListQueryHandler(mapper, mockCommentRepository.Object);

            await createBlogCommandHandler.Handle(new CommentListQuery(), CancellationToken.None);
            var comments = await mockCommentRepository.Object.ListAllAsync();

            comments.Should().HaveCount(2, "because total number of comments in mock comment repository is 2");
        }


        [Fact]
        public async Task Test_That_Valid_Comment_Is_Added_To_CommentRepository()
        {
            var createBlogCommandHandler = new CreateCommentCommandHandler(mockCommentRepository.Object, mapper,
                new Mock<ILogger<CreateCommentCommandHandler>>().Object,mockBlogPostRepository.Object);

            await createBlogCommandHandler.Handle(new CreateCommentCommand() { Content = "new Comment",BlogPostId = RepositoryMocks.ItemToBeModifiedGuid,UserId = Guid.NewGuid()},
                CancellationToken.None);
            var allBlogs = await mockCommentRepository.Object.ListAllAsync();

            allBlogs.Should().HaveCount(3, "because new comment was added to the comment repository successfully");
        }

        [Fact]
        public async Task Test_That_Comment_Is_Deleted_From_CommentRepository()
        {
            var deleteBlogCommandHandler = new DeleteCommentCommandHandler(mockCommentRepository.Object,
                new Mock<ILogger<DeleteCommentCommandHandler>>().Object);

            await deleteBlogCommandHandler.Handle(new DeleteCommentCommand() { Id = RepositoryMocks.ItemToBeModifiedGuid },
                CancellationToken.None);
            var allBlogs = await mockCommentRepository.Object.ListAllAsync();

            allBlogs.Should().HaveCount(1, "because one comment was deleted from comment repository successfully");
        }
    }
}
