using appointment.Model;
using Microsoft.Data.SqlClient;

namespace appointment.Repository;

public class PatientRepository : IPatientRepository
{
    private readonly string _connectionString;

    public PatientRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("Default");
    }

    public async Task<Patient> GetPatientByIdAsync(int id, CancellationToken cancellationToken)
    {
        var con = new SqlConnection(_connectionString);
        var com = new SqlCommand();

        com.Connection = con;
        com.CommandText = "Select * from Patient where patient_id = @id";
        com.Parameters.AddWithValue("@id", id);
        await con.OpenAsync(cancellationToken);
        var patient = new Patient();

        await using (var reader = await com.ExecuteReaderAsync(cancellationToken))
        {
            while (await reader.ReadAsync(cancellationToken))
            {
                patient.Id = (int)reader["patient_id"];
                patient.FirstName = (string)reader["first_name"];
                patient.LastName = (string)reader["last_name"];
                patient.DateOfBirth = (DateTime)reader["date_of_birth"];
            }
        }

        await con.DisposeAsync();
        return patient;
    }
}