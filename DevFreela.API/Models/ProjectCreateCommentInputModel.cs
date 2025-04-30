namespace DevFreela.API.Models
{
    public class ProjectCreateCommentInputModel
    {
        public string Content { get; set; }
        public int IdProject { get; set; }
        public int IdUser { get; set; }
    }
}
