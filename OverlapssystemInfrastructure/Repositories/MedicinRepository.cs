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

        public async Task<List<MedicinModel>> GetMedicinByResidentIdAsync(int residentId)
        {
            List<MedicinModel> medicinTimes = new();

            using SqlConnection connection = new SqlConnection(_connectionString);
            using SqlCommand command = new SqlCommand("dbo.uspGetMedicinTimesByResidentID", connection);

            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@ResidentID", SqlDbType.Int).Value = residentId;

            await connection.OpenAsync();

            using SqlDataReader reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                MedicinModel medicinTime = new MedicinModel
                {
                    MedicinTimeID = Convert.ToInt32(reader["MedicinTimeID"]),
                    ResidentID = reader["ResidentID"] == DBNull.Value ? null : Convert.ToInt32(reader["ResidentID"]),
                    MedicinTime = reader["MedicinTime"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["MedicinTime"]),
                    MedicinCheckTimeStamp = reader["MedicinCheckTimeStamp"] == DBNull.Value ? null : Convert.ToDateTime(reader["MedicinCheckTimeStamp"])
                };

                medicinTimes.Add(medicinTime);
            }

            return medicinTimes;
        }

        public async Task DeleteMedicinAsync(int medicinId)
        {
            using SqlConnection connection = new SqlConnection(_connectionString);
            using SqlCommand command = new SqlCommand("dbo.uspDeleteMedicinTime", connection);

            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@MedicinTimeId", SqlDbType.Int).Value = medicinId;

            await connection.OpenAsync();
            await command.ExecuteNonQueryAsync();
        }

        public async Task<int> SaveNewMedicinAsync(MedicinModel medicin)
        {
            using SqlConnection connection = new SqlConnection(_connectionString);
            using SqlCommand command = new SqlCommand("dbo.uspCreateMedicinTime", connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add("@ResidentID", SqlDbType.Int).Value = medicin.ResidentID;
            command.Parameters.Add("@MedicinTime", SqlDbType.DateTime).Value =
    medicin.MedicinTime.HasValue
        ? medicin.MedicinTime.Value
        : DBNull.Value;
            command.Parameters.Add("@MedicinCheckTimeStamp", SqlDbType.DateTime).Value =
                medicin.MedicinCheckTimeStamp.HasValue
                    ? medicin.MedicinCheckTimeStamp.Value
                    : DBNull.Value;

            await connection.OpenAsync();
            object? result = await command.ExecuteScalarAsync();
            return Convert.ToInt32(result);
        }


        public async Task UpdateMedicinAsync(MedicinModel medicin)
        {
            using SqlConnection connection = new SqlConnection(_connectionString);
            using SqlCommand command = new SqlCommand("dbo.uspUpdateMedicinTimeById", connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add("@MedicinTime", SqlDbType.DateTime).Value = medicin.MedicinTime;
            command.Parameters.Add("@MedicinTimeID", SqlDbType.Int).Value = medicin.MedicinTimeID;
            command.Parameters.Add("@IsChecked", SqlDbType.Bit).Value = medicin.IsChecked;
            command.Parameters.Add("@MedicinCheckTimeStamp", SqlDbType.DateTime).Value =
                 medicin.MedicinCheckTimeStamp.HasValue
                     ? medicin.MedicinCheckTimeStamp.Value
                     : DBNull.Value;


            await connection.OpenAsync();
            await command.ExecuteNonQueryAsync();
        }

        public async Task<MedicinModel> GetMedicinByIdAsync(int medicinId)
        {
            MedicinModel medicinTime = new MedicinModel();

            using SqlConnection connection = new SqlConnection(_connectionString);
            using SqlCommand command = new SqlCommand("dbo.uspGetMedicinTimesByMedicinTimeID", connection);

            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@MedicinTimeID", SqlDbType.Int).Value = medicinId;

            await connection.OpenAsync();

            using SqlDataReader reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                MedicinModel newMedicinTime = new MedicinModel
                {
                    MedicinTimeID = Convert.ToInt32(reader["MedicinTimeID"]),
                    ResidentID = reader["ResidentID"] == DBNull.Value ? null : Convert.ToInt32(reader["ResidentID"]),
                    MedicinTime = reader["MedicinTime"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(reader["MedicinTime"]),
                    MedicinCheckTimeStamp = reader["MedicinCheckTimeStamp"] == DBNull.Value ? null : Convert.ToDateTime(reader["MedicinCheckTimeStamp"])
                };

                medicinTime = newMedicinTime;
            }
            return medicinTime;

        }

        public async Task SetMedicinCheckedAsync(int medicinTimeId, bool isChecked)
        {
            using SqlConnection connection = new SqlConnection(_connectionString);
            using SqlCommand command = new SqlCommand("dbo.uspSetMedicinChecked", connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add("@MedicinTimeID", SqlDbType.Int).Value = medicinTimeId;
            command.Parameters.Add("@IsChecked", SqlDbType.Bit).Value = isChecked;

            await connection.OpenAsync();
            await command.ExecuteNonQueryAsync();
        }

    }
}
