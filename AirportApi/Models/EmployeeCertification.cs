using System;
using System.Collections.Generic;

namespace AirportApi.Models;

public partial class EmployeeCertification
{
    public int Id { get; set; }

    public int CertificationId { get; set; }

    public int EmployeeId { get; set; }

    public int? AdminId { get; set; }

    public string Status { get; set; } = null!;

    public DateOnly IssueDate { get; set; }

    public string DocumentReference { get; set; } = null!;

    public DateOnly? ReviewDate { get; set; }

    public DateOnly? ExpirationDate { get; set; }

    public virtual Employee? Admin { get; set; }

    public virtual Certification Certification { get; set; } = null!;

    public virtual Employee Employee { get; set; } = null!;
}
