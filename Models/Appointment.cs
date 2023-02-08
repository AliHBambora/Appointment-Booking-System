using System;
using System.Collections.Generic;

namespace Appointment_booking_system.Models;

public partial class Appointment
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public int? BookingYear { get; set; }

    public int? BookingMonth { get; set; }

    public int? BookingDay { get; set; }

    public DateTime BookingDate { get; set; }

    public string? BookingTime { get; set; }

    public virtual User User { get; set; } = null!;
}
