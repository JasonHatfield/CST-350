using System.ComponentModel.DataAnnotations;

namespace LoginForm.Models;

public class UserModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "First Name is required.")]
    public string? FirstName { get; set; }

    [Required(ErrorMessage = "Last Name is required.")]
    public string? LastName { get; set; }

    [Required(ErrorMessage = "Sex is required.")]
    public string? Sex { get; set; }

    [Required(ErrorMessage = "Age is required.")]
    public int Age { get; set; }

    [Required(ErrorMessage = "State is required.")]
    public string? State { get; set; }

    [Required(ErrorMessage = "Email Address is required.")]
    [EmailAddress(ErrorMessage = "Invalid Email Address.")]
    public string? EmailAddress { get; set; }

    [Required(ErrorMessage = "Username is required.")]
    public string? UserName { get; set; }

    [Required(ErrorMessage = "Password is required.")]
    [DataType(DataType.Password)]
    public string? Password { get; set; }
}
