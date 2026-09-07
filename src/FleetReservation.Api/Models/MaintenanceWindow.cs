namespace FleetReservation.Api.Models;

public class MaintenanceWindow
{
    public int Id { get; set; }

    public int VehicleId { get; set; }
    public Vehicle Vehicle { get; set; } = null!;

    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }

    public string Reason { get; set; } = string.Empty;
}
