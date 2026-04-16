using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using OverlapssystemDomain.Entities;
using OverlapssystemDomain.Enums;
using OverlapssystemDomain.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OverlapssystemInfrastructure.Repositories
{
    public class DepartmentTaskRepository : IDepartmentTaskRepository
    {
        private readonly string _connectionString;

        public DepartmentTaskRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ProjektDB")
                ?? throw new InvalidOperationException("Connection string 'ProjektDB' not found.");
        }

        //Helper method til at parse ShiftType (Enum) fra database, da den gemmes som string i databasen
        private ShiftType ParseShiftType(object value)
        {
            return Enum.TryParse(value?.ToString(), true, out ShiftType shift)
                ? shift
                : throw new Exception($"Ugyldig ShiftType værdi i databasen: {value}");
        }

        public async Task<List<DepartmentTaskModel>> GetAllDepartmentTasksAsync()
        {
            List<DepartmentTaskModel> departmentTasks = new();

            using SqlConnection connection = new SqlConnection(_connectionString);
            using SqlCommand command = new SqlCommand("dbo.uspGetAllDepartmentTasks", connection);
            command.CommandType = CommandType.StoredProcedure;

            await connection.OpenAsync();

            using SqlDataReader reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                DepartmentTaskModel task = new DepartmentTaskModel
                {
                    DepartmentTaskID = Convert.ToInt32(reader["DepartmentTaskID"]),
                    DepartmentID = reader["DepartmentID"] == DBNull.Value ? null : Convert.ToInt32(reader["DepartmentID"]),
                    DepartmentTaskTopic = reader["DepartmentTaskTopic"]?.ToString() ?? "",
                    EmployeeName = reader["EmployeeName"]?.ToString() ?? "",
                    ShiftType = ParseShiftType(reader["ShiftType"])
                };

                departmentTasks.Add(task);
            }

            return departmentTasks;
        }

        public async Task<DepartmentTaskModel> GetDepartmentTaskByIdAsync(int departmentTaskId)
        {
            DepartmentTaskModel? task = null;

            using SqlConnection connection = new SqlConnection(_connectionString);
            using SqlCommand command = new SqlCommand("dbo.uspGetDepartmentTaskById", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@DepartmentTaskID", SqlDbType.Int).Value = departmentTaskId;

            await connection.OpenAsync();

            using SqlDataReader reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                task = new DepartmentTaskModel
                {
                    DepartmentTaskID = Convert.ToInt32(reader["DepartmentTaskID"]),
                    DepartmentID = reader["DepartmentID"] == DBNull.Value ? null : Convert.ToInt32(reader["DepartmentID"]),
                    DepartmentTaskTopic = reader["DepartmentTaskTopic"]?.ToString() ?? "",
                    EmployeeName = reader["EmployeeName"]?.ToString() ?? "",
                    ShiftType = ParseShiftType(reader["ShiftType"])
                };
            }

            return task!;
        }

        public async Task<List<DepartmentTaskModel>> GetDepartmentTaskByDepartmentIdAsync(int departmentId)
        {
            List<DepartmentTaskModel> departmentTasks = new();

            using SqlConnection connection = new SqlConnection(_connectionString);
            using SqlCommand command = new SqlCommand("dbo.uspGetDepartmentTasksByDepartmentId", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@DepartmentID", SqlDbType.Int).Value = departmentId;

            await connection.OpenAsync();

            using SqlDataReader reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                DepartmentTaskModel task = new DepartmentTaskModel
                {
                    DepartmentTaskID = Convert.ToInt32(reader["DepartmentTaskID"]),
                    DepartmentID = reader["DepartmentID"] == DBNull.Value ? null : Convert.ToInt32(reader["DepartmentID"]),
                    DepartmentTaskTopic = reader["DepartmentTaskTopic"]?.ToString() ?? "",
                    EmployeeName = reader["EmployeeName"]?.ToString() ?? "",
                    ShiftType = ParseShiftType(reader["ShiftType"])
                };

                departmentTasks.Add(task);
            }

            return departmentTasks;
        }

        public async Task<int> SaveNewDepartmentTaskAsync(DepartmentTaskModel departmentTask)
        {
            using SqlConnection connection = new SqlConnection(_connectionString);
            using SqlCommand command = new SqlCommand("dbo.uspCreateDepartmentTask", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add("@DepartmentID", SqlDbType.Int).Value = departmentTask.DepartmentID ?? (object)DBNull.Value;
            command.Parameters.Add("@DepartmentTaskTopic", SqlDbType.NVarChar, 250).Value = departmentTask.DepartmentTaskTopic ?? (object)DBNull.Value;
            command.Parameters.Add("@EmployeeName", SqlDbType.NVarChar, 100).Value = departmentTask.EmployeeName ?? (object)DBNull.Value;
            command.Parameters.Add("@ShiftType", SqlDbType.NVarChar, 100).Value = departmentTask.ShiftType.ToString();

            await connection.OpenAsync();
            object? result = await command.ExecuteScalarAsync();
            return Convert.ToInt32(result);
        }

        public async Task UpdateDepartmentTaskAsync(DepartmentTaskModel departmentTask)
        {
            using SqlConnection connection = new SqlConnection(_connectionString);
            using SqlCommand command = new SqlCommand("dbo.uspUpdateDepartmentTaskById", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add("@DepartmentTaskID", SqlDbType.Int).Value = departmentTask.DepartmentTaskID;
            command.Parameters.Add("@DepartmentID", SqlDbType.Int).Value = departmentTask.DepartmentID ?? (object)DBNull.Value;
            command.Parameters.Add("@DepartmentTaskTopic", SqlDbType.NVarChar, 250).Value = departmentTask.DepartmentTaskTopic ?? (object)DBNull.Value;
            command.Parameters.Add("@EmployeeName", SqlDbType.NVarChar, 100).Value = departmentTask.EmployeeName ?? (object)DBNull.Value;
            command.Parameters.Add("@ShiftType", SqlDbType.NVarChar, 100).Value = departmentTask.ShiftType.ToString();

            await connection.OpenAsync();
            await command.ExecuteNonQueryAsync();
        }

        public async Task DeleteDepartmentTaskAsync(int departmentTaskId)
        {
            using SqlConnection connection = new SqlConnection(_connectionString);
            using SqlCommand command = new SqlCommand("dbo.uspDeleteDepartmentTask", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add("@DepartmentTaskID", SqlDbType.Int).Value = departmentTaskId;

            await connection.OpenAsync();
            await command.ExecuteNonQueryAsync();
        }
    }
}
