using DevFreela.API.Entities;

namespace DevFreela.API.Models
{
    public class UserCreateInputModel
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }

        public User ToEntity()
        {
            return new User(FullName, Email, BirthDate);
        }
    }
}
