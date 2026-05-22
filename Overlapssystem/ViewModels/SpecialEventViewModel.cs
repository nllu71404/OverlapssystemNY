namespace Overlapssystem.ViewModels
{
    public class SpecialEventViewModel
    {
        public int SpecialEventID { get; set; }
        public int? ResidentID { get; set; }
        public string? SpecialEventNote { get; set; } = "";
        public string? SpecialEventDateTimeText { get; set; } = ""; 
    }
}
