using appointment.Model.Dto;

namespace appointment.Repository;

public interface IAppointmentServiceRepository
{
    Task<IEnumerable<AppointmentService>> GetAppointmentServicesForAppointmentByIdAsync(int id,
        CancellationToken cancellationToken);
}