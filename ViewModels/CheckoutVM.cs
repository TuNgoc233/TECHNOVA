using System.Reflection.Metadata;

namespace TECHNOVA.ViewModels
{
    public class CheckoutVM
    {
        public bool IsChecked { get; set; }
        public string? fullName { get; set; }

        public string address { get; set; }

        public string? phone { get; set; }

        public string? note { get; set; }
    }
}
