using Matrix42SimpleBlogProject.Application.Contracts.Persistence;
using Matrix42SimpleBlogProject.Domain.Common;
using Matrix42SimpleBlogProject.Domain.Entities;

namespace Matrix42SimpleBlogProject.UnitTest.Mocks;

public class RepositoryMocks
{
    public static Guid ItemToBeModifiedGuid = Guid.Parse("{B1888D2F-8003-45C1-92A4-EDC76A7C5DEE}");

    public static Mock<IAsyncRepository<Blog>> GetBlogRepository()
    {
        var userId = Guid.Parse("{A0888D2F-8003-45C1-92A4-EDC76A7C5DEE}");

        var blogs = new List<Blog>
        {
            new()
            {
                Id = Guid.NewGuid(),
                CreatedDate = DateTime.Now,
                Description = "blog 1 description",
                Name = "blog 1",
                UserId = userId
            },
            new()
            {
                Id = ItemToBeModifiedGuid,
                CreatedDate = DateTime.Now,
                Description = "blog 2 description",
                Name = "blog 2",
                UserId = userId
            }
        };

        return GetGenericRepositoryMock(blogs);
    }


    public static Mock<IAsyncRepository<Tag>> GetTagBaseRepository()
    {
        var tags = new List<Tag>
        {
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Tag 1"
            },
            new()
            {
                Id = ItemToBeModifiedGuid,
                Name = "Tag 2"
            }
        };
        return GetGenericRepositoryMock(tags);
    }

    public static Mock<ITagsRepository> GetTagRepositoryMock()
    {
        var repositoryMock = new Mock<ITagsRepository>();
        repositoryMock.Setup(b => b.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(
            (Guid id) => new Tag { Id = id });

        return repositoryMock;
    }

    public static Mock<ICommentsRepository> GetCommentRepositoryMock()
    {
        var repositoryMock = new Mock<ICommentsRepository>();
        repositoryMock.Setup(b => b.GetAllCommentsForBlogPostWithId(It.IsAny<Guid>())).ReturnsAsync(
            (Guid _) => new List<Comment>());
        return repositoryMock;
    }

    public static Mock<IAsyncRepository<BlogPostTag>> GetBlogPostTagRepository()
    {
        var blogPostTags = new List<BlogPostTag>
        {
            new()
            {
                Id = Guid.NewGuid(),
                BlogPostId = Guid.NewGuid(),
                TagId = Guid.NewGuid()
            },
            new()
            {
                Id = Guid.NewGuid(),
                BlogPostId = Guid.NewGuid(),
                TagId = Guid.NewGuid()
            }
        };
        return GetGenericRepositoryMock(blogPostTags);
    }

    public static Mock<IAsyncRepository<Comment>> GetCommentBaseRepository()
    {
        var comments = new List<Comment>
        {
            new()
            {
                Id = Guid.NewGuid(),
                Content = "Tag 1",
                CreatedDate = DateTime.Now,
                LastModifiedDate = DateTime.Now
            },
            new()
            {
                Id = ItemToBeModifiedGuid,
                Content = "Tag 2",
                CreatedDate = DateTime.Now,
                LastModifiedDate = DateTime.Now
            }
        };

        return GetGenericRepositoryMock(comments);
    }

    public static Mock<IAsyncRepository<BlogPost>> GetBlogPostRepository()
    {
        var blogPosts = new List<BlogPost>
        {
            new()
            {
                Id = Guid.NewGuid(),
                Name = "blog 1",
                Content = "blog post 1 content",
                CreatedDate = DateTime.Now,
                LastModifiedDate = DateTime.Now
            },
            new()
            {
                Name = "blog 2",
                Id = ItemToBeModifiedGuid,
                Content = "blog post 2 content",
                CreatedDate = DateTime.Now,
                LastModifiedDate = DateTime.Now
            }
        };

        return GetGenericRepositoryMock(blogPosts);
    }

    private static Mock<IAsyncRepository<T>> GetGenericRepositoryMock<T>(List<T> entities) where T : BaseEntity
    {
        var repositoryMock = new Mock<IAsyncRepository<T>>();
        repositoryMock.Setup(b => b.ListAllAsync()).ReturnsAsync(entities);
        repositoryMock.Setup(r => r.AddAsync(It.IsAny<T>())).ReturnsAsync(
            (T entity) =>
            {
                entities.Add(entity);
                return entity;
            });

        repositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(
            (Guid id) => entities.First(b => b.Id.Equals(id)));


        repositoryMock.Setup(r => r.DeleteAsync(It.IsAny<T>())).Returns<T>(
            d =>
            {
                var toDelete = entities.First(b => b.Id.Equals(d.Id));
                entities.Remove(toDelete);
                return Task.CompletedTask;
            });

        repositoryMock.Setup(r => r.UpdateAsync(It.IsAny<T>())).Returns<T>(
            d =>
            {
                var blogToDelete = entities.First(b => b.Id.Equals(d.Id));
                entities.Remove(blogToDelete);
                entities.Add(d);
                return Task.CompletedTask;
            });

        return repositoryMock;
    }
}

