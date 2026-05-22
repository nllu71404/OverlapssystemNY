namespace Overlapssystem.ViewModels
{
    public class MedicinViewModel
    {
        public int MedicinTimeID { get; set; }

        public int? ResidentID { get; set; } = 0;

        public string MedicinTimeText { get; set; } = ""; 
        public bool IsChecked { get; set; }

        public string? MedicinCheckTimeStampText { get; set; } = ""; 
    }
}
