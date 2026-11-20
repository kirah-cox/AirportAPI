using System;
using System.Collections.Generic;

namespace AirportApi.Models;

public partial class WeekRequest
{
    public int Id { get; set; }

    public int EmployeeId { get; set; }

    public string Status { get; set; } = null!;

    public DateTime RequestDate { get; set; }

    public DateTime? LastMatchedDate { get; set; }

    public virtual ICollection<Choice> Choices { get; set; } = new List<Choice>();

    public virtual Employee Employee { get; set; } = null!;
}
