using Matrix42SimpleBlogProject.Application.Features.Comment.Command.CreateComment;
using Matrix42SimpleBlogProject.Application.Features.Comment.Command.DeleteComment;
using Matrix42SimpleBlogProject.Application.Features.Comment.Queries.GetAllComments;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Matrix42SimpleBlogProject.Api.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class CommentController : ControllerBase
{

    private readonly IMediator mediator;

    public CommentController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpGet("{blogPostId}", Name = "GetAllCommentsByBlogPostId")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<List<CommentListVm>>> GetAllCommentsByBlogPostId(Guid blogPostId)
    {
        var commentListVms = await mediator.Send(new CommentListQuery() { BlogPostId = blogPostId });
        return Ok(commentListVms);
    }

    [HttpPost(Name = "AddComment")]
    public async Task<ActionResult<Guid>> Create([FromBody] CreateCommentCommand createCommentCommand)
    {
        var response = await mediator.Send(createCommentCommand);
        return Ok(response);
    }

    [HttpPost(Name = "DeleteComment")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> Delete([FromBody] DeleteCommentCommand deleteCommentCommand)
    {
        await mediator.Send(deleteCommentCommand);
        return NoContent();
    }
}