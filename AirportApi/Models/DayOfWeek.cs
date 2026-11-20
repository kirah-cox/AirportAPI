using System;
using System.Collections.Generic;

namespace AirportApi.Models;

public partial class DayOfWeek
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Choice> Choices { get; set; } = new List<Choice>();

    public virtual ICollection<DatedSlot> DatedSlots { get; set; } = new List<DatedSlot>();

    public virtual ICollection<Template> Templates { get; set; } = new List<Template>();
}
