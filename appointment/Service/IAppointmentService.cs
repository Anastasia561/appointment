using appointment.Model;
using appointment.Model.Dto;

namespace appointment.Service;

public interface IAppointmentService
{
    Task<AppointmentDto> GetAppointmentByIdAsync(int id, CancellationToken cancellationToken);
}