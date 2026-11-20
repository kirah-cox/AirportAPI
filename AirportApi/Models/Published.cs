using System;
using System.Collections.Generic;

namespace AirportApi.Models;

public partial class Published
{
    public int Id { get; set; }

    public int WeekId { get; set; }

    public int AdminId { get; set; }

    public DateTime PublishedAt { get; set; }

    public string Kind { get; set; } = null!;

    public virtual Employee Admin { get; set; } = null!;

    public virtual ICollection<Assignment> Assignments { get; set; } = new List<Assignment>();

    public virtual Week Week { get; set; } = null!;

    public virtual ICollection<WeekResponse> WeekResponses { get; set; } = new List<WeekResponse>();
}
