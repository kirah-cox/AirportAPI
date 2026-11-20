using System;
using System.Collections.Generic;

namespace AirportApi.Models;

public partial class Absence
{
    public int Id { get; set; }

    public int EmployeeId { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    public virtual Employee Employee { get; set; } = null!;
}
