using appointment.Mapper;
using appointment.Model.Dto;
using appointment.Repository;

namespace appointment.Service;

public class AppointmentService : IAppointmentService
{
    private readonly IAppointmentRepository _appointmentRepository;
    private readonly IPatientRepository _patientRepository;
    private readonly IDoctorRepository _doctorRepository;
    private readonly IAppointmentServiceRepository _appointmentServiceRepository;

    public AppointmentService(IAppointmentRepository appointmentRepository, IPatientRepository patientRepository,
        IDoctorRepository doctorRepository, IAppointmentServiceRepository appointmentServiceRepository)
    {
        _appointmentRepository = appointmentRepository;
        _patientRepository = patientRepository;
        _doctorRepository = doctorRepository;
        _appointmentServiceRepository = appointmentServiceRepository;
    }

    public async Task<AppointmentDto> GetAppointmentByIdAsync(int id, CancellationToken cancellationToken)
    {
        if (!await _appointmentRepository.CheckIfAppointmentExistsAsync(id, cancellationToken))
        {
            throw new Exception($"Appointment with id - {id} not found");
        }

        var appointment = await _appointmentRepository.GetAppointmentByIdAsync(id, cancellationToken);
        var patient = await _patientRepository.GetPatientByIdAsync(id, cancellationToken);
        var doctor = await _doctorRepository.GetDoctorByIdAsync(id, cancellationToken);
        var appServices =
            await _appointmentServiceRepository.GetAppointmentServicesForAppointmentByIdAsync(id, cancellationToken);

        var patientDto = PatientMapper.ToPatientDto(patient);
        var doctorDto = DoctorMapper.ToDoctorDto(doctor);
        var appServDtos = appServices.Select(AppointmentServiceMapper.ToAppointmentServiceDto).ToList();
        var appointmentDto = AppointmentMapper.ToAppointmentDto(appointment, patientDto, doctorDto, appServDtos);
        return appointmentDto;
    }
}