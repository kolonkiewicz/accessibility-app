namespace inzynierka.Models
{
    public class FileViewModel
    {
        public int id {  get; set; }
        public string Name { get; set; }
        public IFormFile formFile { get; set; }
    }
}
