namespace inzynierka.Models.ViewModels
{
    public class DeclarationViewModel
    {
        public string Url { get; set; }
        public DateTime ScanDate { get; set; }
        public List<ViolationModel> Violations { get; set; }

        // pomocnicze pola
        public string Status =>
            Violations.Count == 0 ? "Strona zgodna z WCAG 2.1" :
            Violations.Count < 10 ? "Strona częściowo zgodna" :
            "Strona niezgodna";

        public string PreparedDate => DateTime.Now.ToString("dd.MM.yyyy");
    }
}
