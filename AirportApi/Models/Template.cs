using System;
using System.Collections.Generic;

namespace AirportApi.Models;

public partial class Template
{
    public int Id { get; set; }

    public int LocationId { get; set; }

    public int PositionId { get; set; }

    public int DayOfWeekId { get; set; }

    public int SeatNumber { get; set; }

    public int? RegularEmployeeId { get; set; }

    public virtual ICollection<Choice> Choices { get; set; } = new List<Choice>();

    public virtual DayOfWeek DayOfWeek { get; set; } = null!;

    public virtual Location Location { get; set; } = null!;

    public virtual Position Position { get; set; } = null!;

    public virtual Employee? RegularEmployee { get; set; }
}
