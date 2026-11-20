using System;
using System.Collections.Generic;

namespace AirportApi.Models;

public partial class Certification
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int? ValidForMonths { get; set; }

    public virtual ICollection<EmployeeCertification> EmployeeCertifications { get; set; } = new List<EmployeeCertification>();

    public virtual ICollection<PositionCertification> PositionCertifications { get; set; } = new List<PositionCertification>();
}
