using System;
using System.Collections.Generic;

namespace AirportApi.Models;

public partial class PositionCertification
{
    public int Id { get; set; }

    public int PositionId { get; set; }

    public int CertificationId { get; set; }

    public virtual Certification Certification { get; set; } = null!;

    public virtual Position Position { get; set; } = null!;
}
