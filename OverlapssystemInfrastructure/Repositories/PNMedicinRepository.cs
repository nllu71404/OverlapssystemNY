using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using OverlapssystemDomain.Entities;
using OverlapssystemDomain.Interfaces;

namespace OverlapssystemInfrastructure.Repositories
{
    public class PNMedicinRepository : IPNMedicinRepository
    {
        private readonly string _connectionString;

        public PNMedicinRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ProjektDB")
                ?? throw new InvalidOperationException("Connection string 'ProjektDB' not found.");
        }

        public async Task<List<PNMedicinModel>> GetAllPNMedicinAsync()
        {
            List<PNMedicinModel> pNMedicinTimes = new();

            using SqlConnection connection = new SqlConnection(_connectionString);
            using SqlCommand command = new SqlCommand("dbo.uspGetPNMedicinTimes", connection);

            command.CommandType = CommandType.StoredProcedure;

            await connection.OpenAsync();

            using SqlDataReader reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                PNMedicinModel pNMedicinTime = new PNMedicinModel
                {
                    // Konverter PNMedicinTimeID til int, da det ikke er nullable i modellen
                    PNMedicinID = Convert.ToInt32(reader["PNID"]),

                    // Hvis ResidentID er null i databasen, sæt det til null i modellen, ellers konverter det til int
                    ResidentID = reader["ResidentID"] == DBNull.Value ? null : Convert.ToInt32(reader["ResidentID"]),

                    // Hent PNTime som DateTime, og hvis det er null, sæt det til DateTime.MinValue
                    PNTime = reader["PNTime"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(reader["PNTime"]),


                    // Hent PNTimeStamp som DateTime?, og hvis det er null, sæt det til null i modellen
                    PNTimeStamp = reader["PNTimeStamp"] == DBNull.Value ? null : Convert.ToDateTime(reader["PNTimeStamp"])

                };

                pNMedicinTimes.Add(pNMedicinTime);
            }

            return pNMedicinTimes;
        }
        

        public async Task<List<PNMedicinModel>> GetPNMedicinByResidentIdAsync(int residentId)
        {
            List<PNMedicinModel> pNMedicinTimes = new();

            using SqlConnection connection = new SqlConnection(_connectionString);
            using SqlCommand command = new SqlCommand("dbo.uspGetPNMedicinTimesByResidentID", connection);

            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@ResidentID", SqlDbType.Int).Value = residentId;

            await connection.OpenAsync();

            using SqlDataReader reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                PNMedicinModel medicinTime = new PNMedicinModel
                {
                    PNMedicinID = Convert.ToInt32(reader["PNID"]),
                    ResidentID = reader["ResidentID"] == DBNull.Value ? null : Convert.ToInt32(reader["ResidentID"]),
                    PNTime = reader["PNTime"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(reader["PNTime"]),
                    PNTimeStamp = reader["PNTimeStamp"] == DBNull.Value ? null : Convert.ToDateTime(reader["PNTimeStamp"])
                };

                pNMedicinTimes.Add(medicinTime);
            }

            return pNMedicinTimes;
        }

        public async Task<int> SaveNewPNMedicinAsync(PNMedicinModel pNMedicin)
        {
            using SqlConnection connection = new SqlConnection(_connectionString);
            using SqlCommand command = new SqlCommand("dbo.uspCreatePNMedicinTime", connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add("@ResidentID", SqlDbType.Int).Value = pNMedicin.ResidentID;
            command.Parameters.Add("@PNTime", SqlDbType.DateTime).Value = pNMedicin.PNTime;
            command.Parameters.Add("@PNTimeStamp", SqlDbType.DateTime).Value =
                pNMedicin.PNTimeStamp.HasValue
                    ? pNMedicin.PNTimeStamp.Value
                    : DBNull.Value;

            await connection.OpenAsync();
            object? result = await command.ExecuteScalarAsync();
            return Convert.ToInt32(result);
        }

        public async Task UpdatePNMedicinAsync(PNMedicinModel pNMedicin)
        {
            using SqlConnection connection = new SqlConnection(_connectionString);
            using SqlCommand command = new SqlCommand("dbo.uspUpdatePNMedicinTimeById", connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add("@ResidentID", SqlDbType.Int).Value = pNMedicin.ResidentID;
            command.Parameters.Add("@PNTime", SqlDbType.DateTime).Value = pNMedicin.PNTime;
            command.Parameters.Add("@PNTimeStamp", SqlDbType.DateTime).Value =
                pNMedicin.PNTimeStamp.HasValue
                    ? pNMedicin.PNTimeStamp.Value
                    : DBNull.Value;

            await connection.OpenAsync();
            await command.ExecuteNonQueryAsync();
        }

        public async Task DeletePNMedicinAsync(int pNMedicinId)
        {
            using SqlConnection connection = new SqlConnection(_connectionString);
            using SqlCommand command = new SqlCommand("dbo.uspDeletePNMedicinTime", connection);

            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@PNID", SqlDbType.Int).Value = pNMedicinId;

            await connection.OpenAsync();
            await command.ExecuteNonQueryAsync();
        }
    }
}
