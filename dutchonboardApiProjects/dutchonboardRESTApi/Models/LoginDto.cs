using System.ComponentModel.DataAnnotations;

namespace dutchonboardRESTApi.Models;

#nullable disable
public class LoginDto
{
    [Required]
    public string EmailAddress { get; set; }

    [Required]
    public string Password { get; set; }
}