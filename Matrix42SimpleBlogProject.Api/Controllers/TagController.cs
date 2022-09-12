using Matrix42SimpleBlogProject.Application.Features.Tag.Commands.CreateTag;
using Matrix42SimpleBlogProject.Application.Features.Tag.Commands.DeleteTag;
using Matrix42SimpleBlogProject.Application.Features.Tag.Queries.GetAllTags;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Matrix42SimpleBlogProject.Api.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class TagController : ControllerBase
{

    private readonly IMediator mediator;

    public TagController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpGet(Name = "GetAllTags")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<List<TagListVm>>> GetAllTags()
    {
        var tagListVms = await mediator.Send(new GetTagListQuery());
        return Ok(tagListVms);
    }

    [HttpPost(Name = "AddTag")]
    public async Task<ActionResult<Guid>> Create([FromBody] CreateTagCommand createTagCommand)
    {
        var response = await mediator.Send(createTagCommand);
        return Ok(response);
    }

    [HttpPost(Name = "DeleteTag")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> Delete([FromBody] DeleteTagCommand deleteTagCommand)
    {
        await mediator.Send(deleteTagCommand);
        return NoContent();
    }
}