using System;
using System.Collections.Generic;

namespace AirportApi.Models;

public partial class DatedSlot
{
    public int Id { get; set; }

    public int WeekId { get; set; }

    public int LocationId { get; set; }

    public int PositionId { get; set; }

    public int DayOfWeekId { get; set; }

    public int SeatNumber { get; set; }

    public DateOnly WorkDate { get; set; }

    public virtual ICollection<Assignment> Assignments { get; set; } = new List<Assignment>();

    public virtual DayOfWeek DayOfWeek { get; set; } = null!;

    public virtual Location Location { get; set; } = null!;

    public virtual Position Position { get; set; } = null!;

    public virtual Week Week { get; set; } = null!;
}
