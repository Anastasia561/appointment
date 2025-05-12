using appointment.Model.Dto;

namespace appointment.Mapper;

public class AppointmentServiceMapper
{
    public static AppointmentServiceDto ToAppointmentServiceDto(AppointmentService appointmentService)
    {
        return new AppointmentServiceDto()
        {
            Name = appointmentService.Name,
            ServiceFee = appointmentService.ServiceFee
        };
    }
}