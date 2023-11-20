namespace WebAPP.Models
{
    public class FileUploadModel
    {
        public IFormFile file { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
    }
}
