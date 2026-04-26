namespace Overlapssystem.ViewModels
{
    public class SpecialEventViewModel
    {
        public int SpecialEventID { get; set; }
        public int? ResidentID { get; set; }
        public string? SpecialEventNote { get; set; }
        public string? SpecialEventDateTimeText { get; set; } = null; // så vi ikke skal convertere til og fra DateTime i viewet
    }
}
