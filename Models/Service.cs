namespace back_end.Models
{
    public class Service
    {
        public string ServiceName { get; set; }
        public string Description { get; set; }
        public string CategoryID { get; set; }
        public int StudioID {  get; set; }
        public decimal Price { get; set; }
        public IFormFile Image {  get; set; }

        public float Rating { get; set; }
    }
}
