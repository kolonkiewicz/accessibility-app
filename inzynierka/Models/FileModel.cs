using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace inzynierka.Models
{
    public class FileModel
    {
        
        [Key, Column(Order = 1)]
        public int FileID { get; set; }

        [Required(ErrorMessage = "Wprowadz nazwe pliku" )]
        public string FileName { get; set; }
        public DateTime Date_add { get; set; }
        public long Size { get; set; }

        
        public byte[]? FileData { get; set; } 
        public int UserFileNumber { get; set; } //inkrementacja uzytkownika do danego pliku

        public int UserId { get; set; }  // Klucz obcy do identyfikatora użytkownika
        public UserModel User { get; set; } = null!; // Właściwość nawigacyjna do użytkownika

    }

}
