using System.ComponentModel.DataAnnotations;

namespace inzynierka.Models
{
    public class FixSuggestion
    {
        [Key]
        public int Id { get; set; }
        public string RuleId { get; set; }
        public string Suggestion { get; set; }
    }
}
