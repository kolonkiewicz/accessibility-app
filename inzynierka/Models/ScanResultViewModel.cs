namespace inzynierka.Models
{
    public class ScanResultViewModel
    {
        public string Url { get; set; }
        public DateTime Date { get; set; }
        public List<ViolationModel> Violations { get; set; }
    }
}
