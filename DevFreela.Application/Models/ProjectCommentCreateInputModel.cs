namespace DevFreela.Application.Models
{
    public class ProjectCommentCreateInputModel
    {
        public string Content { get; set; }
        public int IdProject { get; set; }
        public int IdUser { get; set; }
    }
}
