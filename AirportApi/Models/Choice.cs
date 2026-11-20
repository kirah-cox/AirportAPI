using System;
using System.Collections.Generic;

namespace AirportApi.Models;

public partial class Choice
{
    public int Id { get; set; }

    public int DayOfWeekId { get; set; }

    public int LocationId { get; set; }

    public int PositionId { get; set; }

    public int RequestId { get; set; }

    public int? TemplateSlotId { get; set; }

    public int? AdminId { get; set; }

    public int Rank { get; set; }

    public DateOnly? MatchedDate { get; set; }

    public virtual Employee? Admin { get; set; }

    public virtual DayOfWeek DayOfWeek { get; set; } = null!;

    public virtual Location Location { get; set; } = null!;

    public virtual Position Position { get; set; } = null!;

    public virtual WeekRequest Request { get; set; } = null!;

    public virtual Template? TemplateSlot { get; set; }
}
