using AutoMapper;
using Matrix42SimpleBlogProject.Application.Contracts.Persistence;
using MediatR;

namespace Matrix42SimpleBlogProject.Application.Features.Tag.Queries.GetAllTags;

public class GetTagListHandler : IRequestHandler<GetTagListQuery, List<TagListVm>>
{
    private readonly IAsyncRepository<Domain.Entities.Tag> tagRepository;
    private readonly IMapper mapper;

    public GetTagListHandler(IMapper mapper, IAsyncRepository<Domain.Entities.Tag> tagRepository)
    {
        this.mapper = mapper;
        this.tagRepository = tagRepository;
    }


    public async Task<List<TagListVm>> Handle(GetTagListQuery request, CancellationToken cancellationToken)
    {
        var allTags = await tagRepository.ListAllAsync();
        return mapper.Map<List<TagListVm>>(allTags);
    }
}