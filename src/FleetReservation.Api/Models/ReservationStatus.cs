namespace FleetReservation.Api.Models;

public enum ReservationStatus
{
    Requested,
    Approved,
    CheckedOut,
    Returned,
    Cancelled,
    Overdue
}
