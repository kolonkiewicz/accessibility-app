using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace inzynierka.Models
{
    public class ViolationModel
    {
        [Key]
        public int ViolationId { get; set; }

        public string RuleId { get; set; }
        public string Impact { get; set; }
        public string Description { get; set; }
        public string Help { get; set; }
        public string Selector { get; set; }
        public string Html { get; set; }

        // FK → Scan
        public int ScanId { get; set; }
        public ScanModel Scan { get; set; }
    }
}
