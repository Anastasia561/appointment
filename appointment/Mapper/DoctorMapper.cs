using appointment.Model;
using appointment.Model.Dto;

namespace appointment.Mapper;

public class DoctorMapper
{
    public static DoctorDto ToDoctorDto(Doctor doctor)
    {
        return new DoctorDto()
        {
            Id = doctor.Id,
            PWZ = doctor.PWZ
        };
    }
}