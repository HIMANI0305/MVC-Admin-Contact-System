using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;
namespace Repositories;
public class t_User
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int c_UserId { get; set; }
    [StringLength(100)]
    [Required(ErrorMessage = "Username is required.")]
    public string c_UserName { get; set; }
    
    
    [StringLength(100)]
    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Invalid email format.")]
    public  string c_Email { get; set; }
    [StringLength(100)]
    [Required(ErrorMessage = "Password is required.")]
    [MinLength(6, ErrorMessage = "Password must be at least 6 characters long.")]
    public string c_Password { get; set; }
    [StringLength(100)]
    [Required(ErrorMessage = "Confirm Password is required.")]
    [Compare("c_Password", ErrorMessage = "Passwords do not match.")]    
    public string c_ConfirmPassword { get; set; }
 
    
    [StringLength(500)]
    public string? c_Address { get; set; }
    [StringLength(50)]
    public string? c_Mobile { get; set; }
    [StringLength(10)]
    public string? c_Gender { get; set; }
    [StringLength(4000)]
    public string? c_Image { get; set; }
    public IFormFile? ProfilePicture { get; set; }
}
