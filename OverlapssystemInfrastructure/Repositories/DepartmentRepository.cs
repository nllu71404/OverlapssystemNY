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
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly string _connectionString;

        public DepartmentRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ProjektDB")
                ?? throw new InvalidOperationException("Connection string 'ProjektDB' not found.");
        }

        public async Task<List<DepartmentModel>> GetAllDepartmentsAsync()
        {
            List<DepartmentModel> departments = new();

            using SqlConnection connection = new SqlConnection(_connectionString);
            using SqlCommand command = new SqlCommand("dbo.uspGetDepartments", connection);

            command.CommandType = CommandType.StoredProcedure;

            await connection.OpenAsync();

            using SqlDataReader reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                DepartmentModel department = new DepartmentModel
                {

                    DepartmentID = Convert.ToInt32(reader["DepartmentID"]),              
                    Name = reader["DepartmentName"]?.ToString() ?? ""

                };

                departments.Add(department);
            }

            return departments;
        }

        public async Task<DepartmentModel> GetDepartmentByIdAsync(int departmentId)
        {
            DepartmentModel department = new();

            using SqlConnection connection = new SqlConnection(_connectionString);
            using SqlCommand command = new SqlCommand("dbo.uspGetDepartmentsByDepartmentID", connection);

            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@DepartmentID", SqlDbType.Int).Value = departmentId;

            await connection.OpenAsync();

            using SqlDataReader reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                DepartmentModel newDepartment = new DepartmentModel
                {

                    DepartmentID = Convert.ToInt32(reader["DepartmentID"]),
                    Name = reader["DepartmentName"]?.ToString() ?? ""

                };

                department = newDepartment;
            }

            return department;
        }

        public async Task<DepartmentModel> GetDepartmentByNameAsync(string departmentName)
        {
            DepartmentModel department = new();

            using SqlConnection connection = new SqlConnection(_connectionString);
            using SqlCommand command = new SqlCommand("dbo.uspGetDepartmentsByDepartmentName", connection);

            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@DepartmentName", SqlDbType.Int).Value = departmentName;

            await connection.OpenAsync();

            using SqlDataReader reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                DepartmentModel newDepartment = new DepartmentModel
                {

                    DepartmentID = Convert.ToInt32(reader["DepartmentID"]),
                    Name = reader["DepartmentName"]?.ToString() ?? ""

                };

                department = newDepartment;
            }

            return department;
        }

        public async Task<int> SaveNewDepartmentAsync(DepartmentModel department)
        {
            using SqlConnection connection = new SqlConnection(_connectionString);
            using SqlCommand command = new SqlCommand("dbo.uspCreateDepartment", connection);

            command.CommandType = CommandType.StoredProcedure;

            
            command.Parameters.Add("@DepartmentName", SqlDbType.NVarChar, 100).Value = department.Name;
      
            await connection.OpenAsync();
            object? result = await command.ExecuteScalarAsync();
            return Convert.ToInt32(result);
        }

        public async Task UpdateDepartmentAsync(DepartmentModel department)
        {
            using SqlConnection connection = new SqlConnection(_connectionString);
            using SqlCommand command = new SqlCommand("dbo.uspUpdateDepartmentById", connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add("@DepartmentName", SqlDbType.NVarChar, 100).Value = department.Name;
            command.Parameters.Add("@DepartmentID", SqlDbType.Int).Value = department.DepartmentID;
            await connection.OpenAsync();
            await command.ExecuteNonQueryAsync();
        }

        public async Task DeleteDepartmentAsync(int departmentId)
        {
            using SqlConnection connection = new SqlConnection(_connectionString);
            using SqlCommand command = new SqlCommand("dbo.uspDeleteDepartment", connection);

            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@DepartmentID", SqlDbType.Int).Value = departmentId;
           
            await connection.OpenAsync();
            await command.ExecuteNonQueryAsync();
        }
    }
}
