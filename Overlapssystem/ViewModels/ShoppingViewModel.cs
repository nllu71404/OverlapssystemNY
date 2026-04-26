using OverlapssystemDomain.Enums;

namespace Overlapssystem.ViewModels
{
    public class ShoppingViewModel
    {
        public int ShoppingID { get; set; }
        public int ResidentID { get; set; } = 0;
        public Day Day { get; set; }
        public string? TimeText { get; set; } = ""; // så vi ikke skal convertere til og fra TimeSpan i viewet
        public string PaymentMethod { get; set; } = "";
    }
}
