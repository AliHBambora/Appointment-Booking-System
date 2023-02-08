using System;
using System.Collections.Generic;

namespace Appointment_booking_system.Models;

public partial class User
{
    public Guid Id { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? CivilId { get; set; }

    public string? MobileNo { get; set; }

    public virtual ICollection<Appointment> Appointments { get; } = new List<Appointment>();
}
