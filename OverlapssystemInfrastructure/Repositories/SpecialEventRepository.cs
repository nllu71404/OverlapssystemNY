using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using OverlapssystemDomain.Entities;
using OverlapssystemDomain.Enums;
using OverlapssystemDomain.Interfaces;

namespace OverlapssystemInfrastructure.Repositories
{
    public class SpecialEventRepository : ISpecialEventRepository
    {
        private readonly string _connectionString;
        public SpecialEventRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ProjektDB")
                ?? throw new InvalidOperationException("Connection string 'ProjektDB' not found.");
        }

        public async Task<List<SpecialEventModel>> GetAllSpecialEvents()
        {
            List<SpecialEventModel> specialEvents = new();

            using SqlConnection connection = new SqlConnection(_connectionString);
            using SqlCommand command = new SqlCommand("dbo.uspGetAllSpecialEvent", connection);

            command.CommandType = CommandType.StoredProcedure;

            await connection.OpenAsync();
            using SqlDataReader reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                SpecialEventModel specialEvent = new SpecialEventModel
                {
                    SpecialEventID = Convert.ToInt32(reader["SpecialEventID"]),
                    ResidentID = reader["ResidentID"] == DBNull.Value ? null : Convert.ToInt32(reader["ResidentID"]),
                    SpecialEventNote = reader["SpecialEventNote"]?.ToString() ?? "",
                    SpecialEventDateTime = reader["SpecialEventDateTime"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["SpecialEvent"])
                };
                specialEvents.Add(specialEvent);
            }
            return specialEvents;
        }
        public async Task<List<SpecialEventModel>> GetSpecialEventByResidentId(int residentId)
        {
            List<SpecialEventModel> specialEventModel = new();

            using SqlConnection connection = new SqlConnection(_connectionString);
            using SqlCommand command = new SqlCommand("dbo.uspGetSpecialEventByResidentID", connection);

            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@ResidentID", SqlDbType.Int).Value = residentId;

            await connection.OpenAsync();

            using SqlDataReader reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                SpecialEventModel specialEvent = new SpecialEventModel
                {
                    SpecialEventID = Convert.ToInt32(reader["SpecialEventID"]),
                    ResidentID = reader["ReaderID"] == DBNull.Value ? null : Convert.ToInt32(reader["ResidentID"]),
                    SpecialEventNote = reader["SpecialEventNote"]?.ToString() ?? "",
                    SpecialEventDateTime = reader["SpecialEvent"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["SpecialEvent"])
                };
                specialEventModel.Add(specialEvent);
            }
            return specialEventModel;
        }
        public async Task<int> SaveNewSpecialEvent(SpecialEventModel specialEvent)
        {
            using SqlConnection connection = new SqlConnection(_connectionString);
            using SqlCommand command = new SqlCommand("dbo.uspCreateSpecialEvent", connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add("@ResidentID", SqlDbType.Int).Value = specialEvent.ResidentID;

            command.Parameters.Add("@SpecialEventNote", SqlDbType.NVarChar, 250).Value = string.IsNullOrWhiteSpace(specialEvent.SpecialEventNote)
                ? DBNull.Value : specialEvent.SpecialEventNote;

            command.Parameters.Add("@SpecialEventDateTime", SqlDbType.DateTime).Value =
                specialEvent.SpecialEventDateTime.HasValue
                    ? specialEvent.SpecialEventDateTime.Value
                    : DBNull.Value;

            await connection.OpenAsync();
            object? result = await command.ExecuteScalarAsync();
            return Convert.ToInt32(result);

        }
        public async Task UpdateSpecialEvent(SpecialEventModel specialEvent)
        {
            using SqlConnection connection = new SqlConnection(_connectionString);
            using SqlCommand command = new SqlCommand("dbo.uspUpdateSpecialEventById", connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add("@SpecialEventID", SqlDbType.Int).Value = specialEvent.SpecialEventID;
            
            command.Parameters.Add("@SpecialEventNote", SqlDbType.NVarChar, 250).Value = string.IsNullOrWhiteSpace(specialEvent.SpecialEventNote)
                ? DBNull.Value : specialEvent.SpecialEventNote;

            command.Parameters.Add("@SpecialEventDateTime", SqlDbType.DateTime).Value =
                specialEvent.SpecialEventDateTime.HasValue
                    ? specialEvent.SpecialEventDateTime.Value
                    : DBNull.Value;

            await connection.OpenAsync();
            await command.ExecuteNonQueryAsync();

        }
        public async Task DeleteSpecialEvent(int specialEventID)
        {
            using SqlConnection connection = new SqlConnection(_connectionString);
            using SqlCommand command = new SqlCommand("dbo.uspDeleteSpecialEvent", connection);

            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@SpecialEventID", SqlDbType.Int).Value = specialEventID;

            await connection.OpenAsync();
            await command.ExecuteNonQueryAsync();
        }

    }
}
