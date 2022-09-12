using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrix42SimpleBlogProject.Application.Features.Blogs.Commands.DeleteBlog
{
    public class DeleteBlogCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
