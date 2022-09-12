using Matrix42SimpleBlogProject.Application.Features.Blog.Queries.GetBlogDetail;
using Matrix42SimpleBlogProject.Application.Features.Blog.Queries.GetBlogsList;
using Matrix42SimpleBlogProject.Application.Features.BlogPost.Commands.CreateBlogPost;
using Matrix42SimpleBlogProject.Application.Features.BlogPost.Commands.UpdateBlogPost;
using Matrix42SimpleBlogProject.Application.Features.BlogPost.Queries.GetBlogPostDetails;
using Matrix42SimpleBlogProject.Application.Features.BlogPost.Queries.GetBlogPostList;
using Matrix42SimpleBlogProject.Application.Features.Blogs.Commands.CreateBlog;
using Matrix42SimpleBlogProject.Application.Features.Blogs.Commands.UpdateBlog;
using Matrix42SimpleBlogProject.Application.Features.Comment.Command.CreateComment;
using Matrix42SimpleBlogProject.Application.Features.Comment.Queries.GetAllComments;
using Matrix42SimpleBlogProject.Application.Features.Tag.Commands.CreateTag;
using Matrix42SimpleBlogProject.Application.Features.Tag.Queries.GetAllTags;
using Matrix42SimpleBlogProject.Domain.Entities;

namespace Matrix42SimpleBlogProject.Application.Profile
{
    public class MappingProfile : AutoMapper.Profile
    {
        public MappingProfile()
        {
            CreateMap<Blog, BlogListVm>().ReverseMap();
            CreateMap<Blog, BlogDetailVm>().ReverseMap();
            CreateMap<Blog, CreateBlogCommand>().ReverseMap();
            CreateMap<Blog, UpdateBlogCommand>().ReverseMap();
            CreateMap<BlogPost, BlogPostListVm>().ReverseMap();
            CreateMap<BlogPost, BlogPostDetailVm>().ReverseMap();
            CreateMap<BlogPost, CreateBlogPostCommand>().ReverseMap();
            CreateMap<BlogPost, UpdateBlogPostCommand>().ReverseMap();
            CreateMap<Tag, CreateTagCommand>().ReverseMap();
            CreateMap<Tag, TagListVm>().ReverseMap();
            CreateMap<Comment, CommentListVm>().ReverseMap();
            CreateMap<Comment, CreateCommentCommand>().ReverseMap();
        }
    }
}
