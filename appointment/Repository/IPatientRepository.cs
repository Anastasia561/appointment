using appointment.Model;

namespace appointment.Repository;

public interface IPatientRepository
{
    Task<Patient> GetPatientByIdAsync(int id, CancellationToken cancellationToken);
}