using System;
using System.Collections.Generic;

namespace AirportApi.Models;

public partial class Week
{
    public int Id { get; set; }

    public DateOnly MondayDate { get; set; }

    public virtual ICollection<DatedSlot> DatedSlots { get; set; } = new List<DatedSlot>();

    public virtual ICollection<Published> Publisheds { get; set; } = new List<Published>();
}
