namespace TECHNOVA.ViewModels
{
    public class VnPaymentResponseModel
    {
        public bool Success { get; set; }
        public string PaymentMethod { get; set; }
        public string OrderDescription { get; set; }
        public string OrderId { get; set; }
        public string PaymentId { get; set; }
        public string TransactionId { get; set; }
        public string Token { get; set; }
        public string VnPayResponseCode { get; set; }
    }

    public class VnPaymentRequestModel
    {
        public string fullName { get; set; }
        public string description { get; set; }
        public double amount { get; set; }

        public DateTime CreatedDate { get; set; }

        public int OrderId { get; set; }
    }
}
