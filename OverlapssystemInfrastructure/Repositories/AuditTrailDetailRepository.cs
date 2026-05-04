using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using OverlapssystemDomain.Entities;
using OverlapssystemDomain.Enums;
using OverlapssystemDomain.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace OverlapssystemInfrastructure.Repositories
{
    public class AuditTrailDetailRepository : IAuditTrailDetailRepository
    {
        private readonly string _connectionString;

        public AuditTrailDetailRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ProjektDB")
                ?? throw new InvalidOperationException("Connection string 'ProjektDB' not found.");
        }

        public async Task<List<AuditTrailDetailModel>> GetAuditTrailDetailsByDepartmentIdAsync(int departmentId)
        {
            var auditTrailDetails = new List<AuditTrailDetailModel>();

            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            using var command = new SqlCommand("dbo.uspGetAuditTrailDetailsByDepartmentId", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@DepartmentId", departmentId);

            using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                var detail = new AuditTrailDetailModel
                {
                    DepartmentID = reader.IsDBNull(reader.GetOrdinal("DepartmentID"))
                        ? null
                        : reader.GetInt32(reader.GetOrdinal("DepartmentID")),

                    AuditLogDetailId = reader.GetInt32(reader.GetOrdinal("AuditLogDetailId")),

                    TableName = GetNullableString(reader, "TableName"),
                    PrimaryKeyValue = GetNullableString(reader, "PrimaryKeyValue"),
                    ColumnName = GetNullableString(reader, "ColumnName"),
                    OldValue = GetNullableString(reader, "OldValue"),
                    NewValue = GetNullableString(reader, "NewValue"),
                    Operation = GetNullableString(reader, "Operation"),
                    ChangedBy = GetNullableString(reader, "ChangedBy"),

                    ChangeDate = reader.GetDateTime(reader.GetOrdinal("ChangeDate"))
                        .ToString("yyyy-MM-dd HH:mm:ss")
                };

                auditTrailDetails.Add(detail);
            }

            return auditTrailDetails;
        }

        private static string? GetNullableString(SqlDataReader reader, string columnName)
        {
            int ordinal = reader.GetOrdinal(columnName);
            return reader.IsDBNull(ordinal) ? null : reader.GetString(ordinal);
        }
    }    
}



