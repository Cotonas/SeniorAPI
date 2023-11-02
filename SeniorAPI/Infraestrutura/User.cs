using System.ComponentModel.DataAnnotations;

namespace SeniorAPI.Infraestrutura
{
    public class User
    {
        [Key]
        public string Login { get; set; }
        public string Password { get; set; }

        public User()
        {
        }

        public User(string login, string password)
        {
            Login = login ?? throw new ArgumentNullException(nameof(login));
            Password = password ?? throw new ArgumentNullException(nameof(password));
        }
    }
}
