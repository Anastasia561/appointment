using appointment.Model;
using appointment.Model.Dto;
using Microsoft.Data.SqlClient;

namespace appointment.Repository;

public class AppointmentServiceRepository : IAppointmentServiceRepository
{
    private readonly string _connectionString;

    public AppointmentServiceRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("Default");
    }

    public async Task<IEnumerable<AppointmentService>> GetAppointmentServicesForAppointmentByIdAsync(int id,
        CancellationToken cancellationToken)
    {
        var con = new SqlConnection(_connectionString);
        var com = new SqlCommand();

        com.Connection = con;
        com.CommandText =
            "select * from Service s join Appointment_Service a on s.service_id = a.service_id where appointment_id=@id";
        com.Parameters.AddWithValue("@id", id);
        await con.OpenAsync(cancellationToken);
        var appServices = new List<AppointmentService>();

        await using (var reader = await com.ExecuteReaderAsync(cancellationToken))
        {
            while (await reader.ReadAsync(cancellationToken))
            {
                var a = new AppointmentService()
                {
                    Name = reader["name"].ToString(),
                    ServiceFee = (double)reader["service_fee"]
                };
                appServices.Add(a);
            }
        }

        await con.DisposeAsync();
        return appServices;
    }
}