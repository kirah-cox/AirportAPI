using System;
using System.Collections.Generic;

namespace AirportApi.Models;

public partial class Assignment
{
    public int Id { get; set; }

    public int DatedSlotId { get; set; }

    public int? EmployeeId { get; set; }

    public int? ReplacedEmployeeId { get; set; }

    public int? PublishedId { get; set; }

    public int AdminId { get; set; }

    public string Source { get; set; } = null!;

    public DateTime AssignmentDate { get; set; }

    public virtual Employee Admin { get; set; } = null!;

    public virtual DatedSlot DatedSlot { get; set; } = null!;

    public virtual Employee? Employee { get; set; }

    public virtual Published? Published { get; set; }

    public virtual Employee? ReplacedEmployee { get; set; }
}
