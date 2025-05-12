using appointment.Model.Dto;

namespace appointment.Model;

public class AppointmentCreationRequestDto
{
    public int AppointmentId { get; set; }
    public int PatientId { get; set; }
    public string PWZ { get; set; }
    public List<AppointmentServiceDto> Services { get; set; }
}