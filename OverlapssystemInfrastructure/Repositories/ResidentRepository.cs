using OverlapssystemDomain.Entities;
using OverlapssystemDomain.Enums;
using OverlapssystemDomain.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;


namespace OverlapssystemInfrastructure.Repositories
{
    public class ResidentRepository : IResidentRepository
    {
        private readonly string _connectionString;

        public ResidentRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ProjektDB")
                ?? throw new InvalidOperationException("Connection string 'ProjektDB' not found.");
        }

        // Dette er funktionen som bliver kaldt for at hente alle beboere, og den kalder på stored procedure i SQL Server
        public async Task<List<ResidentModel>> GetAllResidentsAsync()
        {
            List<ResidentModel> residents = new();

            using SqlConnection connection = new SqlConnection(_connectionString);
            using SqlCommand command = new SqlCommand("dbo.uspGetResidents", connection);

            command.CommandType = CommandType.StoredProcedure;

            await connection.OpenAsync();

            using SqlDataReader reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                ResidentModel resident = new ResidentModel
                {
                    // Konverter ResidentID til int, da det ikke er nullable i modellen
                    ResidentId = Convert.ToInt32(reader["ResidentID"]),

                    // Hvis DepartmentID er null i databasen, sæt det til null i modellen, ellers konverter det til int
                    DepartmentId = reader["DepartmentID"] == DBNull.Value ? null : Convert.ToInt32(reader["DepartmentID"]),

                    // Hent ResidentName som string, og hvis det er null, sæt det til en tom streng
                    Name = reader["ResidentName"]?.ToString() ?? "",

                    // Hent ResidentStatus som string, og hvis det er null, sæt det til en tom streng
                    Status = reader["ResidentStatus"]?.ToString() ?? "",

                    // Prøv at parse Risk til enum Risiko, og hvis det mislykkes, sæt det til Risiko.Green som default
                    Risiko = Enum.TryParse<Risiko>(reader["Risk"]?.ToString(), out var risiko) ? risiko : Risiko.Green
                };

                residents.Add(resident);
            }

            return residents;
        }
       
        public async Task<List<ResidentModel>> GetResidentByDepartmentIdAsync(int departmentId)
        {
            List<ResidentModel> residents = new();

            using SqlConnection connection = new SqlConnection(_connectionString);
            using SqlCommand command = new SqlCommand("dbo.uspGetResidentsByDepartmentID", connection);

            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@DepartmentID", SqlDbType.Int).Value = departmentId;

            await connection.OpenAsync();

            using SqlDataReader reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                ResidentModel resident = new ResidentModel
                {
                    ResidentId = Convert.ToInt32(reader["ResidentID"]),
                    DepartmentId = reader["DepartmentID"] == DBNull.Value ? null : Convert.ToInt32(reader["DepartmentID"]),
                    Name = reader["ResidentName"]?.ToString() ?? "",
                    Status = reader["ResidentStatus"]?.ToString() ?? "",
                    Risiko = Enum.TryParse<Risiko>(reader["Risk"]?.ToString(), out var risiko)
                        ? risiko
                        : Risiko.Green
                };

                residents.Add(resident);
            }

            return residents;
        }

        public async Task UpdateResidentAsync(ResidentModel resident)
        {

            using SqlConnection connection = new SqlConnection(_connectionString);
            using SqlCommand command = new SqlCommand("dbo.uspUpdateResidentById", connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add("@ResidentName", SqlDbType.NVarChar, 100).Value = resident.Name;
            command.Parameters.Add("@DepartmentID", SqlDbType.Int).Value =
                resident.DepartmentId.HasValue ? resident.DepartmentId.Value : DBNull.Value;
            command.Parameters.Add("@ResidentStatus", SqlDbType.NVarChar, 100).Value = resident.Status;
            command.Parameters.Add("@Risk", SqlDbType.NVarChar, 100).Value = resident.Risiko.ToString();
            command.Parameters.Add("@ResidentId", SqlDbType.Int).Value = resident.ResidentId;

            await connection.OpenAsync();
            await command.ExecuteNonQueryAsync();
        }

        public async Task DeleteResidentAsync(int residentId)
        {
            using SqlConnection connection = new SqlConnection(_connectionString);
            using SqlCommand command = new SqlCommand("dbo.uspDeleteResident", connection);

            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@ResidentId", SqlDbType.Int).Value = residentId;

            await connection.OpenAsync();
            await command.ExecuteNonQueryAsync();
        }

        public async Task<int> SaveNewResidentAsync(ResidentModel resident)
        {
            using SqlConnection connection = new SqlConnection(_connectionString);
            using SqlCommand command = new SqlCommand("dbo.uspCreateResident", connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add("@ResidentName", SqlDbType.NVarChar, 100).Value = resident.Name;
            command.Parameters.Add("@DepartmentID", SqlDbType.Int).Value =
                resident.DepartmentId.HasValue ? resident.DepartmentId.Value : DBNull.Value;
            command.Parameters.Add("@ResidentStatus", SqlDbType.NVarChar, 100).Value = resident.Status;
            command.Parameters.Add("@Risk", SqlDbType.NVarChar, 100).Value = resident.Risiko.ToString();

            await connection.OpenAsync();

            object? result = await command.ExecuteScalarAsync();
            return Convert.ToInt32(result);
        }
    }
}
