namespace Overlapssystem.ViewModels
{
    public class MedicinViewModel
    {
        public int MedicinTimeID { get; set; }

        public int ResidentID { get; set; } = 0;

        public string MedicinTimeText { get; set; } = ""; // så vi ikke skal convertere til og fra TimeSpan i viewet
        public bool IsChecked { get; set; }

        public string? MedicinCheckTimeStampText { get; set; } = ""; // så vi ikke skal convertere til og fra DateTime i viewet
    }
}
