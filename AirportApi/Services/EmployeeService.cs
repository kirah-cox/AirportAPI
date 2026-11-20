using AirportApi.Data;
using AirportApi.Models;
using Microsoft.EntityFrameworkCore;

namespace AirportApi.Services;

public class EmployeeService(AirportContext db)
{
    public Task<List<Employee>> GetAllAsync() =>
        db.Employees.AsNoTracking().OrderBy(e => e.Name).ToListAsync();

    public Task<Employee?> GetByIdAsync(int id) =>
        db.Employees.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);
}
