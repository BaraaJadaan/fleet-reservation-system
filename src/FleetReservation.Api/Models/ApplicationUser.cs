namespace FleetReservation.Api.Models;

public class ApplicationUser
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public UserRole Role { get; set; } = UserRole.Employee;

    public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
}
