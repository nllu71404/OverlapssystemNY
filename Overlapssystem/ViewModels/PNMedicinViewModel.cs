namespace Overlapssystem.ViewModels
{
    public class PNMedicinViewModel
    {
        public int PNMedicinID { get; set; }
        public int? ResidentID { get; set; } = 0;
        public DateTime? PNTime { get; set; }
        public string Reason { get; set; }= "";
    }
}
