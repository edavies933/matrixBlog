namespace Matrix42SimpleBlogProject.Domain
{
    public class BlogPost
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public Tag Tag { get; set; }

        public BlogPost(string content, Tag tag = Tag.Others)
        {
            this.Content = content;
            Tag = tag;
        }

        public DateTime CreatedDate { get; set; }
        public List<Comment>? Comments { get; set; }
    }
}