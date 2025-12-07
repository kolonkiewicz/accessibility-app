namespace inzynierka.Models.ViewModels
{
    public class ReportListItemViewModel
    {
        public int ScanId { get; set; }
        public string Url { get; set; }
        public DateTime ScanDate { get; set; }
        public int ErrorCount { get; set; }
    }
}
