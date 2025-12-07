namespace inzynierka.Models.ViewModels
{
    public class ScanResultViewModel
    {
        public int ScanId { get; set; }
        public string Url { get; set; }
        public DateTime Date { get; set; }
        public List<ScanViolationWithFix> Violations { get; set; }
    }
}
