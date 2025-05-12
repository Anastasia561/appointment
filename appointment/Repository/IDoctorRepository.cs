using appointment.Model;

namespace appointment.Repository;

public interface IDoctorRepository
{
    Task<Doctor> GetDoctorByIdAsync(int id, CancellationToken cancellationToken);
}