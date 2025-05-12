using appointment.Model;
using appointment.Model.Dto;

namespace appointment.Mapper;

public class AppointmentMapper
{
    public static AppointmentDto ToAppointmentDto(Appointment appointment, PatientDto patient, DoctorDto doctor,
        List<AppointmentServiceDto> appServices)
    {
        return new AppointmentDto()
        {
            Date = appointment.Date,
            Patient = patient,
            Doctor = doctor,
            AppointmentServices = appServices
        };
    }
}