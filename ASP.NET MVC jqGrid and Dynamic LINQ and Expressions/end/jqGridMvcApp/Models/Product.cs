namespace jqGridMvcApp.Models
{
    public class Product
    {
        public int? ProductId { get; set; }
        public string ProductName { get; set; }
        public string Category { get; set; }
        public string Supplier { get; set; }
        public decimal? UnitPrice { get; set; }
        public short? UnitsInStock { get; set; }
        public string EnglishName { get; set; }
        public bool? Discontinued { get; set; }
    }
}