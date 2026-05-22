using OverlapssystemDomain.Enums;

namespace Overlapssystem.ViewModels
{
    public class ShoppingViewModel
    {
        public int ShoppingID { get; set; }
        public int ResidentID { get; set; } = 0;
        public Day Day { get; set; }
        public string? TimeText { get; set; } = ""; 
        public string PaymentMethod { get; set; } = "";
    }
}
