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
    public class ShoppingRepository : IShoppingRepository
    {
        private readonly string _connectionString;

        public ShoppingRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ProjektDB")
                ?? throw new InvalidOperationException("Connection string 'ProjektDB' not found.");
        }

        public async Task<List<ShoppingModel>> GetAllShoppingAsync()
        {
            List<ShoppingModel> shoppingTimes = new();

            using SqlConnection connection = new SqlConnection(_connectionString);
            using SqlCommand command = new SqlCommand("dbo.uspGetShoppingTimes", connection);

            command.CommandType = CommandType.StoredProcedure;

            await connection.OpenAsync();

            using SqlDataReader reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                ShoppingModel shopping = new ShoppingModel
                {
                    
                    ShoppingID = Convert.ToInt32(reader["ShoppingID"]),
                   
                    ResidentID = Convert.ToInt32(reader["ResidentID"]),

                    
                    Day = Enum.TryParse<Day>(reader["Risk"]?.ToString(), out var day)
                            ? day:Day.Monday,
                    Time = reader["ShoppingTime"] == DBNull.Value ? TimeSpan.Zero : (TimeSpan)reader["ShoppingTime"],

                    //DateAndTime = reader["ShoppingTime"] == DBNull.Value ? null : Convert.ToDateTime(reader["ShoppingTime"]),

                    PaymentMethod = reader["PaymentMethod"]?.ToString() ?? ""

                };

                shoppingTimes.Add(shopping);
            }

            return shoppingTimes;
        }

        public async Task<List<ShoppingModel>> GetShoppingByResidentIdAsync(int residentId)
        {
            List<ShoppingModel> shoppingTimes = new();

            using SqlConnection connection = new SqlConnection(_connectionString);
            using SqlCommand command = new SqlCommand("dbo.uspGetShoppingTimesByResidentID", connection);

            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@ResidentID", SqlDbType.Int).Value = residentId;

            await connection.OpenAsync();

            using SqlDataReader reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                ShoppingModel shopping = new ShoppingModel
                {

                    ShoppingID = Convert.ToInt32(reader["ShoppingID"]),

                    ResidentID = Convert.ToInt32(reader["ResidentID"]),

                    Day = Enum.TryParse<Day>(reader["Risk"]?.ToString(), out var day)
                            ? day : Day.Monday,
                    Time = reader["ShoppingTime"] == DBNull.Value ? TimeSpan.Zero : (TimeSpan)reader["ShoppingTime"],

                    //DateAndTime = reader["ShoppingTime"] == DBNull.Value ? null : Convert.ToDateTime(reader["ShoppingTime"]),

                    PaymentMethod = reader["PaymentMethod"]?.ToString() ?? ""

                };

                shoppingTimes.Add(shopping);
            }

            return shoppingTimes;
        }

        public async Task<int> SaveNewShoppingAsync(ShoppingModel shopping)
        {
            using SqlConnection connection = new SqlConnection(_connectionString);
            using SqlCommand command = new SqlCommand("dbo.uspCreateShoppingTime", connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add("@ResidentID", SqlDbType.Int).Value = shopping.ResidentID;
            command.Parameters.Add("@ShoppingDay", SqlDbType.NVarChar, 100).Value = shopping.Day.ToString();
            command.Parameters.Add("@ShoppingTime", SqlDbType.Time).Value = shopping.Time;
            command.Parameters.Add("@PaymentMethod", SqlDbType.NVarChar, 100).Value = shopping.PaymentMethod;

            await connection.OpenAsync();
            object? result = await command.ExecuteScalarAsync();
            return Convert.ToInt32(result);
        }

        public async Task UpdateShoppingAsync(ShoppingModel shopping)
        {
            using SqlConnection connection = new SqlConnection(_connectionString);
            using SqlCommand command = new SqlCommand("dbo.uspUpdateShoppingTimeById", connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add("@ResidentID", SqlDbType.Int).Value = shopping.ResidentID;
            command.Parameters.Add("@ShoppingDay", SqlDbType.NVarChar, 100).Value = shopping.Day.ToString();
            command.Parameters.Add("@ShoppingTime", SqlDbType.Time).Value = shopping.Time;
            command.Parameters.Add("@PaymentMethod", SqlDbType.NVarChar, 100).Value = shopping.PaymentMethod;
            command.Parameters.Add("@ShoppingID", SqlDbType.Int).Value = shopping.ShoppingID;

            await connection.OpenAsync();
            await command.ExecuteNonQueryAsync();
        }

        public async Task DeleteShoppingAsync(int shoppingId)
        {
            using SqlConnection connection = new SqlConnection(_connectionString);
            using SqlCommand command = new SqlCommand("dbo.uspDeleteShoppingTime", connection);

            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@ShoppingID", SqlDbType.Int).Value = shoppingId;

            await connection.OpenAsync();
            await command.ExecuteNonQueryAsync();
        }
    }
}
