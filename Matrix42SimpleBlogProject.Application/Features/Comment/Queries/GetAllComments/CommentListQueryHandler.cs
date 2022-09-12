using AutoMapper;
using Matrix42SimpleBlogProject.Application.Contracts.Persistence;
using MediatR;

namespace Matrix42SimpleBlogProject.Application.Features.Comment.Queries.GetAllComments
{
    public class CommentListQueryHandler : IRequestHandler<CommentListQuery, List<CommentListVm>>
    {
        private readonly IAsyncRepository<Domain.Entities.Comment> commentRepository;
        private readonly IMapper mapper;

        public CommentListQueryHandler(IMapper mapper, IAsyncRepository<Domain.Entities.Comment> commentRepository)
        {
            this.mapper = mapper;
           this.commentRepository = commentRepository;
        }

        public async Task<List<CommentListVm>> Handle(CommentListQuery request, CancellationToken cancellationToken)
        {
            var allTags = (await commentRepository.ListAllAsync()).Where(b => b.BlogPostId.Equals(request.BlogPostId));
            return mapper.Map<List<CommentListVm>>(allTags);
        }
    }
}
