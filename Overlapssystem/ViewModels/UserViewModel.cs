namespace Overlapssystem.ViewModels
{
    public class UserViewModel
    {
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public int? DepartmentId { get; set; }
        public string Role { get; set; } = string.Empty;
    }
}
