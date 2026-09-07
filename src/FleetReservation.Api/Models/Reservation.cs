namespace FleetReservation.Api.Models;

public class Reservation
{
    public int Id { get; set; }

    // Foreign Key + Reference Navigation Property for Vehicle
    public int VehicleId { get; set; }
    public Vehicle Vehicle { get; set; } = null!;

    // Foreign Key + Reference Navigation Property for ApplicationUser
    public int UserId { get; set; }
    public ApplicationUser User { get; set; } = null!;

    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }

    public ReservationStatus Status { get; set; } = ReservationStatus.Requested;
}
