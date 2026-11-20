using System;
using System.Collections.Generic;

namespace AirportApi.Models;

public partial class Position
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public virtual ICollection<Choice> Choices { get; set; } = new List<Choice>();

    public virtual ICollection<DatedSlot> DatedSlots { get; set; } = new List<DatedSlot>();

    public virtual ICollection<PositionCertification> PositionCertifications { get; set; } = new List<PositionCertification>();

    public virtual ICollection<Template> Templates { get; set; } = new List<Template>();
}
