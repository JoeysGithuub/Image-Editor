namespace ImageEdit.Models
{
    public class UploadImageModel
    {
        public string FileName { get; set; }
        public string Image { get; set; }
        public int Id { get; set; }
        public int CategoryId { get; set; }
    }
}