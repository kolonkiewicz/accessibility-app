namespace inzynierka.Models.ViewModels
{
    public class DeclarationViewModel
    {
        public string Url { get; set; }
        public DateTime ScanDate { get; set; }
        public List<ViolationModel> Violations { get; set; }

        public string Status =>
        Violations.Count == 0
        ? "Strona zgodna z WCAG 2.1"
        : Violations.Any(v => v.Impact == "serious" || v.Impact == "critical")
            ? "Strona niezgodna"
            : "Strona częściowo zgodna";

        public string PreparedDate => DateTime.Now.ToString("dd.MM.yyyy");
    }
}
