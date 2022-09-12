using Matrix42SimpleBlogProject.Application.Contracts.Persistence;
using Matrix42SimpleBlogProject.Application.Features.Tag.Commands.CreateTag;
using Matrix42SimpleBlogProject.Application.Features.Tag.Commands.DeleteTag;
using Matrix42SimpleBlogProject.Application.Features.Tag.Queries.GetAllTags;
using Matrix42SimpleBlogProject.Application.Profile;
using Matrix42SimpleBlogProject.UnitTest.Mocks;

namespace Matrix42SimpleBlogProject.UnitTest.Tags
{
    public class TagsTest
    {
        private readonly IMapper mapper;
        private readonly Mock<IAsyncRepository<Domain.Entities.Tag>> mockTagRepository;

        public TagsTest()
        {
            mockTagRepository = RepositoryMocks.GetTagBaseRepository();
            var configurationProvider = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });
            mapper = configurationProvider.CreateMapper();
        }

        [Fact]
        public async Task Test_That_All_Comments_Can_Be_Retrieved_From_CommentRepository()
        {
            var createBlogCommandHandler = new GetTagListHandler(mapper, mockTagRepository.Object);

            await createBlogCommandHandler.Handle(new GetTagListQuery(), CancellationToken.None);
            var tags = await mockTagRepository.Object.ListAllAsync();

            tags.Should().HaveCount(2, "because total number of tags in mock tag repository is 2");
        }
        [Fact]
        public async Task Test_That_Valid_Tag_Is_Added_To_BlogRepository()
        {
            var createBlogCommandHandler = new CreateTagCommandHandler(mockTagRepository.Object, mapper,
                new Mock<ILogger<CreateTagCommandHandler>>().Object);

            await createBlogCommandHandler.Handle(new CreateTagCommand() { Name = "new tag" },
                CancellationToken.None);
            var allBlogs = await mockTagRepository.Object.ListAllAsync();

            allBlogs.Should().HaveCount(3, "because new tag was added to the tag repository successfully");
        }

        [Fact]
        public async Task Test_That_Tag_Is_Deleted_From_TagRepository()
        {
            var deleteBlogCommandHandler = new DeleteTagCommandHandler(mockTagRepository.Object,
                new Mock<ILogger<DeleteTagCommandHandler>>().Object);

            await deleteBlogCommandHandler.Handle(new DeleteTagCommand() { Id = RepositoryMocks.ItemToBeModifiedGuid },
                CancellationToken.None);
            var allBlogs = await mockTagRepository.Object.ListAllAsync();

            allBlogs.Should().HaveCount(1, "because one tag was deleted from tag repository successfully");
        }
    }
}
