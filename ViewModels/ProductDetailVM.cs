namespace TECHNOVA.ViewModels
{
    public class ProductDetailVM
    {
        public int productID { get; set; }
        public string productName { get; set; }
        public string image {  get; set; }
        public double price { get; set; }

        public string unitDescription { get; set; }

        public string description { get; set; }
        public int viewCount { get; set;}
        public IEnumerable<ItemVM> RelatedProducts { get; set; }
    }
}
