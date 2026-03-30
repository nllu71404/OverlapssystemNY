using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using OverlapssystemDomain.Entities;
using OverlapssystemDomain.Enums;
using OverlapssystemDomain.Interfaces;

namespace OverlapssystemInfrastructure.Repositories
{
    public class MedicinRepository : IMedicinRepository
    {
        private readonly string _connectionString;

        public MedicinRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ProjektDB")
                ?? throw new InvalidOperationException("Connection string 'ProjektDB' not found.");
        }

        public async Task<List<MedicinModel>> GetAllMedicinAsync()
        {
            List<MedicinModel> medicinTimes = new();

            using SqlConnection connection = new SqlConnection(_connectionString);
            using SqlCommand command = new SqlCommand("dbo.uspGetMedicinTimes", connection);

            command.CommandType = CommandType.StoredProcedure;

            await connection.OpenAsync();

            using SqlDataReader reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                MedicinModel medicinTime = new MedicinModel
                {
                    // Konverter MedicinTimeID til int, da det ikke er nullable i modellen
                    MedicinTimeID = Convert.ToInt32(reader["MedicinTimeID"]),

                    // Hvis ResidentID er null i databasen, sæt det til null i modellen, ellers konverter det til int
                    ResidentID = reader["ResidentID"] == DBNull.Value ? null : Convert.ToInt32(reader["ResidentID"]),

                    // Hent MedicinTime som DateTime, og hvis det er null, sæt det til DateTime.MinValue
                    MedicinTime = reader["MedicinTime"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(reader["MedicinTime"]),


                    // Hent MedicinCheckTimeStamp som DateTime?, og hvis det er null, sæt det til null i modellen
                    MedicinCheckTimeStamp = reader["MedicinCheckTimeStamp"] == DBNull.Value ? null : Convert.ToDateTime(reader["MedicinCheckTimeStamp"])

                };

                medicinTimes.Add(medicinTime);
            }

            return medicinTimes;
        }

        public Task<List<MedicinModel>> GetMedicinByResidentIdAsync(int residentId)
        {
            throw new NotImplementedException();
        }

        public Task DeleteMedicinAsync(int medicinId)
        {
            throw new NotImplementedException();
        }

        public async Task SaveNewMedicinAsync(MedicinModel medicin)
        {
            using SqlConnection connection = new SqlConnection(_connectionString);
            using SqlCommand command = new SqlCommand("dbo.uspCreateMedicin", connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add("@ResidentID", SqlDbType.Int).Value = medicin.ResidentID;
            command.Parameters.Add("@MedicinTime", SqlDbType.DateTime).Value = medicin.MedicinTime;
            command.Parameters.Add("@MedicinCheckTimeStamp", SqlDbType.DateTime).Value =
                medicin.MedicinCheckTimeStamp.HasValue
                    ? medicin.MedicinCheckTimeStamp.Value
                    : DBNull.Value;

            await connection.OpenAsync();
            await command.ExecuteScalarAsync();
        }

        public Task ToggleMedicinGivenAsync(MedicinModel medTime)
        {
            throw new NotImplementedException();
        }

        public Task UpdateMedicinAsync(MedicinModel medicin)
        {
            throw new NotImplementedException();
        }
    }
}
