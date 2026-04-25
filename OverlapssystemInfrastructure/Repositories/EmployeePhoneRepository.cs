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
    public class EmployeePhoneRepository : IEmployeePhoneRepository
    {
      private readonly string _connectionString;

        public EmployeePhoneRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ProjektDB")
                ?? throw new InvalidOperationException("Connection string 'ProjektDB' not found.");
        }

        public async Task<List<EmployeePhoneModel>> GetAllEmployeePhoneNumbersAsync()
        {
            List<EmployeePhoneModel> employeePhones = new();

            using SqlConnection connection = new SqlConnection(_connectionString);
            using SqlCommand command = new SqlCommand("dbo.uspGetEmployeePhoneNumbers", connection);
            command.CommandType = CommandType.StoredProcedure;

            await connection.OpenAsync();

            using SqlDataReader reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                EmployeePhoneModel phone = new EmployeePhoneModel
                {
                    EmployeePhoneID = Convert.ToInt32(reader["EmployeePhoneID"]),
                    DepartmentID = reader["DepartmentID"] == DBNull.Value ? null : Convert.ToInt32(reader["DepartmentID"]),
                    PhoneNumber = reader["PhoneNumber"]?.ToString() ?? "",
                    EmployeeName = reader["EmployeeName"]?.ToString() ?? "",
                    Test = reader["Test"] != DBNull.Value && Convert.ToBoolean(reader["Test"])
                };

                employeePhones.Add(phone);
            }

            return employeePhones;
        }


        public async Task<EmployeePhoneModel> GetEmployeePhoneByIdAsync(int employeePhoneId)
        {
            EmployeePhoneModel? phone = null;

            using SqlConnection connection = new SqlConnection(_connectionString);
            using SqlCommand command = new SqlCommand("dbo.uspGetEmployeePhoneTaskByEmployeePhoneID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@EmployeePhoneID", SqlDbType.Int).Value = employeePhoneId;

            await connection.OpenAsync();

            using SqlDataReader reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                phone = new EmployeePhoneModel
                {
                    EmployeePhoneID = Convert.ToInt32(reader["EmployeePhoneID"]),
                    DepartmentID = reader["DepartmentID"] == DBNull.Value ? null : Convert.ToInt32(reader["DepartmentID"]),
                    PhoneNumber = reader["PhoneNumber"]?.ToString() ?? "",
                    EmployeeName = reader["EmployeeName"]?.ToString() ?? "",
                    Test = reader["Test"] != DBNull.Value && Convert.ToBoolean(reader["Test"])
                };
            }

            return phone!;
        }

        public async Task<List<EmployeePhoneModel>> GetEmployeePhonesByDepartmentIdAsync(int departmentId)
        {
            List<EmployeePhoneModel> employeePhones = new();
            using SqlConnection connection = new SqlConnection(_connectionString);
            using SqlCommand command = new SqlCommand("dbo.uspGetEmployeePhoneNumberByDepartmentID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@DepartmentID", SqlDbType.Int).Value = departmentId;
            await connection.OpenAsync();
            using SqlDataReader reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                EmployeePhoneModel phone = new EmployeePhoneModel
                {
                    EmployeePhoneID = Convert.ToInt32(reader["EmployeePhoneID"]),
                    DepartmentID = reader["DepartmentID"] == DBNull.Value ? null : Convert.ToInt32(reader["DepartmentID"]),
                    PhoneNumber = reader["PhoneNumber"]?.ToString() ?? "",
                    EmployeeName = reader["EmployeeName"]?.ToString() ?? "",
                    Test = reader["Test"] != DBNull.Value && Convert.ToBoolean(reader["Test"])
                };
                employeePhones.Add(phone);
            }
            return employeePhones;
        }

        public async Task<int> SaveNewEmployeePhoneAsync(EmployeePhoneModel employeePhone)
        {
            using SqlConnection connection = new SqlConnection(_connectionString);
            using SqlCommand command = new SqlCommand("dbo.uspCreateEmployeePhone", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(@"DepartmentID", SqlDbType.Int).Value = employeePhone.DepartmentID ?? (object)DBNull.Value;
            command.Parameters.Add("@PhoneNumber", SqlDbType.NVarChar, 50).Value = employeePhone.PhoneNumber ?? (object)DBNull.Value;
            command.Parameters.Add("@EmployeeName", SqlDbType.NVarChar, 100).Value = employeePhone.EmployeeName ?? (object)DBNull.Value;
            command.Parameters.Add("@Test", SqlDbType.Bit).Value = employeePhone.Test;

            await connection.OpenAsync();
            object? result = await command.ExecuteScalarAsync();
            return Convert.ToInt32(result);
        }

        public async Task UpdateEmployeePhoneAsync(EmployeePhoneModel employeePhone)
        {
            using SqlConnection connection = new SqlConnection(_connectionString);
            using SqlCommand command = new SqlCommand("dbo.uspUpdateEmployeePhoneById", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add("@EmployeePhoneID", SqlDbType.Int).Value = employeePhone.EmployeePhoneID;
            command.Parameters.Add("@DepartmentID", SqlDbType.Int).Value = employeePhone.DepartmentID ?? (object)DBNull.Value;
            command.Parameters.Add("@PhoneNumber", SqlDbType.NVarChar, 50).Value = employeePhone.PhoneNumber ?? (object)DBNull.Value;
            command.Parameters.Add("@EmployeeName", SqlDbType.NVarChar, 100).Value = employeePhone.EmployeeName ?? (object)DBNull.Value;
            command.Parameters.Add("@Test", SqlDbType.Bit).Value = employeePhone.Test;

            await connection.OpenAsync();
            await command.ExecuteNonQueryAsync();
        }
        public async Task DeleteEmployeePhoneAsync(int employeePhoneId)
        {
            using SqlConnection connection = new SqlConnection(_connectionString);
            using SqlCommand command = new SqlCommand("dbo.uspDeleteEmployeePhone", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add("@EmployeePhoneID", SqlDbType.Int).Value = employeePhoneId;

            await connection.OpenAsync();
            await command.ExecuteNonQueryAsync();
        }
    }
}
