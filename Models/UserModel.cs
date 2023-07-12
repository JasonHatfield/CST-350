using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CST_350_Milestone.Models
{
    public class UserModel
    {
        public int Id { get; set; }

        [Required]
        [DisplayName("First Name")]
        [StringLength(20, MinimumLength = 3)]
        public string FirstName { get; set; }

        [Required]
        [DisplayName("Last Name")]
        [StringLength(20, MinimumLength = 3)]
        public string LastName { get; set; }

        [Required]
        [DisplayName("Sex")]
        [StringLength(15, MinimumLength = 3)]
        public string Sex { get; set; }

        [Required]
        [DisplayName("Age")]
        [Range(18, 120)]
        public int Age { get; set; }

        [Required]
        [DisplayName("State")]
        [StringLength(2, MinimumLength = 2)]
        public string State { get; set; }

        [Required]
        [DisplayName("Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DisplayName("Username")]
        [StringLength(20, MinimumLength = 3)]
        public string Username { get; set; }

        [Required]
        [DisplayName("Password")]
        [StringLength(20, MinimumLength = 4)]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        public UserModel()
        {
        }

        public UserModel(int id, string firstName, string lastName, string sex, int age, string state, string email, string username, string password)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Sex = sex;
            Age = age;
            State = state;
            Email = email;
            Username = username;
            Password = password;
        }
    }
}
