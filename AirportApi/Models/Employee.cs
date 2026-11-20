using System;
using System.Collections.Generic;

namespace AirportApi.Models;

public partial class Employee
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Role { get; set; } = null!;

    public virtual ICollection<Absence> Absences { get; set; } = new List<Absence>();

    public virtual ICollection<Assignment> AssignmentAdmins { get; set; } = new List<Assignment>();

    public virtual ICollection<Assignment> AssignmentEmployees { get; set; } = new List<Assignment>();

    public virtual ICollection<Assignment> AssignmentReplacedEmployees { get; set; } = new List<Assignment>();

    public virtual ICollection<Choice> Choices { get; set; } = new List<Choice>();

    public virtual ICollection<EmployeeCertification> EmployeeCertificationAdmins { get; set; } = new List<EmployeeCertification>();

    public virtual ICollection<EmployeeCertification> EmployeeCertificationEmployees { get; set; } = new List<EmployeeCertification>();

    public virtual ICollection<Published> Publisheds { get; set; } = new List<Published>();

    public virtual ICollection<Template> Templates { get; set; } = new List<Template>();

    public virtual WeekRequest? WeekRequest { get; set; }

    public virtual ICollection<WeekResponse> WeekResponses { get; set; } = new List<WeekResponse>();
}
