using System;
using System.Collections.Generic;
using AirportApi.Models;
using Microsoft.EntityFrameworkCore;

namespace AirportApi.Data;

public partial class AirportContext : DbContext
{
    public AirportContext(DbContextOptions<AirportContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Absence> Absences { get; set; }

    public virtual DbSet<Assignment> Assignments { get; set; }

    public virtual DbSet<Certification> Certifications { get; set; }

    public virtual DbSet<Choice> Choices { get; set; }

    public virtual DbSet<DatedSlot> DatedSlots { get; set; }

    public virtual DbSet<DayOfWeek> DayOfWeeks { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<EmployeeCertification> EmployeeCertifications { get; set; }

    public virtual DbSet<Location> Locations { get; set; }

    public virtual DbSet<Position> Positions { get; set; }

    public virtual DbSet<PositionCertification> PositionCertifications { get; set; }

    public virtual DbSet<Published> Publisheds { get; set; }

    public virtual DbSet<Template> Templates { get; set; }

    public virtual DbSet<Week> Weeks { get; set; }

    public virtual DbSet<WeekRequest> WeekRequests { get; set; }

    public virtual DbSet<WeekResponse> WeekResponses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Absence>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("absence_pk");

            entity.ToTable("absence", "airport");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.EmployeeId).HasColumnName("employee_id");
            entity.Property(e => e.EndDate).HasColumnName("end_date");
            entity.Property(e => e.StartDate).HasColumnName("start_date");

            entity.HasOne(d => d.Employee).WithMany(p => p.Absences)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("absence_employee_fk");
        });

        modelBuilder.Entity<Assignment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("assignment_pk");

            entity.ToTable("assignment", "airport");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AdminId).HasColumnName("admin_id");
            entity.Property(e => e.AssignmentDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("assignment_date");
            entity.Property(e => e.DatedSlotId).HasColumnName("dated_slot_id");
            entity.Property(e => e.EmployeeId).HasColumnName("employee_id");
            entity.Property(e => e.PublishedId).HasColumnName("published_id");
            entity.Property(e => e.ReplacedEmployeeId).HasColumnName("replaced_employee_id");
            entity.Property(e => e.Source)
                .HasMaxLength(30)
                .HasColumnName("source");

            entity.HasOne(d => d.Admin).WithMany(p => p.AssignmentAdmins)
                .HasForeignKey(d => d.AdminId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("assignment_admin_fk");

            entity.HasOne(d => d.DatedSlot).WithMany(p => p.Assignments)
                .HasForeignKey(d => d.DatedSlotId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("assignment_dated_slot_fk");

            entity.HasOne(d => d.Employee).WithMany(p => p.AssignmentEmployees)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("assignment_employee_fk");

            entity.HasOne(d => d.Published).WithMany(p => p.Assignments)
                .HasForeignKey(d => d.PublishedId)
                .HasConstraintName("assignment_published_fk");

            entity.HasOne(d => d.ReplacedEmployee).WithMany(p => p.AssignmentReplacedEmployees)
                .HasForeignKey(d => d.ReplacedEmployeeId)
                .HasConstraintName("assignment_replaced_employee_fk");
        });

        modelBuilder.Entity<Certification>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("certification_pk");

            entity.ToTable("certification", "airport");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.ValidForMonths).HasColumnName("valid_for_months");
        });

        modelBuilder.Entity<Choice>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("choice_pk");

            entity.ToTable("choice", "airport");

            entity.HasIndex(e => new { e.RequestId, e.DayOfWeekId, e.Rank }, "choice_unique").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AdminId).HasColumnName("admin_id");
            entity.Property(e => e.DayOfWeekId).HasColumnName("day_of_week_id");
            entity.Property(e => e.LocationId).HasColumnName("location_id");
            entity.Property(e => e.MatchedDate).HasColumnName("matched_date");
            entity.Property(e => e.PositionId).HasColumnName("position_id");
            entity.Property(e => e.Rank).HasColumnName("rank");
            entity.Property(e => e.RequestId).HasColumnName("request_id");
            entity.Property(e => e.TemplateSlotId).HasColumnName("template_slot_id");

            entity.HasOne(d => d.Admin).WithMany(p => p.Choices)
                .HasForeignKey(d => d.AdminId)
                .HasConstraintName("choice_admin_fk");

            entity.HasOne(d => d.DayOfWeek).WithMany(p => p.Choices)
                .HasForeignKey(d => d.DayOfWeekId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("choice_day_of_week_fk");

            entity.HasOne(d => d.Location).WithMany(p => p.Choices)
                .HasForeignKey(d => d.LocationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("choice_location_fk");

            entity.HasOne(d => d.Position).WithMany(p => p.Choices)
                .HasForeignKey(d => d.PositionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("choice_position_fk");

            entity.HasOne(d => d.Request).WithMany(p => p.Choices)
                .HasForeignKey(d => d.RequestId)
                .HasConstraintName("choice_request_fk");

            entity.HasOne(d => d.TemplateSlot).WithMany(p => p.Choices)
                .HasForeignKey(d => d.TemplateSlotId)
                .HasConstraintName("choice_template_slot_fk");
        });

        modelBuilder.Entity<DatedSlot>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("dated_slot_pk");

            entity.ToTable("dated_slot", "airport");

            entity.HasIndex(e => new { e.WeekId, e.LocationId, e.PositionId, e.DayOfWeekId, e.SeatNumber }, "dated_slot_unique").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DayOfWeekId).HasColumnName("day_of_week_id");
            entity.Property(e => e.LocationId).HasColumnName("location_id");
            entity.Property(e => e.PositionId).HasColumnName("position_id");
            entity.Property(e => e.SeatNumber).HasColumnName("seat_number");
            entity.Property(e => e.WeekId).HasColumnName("week_id");
            entity.Property(e => e.WorkDate).HasColumnName("work_date");

            entity.HasOne(d => d.DayOfWeek).WithMany(p => p.DatedSlots)
                .HasForeignKey(d => d.DayOfWeekId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("dated_slot_day_of_week_fk");

            entity.HasOne(d => d.Location).WithMany(p => p.DatedSlots)
                .HasForeignKey(d => d.LocationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("dated_slot_location_fk");

            entity.HasOne(d => d.Position).WithMany(p => p.DatedSlots)
                .HasForeignKey(d => d.PositionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("dated_slot_position_fk");

            entity.HasOne(d => d.Week).WithMany(p => p.DatedSlots)
                .HasForeignKey(d => d.WeekId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("dated_slot_week_fk");
        });

        modelBuilder.Entity<DayOfWeek>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("day_of_week_pk");

            entity.ToTable("day_of_week", "airport");

            entity.HasIndex(e => e.Name, "day_of_week_name_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("employee_pk");

            entity.ToTable("employee", "airport");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.Role)
                .HasMaxLength(50)
                .HasColumnName("role");
        });

        modelBuilder.Entity<EmployeeCertification>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("employee_certification_pk");

            entity.ToTable("employee_certification", "airport");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AdminId).HasColumnName("admin_id");
            entity.Property(e => e.CertificationId).HasColumnName("certification_id");
            entity.Property(e => e.DocumentReference)
                .HasMaxLength(500)
                .HasColumnName("document_reference");
            entity.Property(e => e.EmployeeId).HasColumnName("employee_id");
            entity.Property(e => e.ExpirationDate).HasColumnName("expiration_date");
            entity.Property(e => e.IssueDate).HasColumnName("issue_date");
            entity.Property(e => e.ReviewDate).HasColumnName("review_date");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValueSql("'pending'::character varying")
                .HasColumnName("status");

            entity.HasOne(d => d.Admin).WithMany(p => p.EmployeeCertificationAdmins)
                .HasForeignKey(d => d.AdminId)
                .HasConstraintName("employee_certification_reviewed_by_fk");

            entity.HasOne(d => d.Certification).WithMany(p => p.EmployeeCertifications)
                .HasForeignKey(d => d.CertificationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("employee_certification_certification_fk");

            entity.HasOne(d => d.Employee).WithMany(p => p.EmployeeCertificationEmployees)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("employee_certification_employee_fk");
        });

        modelBuilder.Entity<Location>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("location_pk");

            entity.ToTable("location", "airport");

            entity.HasIndex(e => e.Code, "location_code_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Code)
                .HasMaxLength(50)
                .HasColumnName("code");
            entity.Property(e => e.Type)
                .HasMaxLength(50)
                .HasColumnName("type");
        });

        modelBuilder.Entity<Position>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("position_pk");

            entity.ToTable("position", "airport");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Title)
                .HasMaxLength(100)
                .HasColumnName("title");
        });

        modelBuilder.Entity<PositionCertification>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("position_certification_pk");

            entity.ToTable("position_certification", "airport");

            entity.HasIndex(e => new { e.PositionId, e.CertificationId }, "position_certification_unique").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CertificationId).HasColumnName("certification_id");
            entity.Property(e => e.PositionId).HasColumnName("position_id");

            entity.HasOne(d => d.Certification).WithMany(p => p.PositionCertifications)
                .HasForeignKey(d => d.CertificationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("position_certification_certification_fk");

            entity.HasOne(d => d.Position).WithMany(p => p.PositionCertifications)
                .HasForeignKey(d => d.PositionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("position_certification_position_fk");
        });

        modelBuilder.Entity<Published>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("published_pk");

            entity.ToTable("published", "airport");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AdminId).HasColumnName("admin_id");
            entity.Property(e => e.Kind)
                .HasMaxLength(50)
                .HasColumnName("kind");
            entity.Property(e => e.PublishedAt)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("published_at");
            entity.Property(e => e.WeekId).HasColumnName("week_id");

            entity.HasOne(d => d.Admin).WithMany(p => p.Publisheds)
                .HasForeignKey(d => d.AdminId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("published_admin_fk");

            entity.HasOne(d => d.Week).WithMany(p => p.Publisheds)
                .HasForeignKey(d => d.WeekId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("published_week_fk");
        });

        modelBuilder.Entity<Template>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("template_pk");

            entity.ToTable("template", "airport");

            entity.HasIndex(e => new { e.LocationId, e.PositionId, e.DayOfWeekId, e.SeatNumber }, "template_unique").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DayOfWeekId).HasColumnName("day_of_week_id");
            entity.Property(e => e.LocationId).HasColumnName("location_id");
            entity.Property(e => e.PositionId).HasColumnName("position_id");
            entity.Property(e => e.RegularEmployeeId).HasColumnName("regular_employee_id");
            entity.Property(e => e.SeatNumber).HasColumnName("seat_number");

            entity.HasOne(d => d.DayOfWeek).WithMany(p => p.Templates)
                .HasForeignKey(d => d.DayOfWeekId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("template_day_of_week_fk");

            entity.HasOne(d => d.Location).WithMany(p => p.Templates)
                .HasForeignKey(d => d.LocationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("template_location_fk");

            entity.HasOne(d => d.Position).WithMany(p => p.Templates)
                .HasForeignKey(d => d.PositionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("template_position_fk");

            entity.HasOne(d => d.RegularEmployee).WithMany(p => p.Templates)
                .HasForeignKey(d => d.RegularEmployeeId)
                .HasConstraintName("template_employee_fk");
        });

        modelBuilder.Entity<Week>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("week_pk");

            entity.ToTable("week", "airport");

            entity.HasIndex(e => e.MondayDate, "week_unique").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.MondayDate).HasColumnName("monday_date");
        });

        modelBuilder.Entity<WeekRequest>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("week_request_pk");

            entity.ToTable("week_request", "airport");

            entity.HasIndex(e => e.EmployeeId, "week_request_unique").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.EmployeeId).HasColumnName("employee_id");
            entity.Property(e => e.LastMatchedDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("last_matched_date");
            entity.Property(e => e.RequestDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("request_date");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasColumnName("status");

            entity.HasOne(d => d.Employee).WithOne(p => p.WeekRequest)
                .HasForeignKey<WeekRequest>(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("week_request_employee_fk");
        });

        modelBuilder.Entity<WeekResponse>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("week_response_pk");

            entity.ToTable("week_response", "airport");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Comments)
                .HasMaxLength(250)
                .HasColumnName("comments");
            entity.Property(e => e.EmployeeId).HasColumnName("employee_id");
            entity.Property(e => e.PublishedId).HasColumnName("published_id");
            entity.Property(e => e.Response)
                .HasMaxLength(50)
                .HasColumnName("response");
            entity.Property(e => e.ResponseDate).HasColumnName("response_date");

            entity.HasOne(d => d.Employee).WithMany(p => p.WeekResponses)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("week_response_employee_fk");

            entity.HasOne(d => d.Published).WithMany(p => p.WeekResponses)
                .HasForeignKey(d => d.PublishedId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("week_response_published_fk");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
