using Matrix42SimpleBlogProject.Application.Contracts.Persistence;
using Matrix42SimpleBlogProject.Application.Features.BlogPost.Commands.CreateBlogPost;
using Matrix42SimpleBlogProject.Application.Features.BlogPost.Commands.DeleteBlogPost;
using Matrix42SimpleBlogProject.Application.Features.BlogPost.Commands.UpdateBlogPost;
using Matrix42SimpleBlogProject.Application.Features.BlogPost.Queries.GetBlogPostDetails;
using Matrix42SimpleBlogProject.Application.Features.BlogPost.Queries.GetBlogPostList;
using Matrix42SimpleBlogProject.Application.Profile;
using Matrix42SimpleBlogProject.Domain.Entities;
using Matrix42SimpleBlogProject.UnitTest.Mocks;

namespace Matrix42SimpleBlogProject.UnitTest.BlogPosts
{
    public class BlogPostsTest
    {
        private readonly IMapper mapper;
        private readonly Mock<IAsyncRepository<BlogPost>> mockBlogPostRepository;
        private readonly Mock<IAsyncRepository<Blog>> mockBlogRepository;
        private readonly Mock<IAsyncRepository<BlogPostTag>> mockBlogPostTagRepository;
        private readonly Mock<ICommentsRepository> mockCommentRepository;
        private readonly Mock<ITagsRepository> mockTagRepository;

        public BlogPostsTest()
        {
            mockBlogPostRepository = RepositoryMocks.GetBlogPostRepository();
            mockBlogRepository = RepositoryMocks.GetBlogRepository();
            mockBlogPostTagRepository = RepositoryMocks.GetBlogPostTagRepository();
            mockTagRepository = RepositoryMocks.GetTagRepositoryMock();
            mockCommentRepository = RepositoryMocks.GetCommentRepositoryMock();
            var configurationProvider = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });
            mapper = configurationProvider.CreateMapper();
        }

        [Fact]
        public async Task Test_That_All_Blogs_Can_Be_Retrieved_From_BlogRepository()
        {
            var createBlogCommandHandler = new GetBlogPostListHandler(mapper, mockBlogPostRepository.Object,mockBlogRepository.Object);

            await createBlogCommandHandler.Handle(new GetBlogPostListQuery(){BlogId = RepositoryMocks.ItemToBeModifiedGuid }, CancellationToken.None);
            var blogPosts = await mockBlogPostRepository.Object.ListAllAsync();

            blogPosts.Should().HaveCount(2, "because total number of blog post in mock blog post repository is 2");
        }

        [Fact]
        public async Task Test_That_Valid_BlogPost_Is_Added_To_BlogPostRepository()
        {
            var createBlogPostCommandHandler = new CreateBlogPostCommandHandler(mockBlogPostRepository.Object, mapper,
                new Mock<ILogger<CreateBlogPostCommandHandler>>().Object,mockBlogRepository.Object,mockBlogPostTagRepository.Object,mockTagRepository.Object);

            await createBlogPostCommandHandler.Handle(new CreateBlogPostCommand() { Name = "new blog post", Content = "new blog post description",BlogId = RepositoryMocks.ItemToBeModifiedGuid},
                CancellationToken.None);
            var allBlogs = await mockBlogPostRepository.Object.ListAllAsync();

            allBlogs.Should().HaveCount(3, "because new blog post was added to the blog post repository successfully");
        }

        [Fact]
        public async Task Test_That_BlogPost_Is_Deleted_From_BlogPostRepository()
        {
            var deleteBlogCommandHandler = new DeleteBlogPostCommandHandler(mockBlogPostRepository.Object,
                new Mock<ILogger<DeleteBlogPostCommandHandler>>().Object);

            await deleteBlogCommandHandler.Handle(new DeleteBlogPostCommand() { BlogPostId = RepositoryMocks.ItemToBeModifiedGuid },
                CancellationToken.None);
            var allBlogs = await mockBlogPostRepository.Object.ListAllAsync();

            allBlogs.Should().HaveCount(1, "because one blog post was deleted from blog post repository successfully");
        }

        [Fact]
        public async Task Test_That_BlogPost_Is_Updated_In_BlogPostRepository()
        {
            var deleteBlogCommandHandler = new UpdateBlogPostCommandHandler(mockBlogPostRepository.Object, mapper,
                new Mock<ILogger<UpdateBlogPostCommandHandler>>().Object);
            var updatedBlogCommand = new UpdateBlogPostCommand()
            {
                Id = RepositoryMocks.ItemToBeModifiedGuid,
                Name = "updated blog post name",
                Content = "updated blog post content"
            };

            await deleteBlogCommandHandler.Handle(updatedBlogCommand,
                CancellationToken.None);

            var updatedBlog = (await mockBlogPostRepository.Object.ListAllAsync()).First(b => b.Id.Equals(RepositoryMocks.ItemToBeModifiedGuid));

            updatedBlog.Id.Should().Be(updatedBlogCommand.Id);
            updatedBlog.Name.Should().Be(updatedBlogCommand.Name);
            updatedBlog.Content.Should().Be(updatedBlogCommand.Content);
        }

        [Fact]
        public async Task Test_That_Blog_Details_Can_Be_Retrieved_From_BlogRepository()
        {
            var getBlogDetailHandler = new GetBlogPostDetailHandler(mapper, mockBlogPostRepository.Object,mockCommentRepository.Object,
                mockTagRepository.Object,mockBlogPostTagRepository.Object);

            var getBlogDetailQuery = new GetBlogPostDetailQuery()
            {
                BlogPostId = RepositoryMocks.ItemToBeModifiedGuid
            };

            var blogDetailVm = await getBlogDetailHandler.Handle(getBlogDetailQuery,
                CancellationToken.None);


            blogDetailVm.Id.Should().Be(getBlogDetailQuery.BlogPostId);
            blogDetailVm.Name.Should().Be("blog 2");
            blogDetailVm.Content.Should().Be("blog post 2 content");
        }
    }
}
