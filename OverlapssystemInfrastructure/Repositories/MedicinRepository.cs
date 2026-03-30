using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using OverlapssystemDomain.Entities;
using OverlapssystemDomain.Interfaces;
using System.Data;

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

        public Task DeleteMedicinAsync(int medicinId)
        {
            throw new NotImplementedException();
        }

        public Task<List<MedicinModel>> GetAllMedicinAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<MedicinModel>> GetMedicinByResidentIdAsync(int residentId)
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