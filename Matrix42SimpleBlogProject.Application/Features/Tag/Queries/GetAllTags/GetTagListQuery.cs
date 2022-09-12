using MediatR;

namespace Matrix42SimpleBlogProject.Application.Features.Tag.Queries.GetAllTags;

public class GetTagListQuery : IRequest<List<TagListVm>>
{
}