using System;
using System.Collections.Generic;

namespace AirportApi.Models;

public partial class WeekResponse
{
    public int Id { get; set; }

    public int PublishedId { get; set; }

    public int EmployeeId { get; set; }

    public string Response { get; set; } = null!;

    public string? Comments { get; set; }

    public DateOnly ResponseDate { get; set; }

    public virtual Employee Employee { get; set; } = null!;

    public virtual Published Published { get; set; } = null!;
}
