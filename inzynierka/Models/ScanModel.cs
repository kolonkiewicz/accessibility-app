using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace inzynierka.Models
{
    public class ScanModel
    {
        [Key]
        public int ScanId { get; set; }

        [Required]
        public string Url { get; set; }

        public DateTime ScanDate { get; set; } = DateTime.Now;

        public string? FullResultJson { get; set; }

        // FK → User
        public int UserId { get; set; }
        public UserModel User { get; set; }

        // relacja do błędów
        public ICollection<ViolationModel> Violations { get; set; }
    }
}
