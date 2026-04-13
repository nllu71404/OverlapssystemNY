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
                    PNMedicinID = Convert.ToInt32(reader["PNID"]),

                    ResidentID = reader["ResidentID"] == DBNull.Value
                        ? null
                        : Convert.ToInt32(reader["ResidentID"]),

                    PNTime = reader["PNTime"] == DBNull.Value
                        ? (DateTime?)null
                        : Convert.ToDateTime(reader["PNTime"]),

                    PNTimeStamp = reader["PNTimeStamp"] == DBNull.Value
                        ? null
                        : Convert.ToDateTime(reader["PNTimeStamp"]),

                    Reason = reader["Reason"]?.ToString() ?? ""
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

                    ResidentID = reader["ResidentID"] == DBNull.Value
                        ? null
                        : Convert.ToInt32(reader["ResidentID"]),

                    PNTime = reader["PNTime"] == DBNull.Value
                        ? (DateTime?)null
                        : Convert.ToDateTime(reader["PNTime"]),

                    PNTimeStamp = reader["PNTimeStamp"] == DBNull.Value
                        ? null
                        : Convert.ToDateTime(reader["PNTimeStamp"]),

                    Reason = reader["Reason"]?.ToString() ?? ""
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

            command.Parameters.Add("@PNTime", SqlDbType.DateTime).Value =
                pNMedicin.PNTime.HasValue
                    ? pNMedicin.PNTime.Value
                    : DBNull.Value;

            command.Parameters.Add("@PNTimeStamp", SqlDbType.DateTime).Value =
                pNMedicin.PNTimeStamp.HasValue
                    ? pNMedicin.PNTimeStamp.Value
                    : DBNull.Value;

            command.Parameters.Add("@Reason", SqlDbType.NVarChar, 250).Value =
                string.IsNullOrWhiteSpace(pNMedicin.Reason)
                    ? DBNull.Value
                    : pNMedicin.Reason;

            await connection.OpenAsync();
            object? result = await command.ExecuteScalarAsync();
            return Convert.ToInt32(result);
        }

        public async Task UpdatePNMedicinAsync(PNMedicinModel pNMedicin)
        {
            using SqlConnection connection = new SqlConnection(_connectionString);
            using SqlCommand command = new SqlCommand("dbo.uspUpdatePNMedicinTimeById", connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add("@PNID", SqlDbType.Int).Value = pNMedicin.PNMedicinID;

            command.Parameters.Add("@PNTime", SqlDbType.DateTime).Value =
                pNMedicin.PNTime.HasValue
                    ? pNMedicin.PNTime.Value
                    : DBNull.Value;

            command.Parameters.Add("@PNTimeStamp", SqlDbType.DateTime).Value =
                pNMedicin.PNTimeStamp.HasValue
                    ? pNMedicin.PNTimeStamp.Value
                    : DBNull.Value;

            command.Parameters.Add("@Reason", SqlDbType.NVarChar, 250).Value =
                string.IsNullOrWhiteSpace(pNMedicin.Reason)
                    ? DBNull.Value
                    : pNMedicin.Reason;

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
