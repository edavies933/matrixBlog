using Matrix42SimpleBlogProject.Application.Features.Blog.Queries.GetBlogsList;
using Matrix42SimpleBlogProject.Application.Features.BlogPost.Commands.CreateBlogPost;
using Matrix42SimpleBlogProject.Application.Features.BlogPost.Commands.DeleteBlogPost;
using Matrix42SimpleBlogProject.Application.Features.BlogPost.Commands.UpdateBlogPost;
using Matrix42SimpleBlogProject.Application.Features.BlogPost.Queries.GetBlogPostDetails;
using Matrix42SimpleBlogProject.Application.Features.BlogPost.Queries.GetBlogPostList;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Matrix42SimpleBlogProject.Api.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class BlogPostController : ControllerBase
{

    private readonly IMediator mediator;

    public BlogPostController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpGet("{blogId}", Name = "GetAllBlogPostByBlogId")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<List<BlogPostListVm>>> GetAllBlogPostByBlogId(Guid blogId)
    {
        var blogPostListVms = await mediator.Send(new GetBlogPostListQuery() { BlogId = blogId });
        return Ok(blogPostListVms);
    }

    [HttpGet("{daysAgo}", Name = "GetAllBlogPostsFromDaysAgo")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<List<BlogListVm>>> GetAllBlogPostsFromDaysAgo(int daysAgo)
    {
        var blogPostListVms = await mediator.Send(new GetBlogPostCreatedInPastDaysQuery { DaysAgo = daysAgo });
        return Ok(blogPostListVms);
    }

    [HttpGet("{id}", Name = "GetBlogPostById")]
    public async Task<ActionResult<BlogListVm>> GetBlogPostById(Guid id)
    {
        var getBlogPostDetailQuery = new GetBlogPostDetailQuery { BlogPostId = id };
        return Ok(await mediator.Send(getBlogPostDetailQuery));
    }

    [HttpPost(Name = "AddBlogPost")]
    public async Task<ActionResult<Guid>> Create([FromBody] CreateBlogPostCommand createBlogPostCommand)
    {
        //TODO authentication and authorization
        var response = await mediator.Send(createBlogPostCommand);
        return Ok(response);
    }

    [HttpPost(Name = "DeleteBlogPost")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> Delete([FromBody] DeleteBlogPostCommand deleteBlogPostCommand)
    {
        await mediator.Send(deleteBlogPostCommand);
        return NoContent();
    }

    [HttpPost(Name = "UpdateBlogPost")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> Update([FromBody] UpdateBlogPostCommand updateBlogPostCommand)
    {
        await mediator.Send(updateBlogPostCommand);
        return NoContent();
    }
}