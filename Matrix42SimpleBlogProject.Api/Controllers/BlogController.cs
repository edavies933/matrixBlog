using Matrix42SimpleBlogProject.Application.Features.Blog.Queries.GetBlogDetail;
using Matrix42SimpleBlogProject.Application.Features.Blog.Queries.GetBlogsList;
using Matrix42SimpleBlogProject.Application.Features.Blogs.Commands.CreateBlog;
using Matrix42SimpleBlogProject.Application.Features.Blogs.Commands.DeleteBlog;
using Matrix42SimpleBlogProject.Application.Features.Blogs.Commands.UpdateBlog;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Matrix42SimpleBlogProject.Api.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class BlogController : ControllerBase
{

    private readonly IMediator mediator;

    public BlogController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpGet(Name = "GetAllBlogs")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<List<BlogListVm>>> GetAllBlogs()
    {
        var dtos = await mediator.Send(new GetBlogsListQuery());
        return Ok(dtos);
    }

    [HttpGet("{daysAgo}", Name = "GetAllBlogsFromDaysAgo")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<List<BlogListVm>>> GetAllBlogsFromDaysAgo(int daysAgo)
    {
        var blogListVms = await mediator.Send(new GetBlogsCreatedInPastDaysQuery {DaysAgo = daysAgo});
        return Ok(blogListVms);
    }


    [HttpGet("{id}", Name = "GetBlogById")]
    public async Task<ActionResult<BlogListVm>> GetBlogById(Guid id)
    {
        var getBlogDetailQuery = new GetBlogDetailQuery { BlogId = id };
        return Ok(await mediator.Send(getBlogDetailQuery));
    }

    [HttpPost(Name = "AddBlog")]
    public async Task<ActionResult<Guid>> Create([FromBody] CreateBlogCommand createBlogCommand)
    {
        //TODO get user id
        var response = await mediator.Send(createBlogCommand);
        return Ok(response);
    }

    [HttpPost(Name = "DeleteBlog")]
    public async Task<ActionResult> Delete([FromBody] DeleteBlogCommand deleteBlogCommand)
    {
        await mediator.Send(deleteBlogCommand);
        return NoContent();
    }

    [HttpPost(Name = "UpdateBlog")]
    public async Task<ActionResult> Update([FromBody] UpdateBlogCommand updateBlogCommand)
    {
        await mediator.Send(updateBlogCommand);
        return NoContent();
    }
}