using appointment.Model;
using Microsoft.Data.SqlClient;

namespace appointment.Repository;

public class DoctorRepository : IDoctorRepository
{
    private readonly string _connectionString;

    public DoctorRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("Default");
    }

    public async Task<Doctor> GetDoctorByIdAsync(int id, CancellationToken cancellationToken)
    {
        var con = new SqlConnection(_connectionString);
        var com = new SqlCommand();

        com.Connection = con;
        com.CommandText = "Select * from Doctor where doctor_id = @id";
        com.Parameters.AddWithValue("@id", id);
        await con.OpenAsync(cancellationToken);
        var doctor = new Doctor();

        await using (var reader = await com.ExecuteReaderAsync(cancellationToken))
        {
            while (await reader.ReadAsync(cancellationToken))
            {
                doctor.Id = (int)reader["doctor_id"];
                doctor.FirstName = (string)reader["first_name"];
                doctor.LastName = (string)reader["last_name"];
                doctor.PWZ = (string)reader["PWZ"];
            }
        }

        await con.DisposeAsync();
        return doctor;
    }
}