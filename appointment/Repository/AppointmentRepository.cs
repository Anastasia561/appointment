using appointment.Model;
using Microsoft.Data.SqlClient;

namespace appointment.Repository;

public class AppointmentRepository : IAppointmentRepository
{
    private readonly string _connectionString;

    public AppointmentRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("Default");
    }

    public async Task<Appointment> GetAppointmentByIdAsync(int id, CancellationToken cancellationToken)
    {
        var con = new SqlConnection(_connectionString);
        var com = new SqlCommand();

        com.Connection = con;
        com.CommandText = "select * from Appointment where appointment_id = @id";
        com.Parameters.AddWithValue("@id", id);

        await con.OpenAsync(cancellationToken);
        var appointment = new Appointment();

        await using (var reader = await com.ExecuteReaderAsync(cancellationToken))
        {
            while (await reader.ReadAsync(cancellationToken))
            {
                appointment.Id = (int)reader["doctor_id"];
                appointment.PatientId = (int)reader["patient_id"];
                appointment.DoctorId = (int)reader["doctor_id"];
                appointment.Date = (DateTime)reader["date"];
            }
        }

        await con.DisposeAsync();
        return appointment;
    }

    public async Task<bool> CheckIfAppointmentExistsAsync(int id, CancellationToken cancellationToken)
    {
        var con = new SqlConnection();
        var com = new SqlCommand();

        com.Connection = con;
        com.CommandText = "select count(*) from Appointment where appointment_id = @id";
        com.Parameters.AddWithValue("@id", id);

        await con.OpenAsync(cancellationToken);

        var result = (int)await com.ExecuteScalarAsync(cancellationToken);

        await con.DisposeAsync();
        return result > 0;
    }


    private async Task<int> GetMaxAppointmentIdAsync(CancellationToken cancellationToken)
    {
        var con = new SqlConnection();
        var com = new SqlCommand();

        com.Connection = con;
        com.CommandText = "select max(appointment_id) from Appointment)";

        await con.OpenAsync(cancellationToken);

        var result = (int)await com.ExecuteScalarAsync(cancellationToken);

        await con.DisposeAsync();
        return result;
    }
}