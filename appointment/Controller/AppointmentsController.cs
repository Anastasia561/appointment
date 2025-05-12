using appointment.Model;
using appointment.Repository;
using appointment.Service;
using Microsoft.AspNetCore.Mvc;

namespace appointment.Controller;

[ApiController]
[Route("api/[controller]")]
public class AppointmentsController : ControllerBase
{
    private readonly IAppointmentService _appointmentService;

    public AppointmentsController(IAppointmentService appointmentService)
    {
        _appointmentService = appointmentService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAppointmentById(int id, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _appointmentService.GetAppointmentByIdAsync(id, cancellationToken);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}