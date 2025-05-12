using appointment.Model;
using appointment.Model.Dto;

namespace appointment.Mapper;

public class PatientMapper
{
    public static PatientDto ToPatientDto(Patient patient)
    {
        return new PatientDto()
        {
            FirstName = patient.FirstName,
            LastName = patient.LastName,
            DateOfBirth = patient.DateOfBirth,
        };
    }
}