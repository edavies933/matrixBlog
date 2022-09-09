namespace Matrix42SimpleBlogProject.Domain
{
    public class Comment
    {
        public string Content { get; set; }

        public Comment(string content)
        {
            Content = content;
        }

        public DateTime CreatedDate { get; set; }

    }
}