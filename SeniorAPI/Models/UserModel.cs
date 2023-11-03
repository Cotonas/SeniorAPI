using System.ComponentModel.DataAnnotations;

namespace SeniorAPI.Models
{
    public class UserModel
    {
        public string Login { get; set; }
        public string Password { get; set; }

        public UserModel(string login, string password)
        {
            Login = login ?? throw new ArgumentNullException(nameof(login));
            Password = password ?? throw new ArgumentNullException(nameof(password));
        }
    }
}
