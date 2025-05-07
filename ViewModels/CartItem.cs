namespace TECHNOVA.ViewModels
{
    public class CartItem
    {
        public int productId { get; set; }
        public string productName { get; set; }

        public string image {  get; set; }

        public double price { get; set; }
        public int quantity { get; set; }

        public double total => price * quantity;
    }
}
