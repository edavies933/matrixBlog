using Matrix42SimpleBlogProject.Application.Contracts.Persistence;
using Matrix42SimpleBlogProject.Application.Features.Blog.Commands.CreateBlog;
using Matrix42SimpleBlogProject.Application.Features.Blog.Commands.DeleteBlog;
using Matrix42SimpleBlogProject.Application.Features.Blog.Commands.UpdateBlog;
using Matrix42SimpleBlogProject.Application.Features.Blog.Queries.GetBlogsList;
using Matrix42SimpleBlogProject.Application.Features.Blogs.Commands.CreateBlog;
using Matrix42SimpleBlogProject.Application.Features.Blogs.Commands.DeleteBlog;
using Matrix42SimpleBlogProject.Application.Features.Blogs.Commands.UpdateBlog;
using Matrix42SimpleBlogProject.Application.Profile;
using Matrix42SimpleBlogProject.Domain.Entities;
using Matrix42SimpleBlogProject.UnitTest.Mocks;

namespace Matrix42SimpleBlogProject.UnitTest.Blogs
{
    public class BlogsTest
    {
        private readonly IMapper mapper;
        private readonly Mock<IAsyncRepository<Blog>> mockBlogRepository;

        public BlogsTest()
        {
            mockBlogRepository = RepositoryMocks.GetBlogRepository();
            var configurationProvider = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });
            mapper = configurationProvider.CreateMapper();
        }


        [Fact]
        public async Task Test_That_All_Blogs_Can_Be_Retrieved_From_BlogRepository()
        {
            var createBlogCommandHandler = new GetBlogsListHandler(mapper, mockBlogRepository.Object);

            await createBlogCommandHandler.Handle(new GetBlogsListQuery(),CancellationToken.None);
            var allBlogs = await mockBlogRepository.Object.ListAllAsync();

            allBlogs.Should().HaveCount(2, "because total number of blogs in mock blogs repository is 2");
        }

        [Fact]
        public async Task Test_That_Valid_Blog_Is_Added_To_BlogRepository()
        {
            var createBlogCommandHandler = new CreateBlogCommandHandler(mockBlogRepository.Object, mapper,
                new Mock<ILogger<CreateBlogCommandHandler>>().Object);

            await createBlogCommandHandler.Handle(new CreateBlogCommand() { Name = "new blog", Description = "new blog description" },
                CancellationToken.None);
            var allBlogs = await mockBlogRepository.Object.ListAllAsync();

            allBlogs.Should().HaveCount(3, "because new blog was added to the blog repository successfully");
        }

        [Fact]
        public async Task Test_That_Blog_Is_Deleted_From_BlogRepository()
        {
            var deleteBlogCommandHandler = new DeleteBlogCommandHandler(mockBlogRepository.Object,
                new Mock<ILogger<DeleteBlogCommandHandler>>().Object);

            await deleteBlogCommandHandler.Handle(new DeleteBlogCommand() { Id = RepositoryMocks.ItemToBeModifiedGuid },
                CancellationToken.None);
            var allBlogs = await mockBlogRepository.Object.ListAllAsync();

            allBlogs.Should().HaveCount(1, "because one blog was deleted from blog repository successfully");
        }

        [Fact]
        public async Task Test_That_Blog_Is_Updated_In_BlogRepository()
        {
            var deleteBlogCommandHandler = new UpdateBlogCommandHandler(mockBlogRepository.Object, mapper,
                new Mock<ILogger<UpdateBlogCommandHandler>>().Object);
            var updatedBlogCommand = new UpdateBlogCommand()
            {
                Id = RepositoryMocks.ItemToBeModifiedGuid,
                Name = "updated name",
                Description = "updated description"
            };

            await deleteBlogCommandHandler.Handle(updatedBlogCommand,
                CancellationToken.None);

            var updatedBlog = (await mockBlogRepository.Object.ListAllAsync()).First(b => b.Id.Equals(RepositoryMocks.ItemToBeModifiedGuid));

            updatedBlog.Id.Should().Be(updatedBlogCommand.Id);
            updatedBlog.Name.Should().Be(updatedBlogCommand.Name);
            updatedBlog.Description.Should().Be(updatedBlogCommand.Description);
        }

        [Fact]
        public async Task Test_That_Blog_Details_Can_Be_Retrieved_From_BlogRepository()
        {
            var getBlogDetailHandler = new GetBlogDetailHandler(mapper, mockBlogRepository.Object);

            var getBlogDetailQuery = new GetBlogDetailQuery()
            {
                BlogId = RepositoryMocks.ItemToBeModifiedGuid
            };

            var blogDetailVm = await getBlogDetailHandler.Handle(getBlogDetailQuery,
                 CancellationToken.None);


            blogDetailVm.Id.Should().Be(getBlogDetailQuery.BlogId);
            blogDetailVm.Name.Should().Be("blog 2");
            blogDetailVm.Description.Should().Be("blog 2 description");
        }
    }
}