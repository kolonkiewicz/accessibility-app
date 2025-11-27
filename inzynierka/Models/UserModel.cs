using Microsoft.AspNetCore.Components.Forms;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace inzynierka.Models
{
    public class UserModel
    {
        [Key, Column(Order = 1)]
        public int UserId { get; set; }


        [Required(ErrorMessage = "To pole jest wymagane")]
        [StringLength(30, MinimumLength = 2,ErrorMessage ="Długość od 2-30")]
        public string Name { get; set; }

        [Required(ErrorMessage = "To pole jest wymagane")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "Długość od 2-30")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "To pole jest wymagane")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "Długość od 2-30")]
        public string Username { get; set; }

        [Required(ErrorMessage = "To pole jest wymagane")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,15}$",ErrorMessage = "Podaj poprawne hasło")]
        public string Password { get; set; }

        [Required(ErrorMessage = "To pole jest wymagane")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}",ErrorMessage ="Podaj poprawny e-mail")]
        public string Email { get; set; }
        
        public string? Role  { get; set; }
        public ICollection<FileModel>? Files { get; set; } = null!;

        [NotMapped]
        [Required(ErrorMessage = "To pole jest wymagane")]
        [Compare("Password" , ErrorMessage ="Hasła muszą być takie same")]
        public string Ppassword { get; set; }

        
    }
}
