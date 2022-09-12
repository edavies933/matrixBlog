using AutoMapper;
using Matrix42SimpleBlogProject.Application.Contracts.Persistence;
using Matrix42SimpleBlogProject.Application.Features.Blogs.Commands.CreateBlog;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Matrix42SimpleBlogProject.Application.Features.Blog.Commands.CreateBlog
{
    public class CreateBlogCommandHandler : IRequestHandler<CreateBlogCommand, Guid>
    {
        private readonly IAsyncRepository<Domain.Entities.Blog> blogRepository;
        private readonly IMapper mapper;
        private readonly ILogger<CreateBlogCommandHandler> logger;
        public CreateBlogCommandHandler(IAsyncRepository<Domain.Entities.Blog> blogRepository, IMapper mapper, ILogger<CreateBlogCommandHandler> logger)
        {
            this.blogRepository = blogRepository;
            this.mapper = mapper;
            this.logger = logger;
        }

        public async Task<Guid> Handle(CreateBlogCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateBlogCommandValidator();
            var validationResult = validator.Validate(request);

            if (validationResult.Errors.Count > 0)
                throw new Exceptions.ValidationException(validationResult);

            var blog = mapper.Map<Domain.Entities.Blog>(request);
            blog.Id = Guid.NewGuid();
            blog = await blogRepository.AddAsync(blog);

            logger.LogInformation($"New blog with id {blog.Id} created");
            return blog.Id;
        }
    }
}
