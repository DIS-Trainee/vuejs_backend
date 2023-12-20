namespace vuejs_backend.Models
{
    public class ProductInfo
    {
        public int id { get; set; }
        public string title { get; set; }
        public int price { get; set; }
        public string? description { get; set; }
        public string? category { get; set; }
        public string? image { get; set; }
        public int rate { get; set; }
        public int count { get; set; }
    }
}