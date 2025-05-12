using appointment.Model;

namespace appointment.Repository;

public interface IAppointmentRepository
{
    Task<Appointment> GetAppointmentByIdAsync(int id, CancellationToken cancellationToken);
    Task<bool> CheckIfAppointmentExistsAsync(int id, CancellationToken cancellationToken);
}