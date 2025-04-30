namespace DevFreela.API.Models
{
    public class UserCreateInputModel
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
