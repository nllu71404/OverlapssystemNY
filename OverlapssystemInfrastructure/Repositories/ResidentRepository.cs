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
        //Husk at tjekke dictionaries ud!!
        public async Task<List<ResidentModel>> GetAllResidentsAsync()
        {
            List<ResidentModel> residents = new();

            using SqlConnection connection = new SqlConnection(_connectionString);
            using SqlCommand command = new SqlCommand("dbo.uspGetResidents", connection);

            command.CommandType = CommandType.StoredProcedure;

            await connection.OpenAsync();

            using SqlDataReader reader = await command.ExecuteReaderAsync();

            var residentDict = new Dictionary<int, ResidentModel>(); //Tjek dictionaries ud!!!!

            while (await reader.ReadAsync())
            {
                int residentId = Convert.ToInt32(reader["ResidentID"]);

                if (!residentDict.ContainsKey(residentId))
                {
                    residentDict[residentId] = new ResidentModel
                    {
                        ResidentId = residentId,
                        DepartmentId = reader["DepartmentID"] == DBNull.Value ? null : Convert.ToInt32(reader["DepartmentID"]),
                        Name = reader["ResidentName"]?.ToString() ?? "",
                        Status = reader["ResidentStatus"]?.ToString() ?? "",
                        Activity = reader["Activity"]?.ToString() ?? "",
                        Family = reader["FamilyNote"]?.ToString() ?? "",
                        ResidentEmployee = reader["ResidentEmployee"]?.ToString() ?? "",
                        Risiko = Enum.TryParse<Risiko>(reader["Risk"]?.ToString(), out var risiko)
                            ? risiko
                            : Risiko.Green,
                        Mood = Enum.TryParse<Mood>(reader["Mood"]?.ToString(), out var mood)
                            ? mood
                            : Mood.Neutral,
                        MedicinTimes = new List<MedicinModel>(),
                        SpecialEvent = new List<SpecialEventModel>(),
                        PNMedicin = new List<PNMedicinModel>(),
                        Shopping = new List<ShoppingModel>()
                    };
                }

                // Hvis der findes medicintid
                if (reader["MedicinTimeID"] != DBNull.Value)
                {
                    var medicinId = Convert.ToInt32(reader["MedicinTimeID"]);

                    if (!residentDict[residentId].MedicinTimes
                        .Any(m => m.MedicinTimeID == medicinId))
                    {
                        residentDict[residentId].MedicinTimes.Add(new MedicinModel
                        {
                            MedicinTimeID = medicinId,
                            ResidentID = residentId,
                            MedicinTime = reader["MedicinTime"] == DBNull.Value ? (TimeSpan?)null : (TimeSpan)reader["MedicinTime"],
                            IsChecked = Convert.ToBoolean(reader["IsChecked"]),
                            MedicinCheckTimeStamp = reader["MedicinCheckTimeStamp"] == DBNull.Value
                                ? null
                                : Convert.ToDateTime(reader["MedicinCheckTimeStamp"])
                        });
                    }
                }

                // Hvis der findes PN medicin
                if (reader["PNID"] != DBNull.Value)
                {
                    var pnId = Convert.ToInt32(reader["PNID"]);

                    if (!residentDict[residentId].PNMedicin
                        .Any(p => p.PNMedicinID == pnId))
                    {
                        residentDict[residentId].PNMedicin.Add(new PNMedicinModel
                        {
                            PNMedicinID = pnId,
                            ResidentID = residentId,
                            PNTime = reader["PNTime"] == DBNull.Value
                                ? (DateTime?)null
                                : Convert.ToDateTime(reader["PNTime"]),

                            PNTimeStamp = reader["PNTimeStamp"] == DBNull.Value
                                ? null
                                : Convert.ToDateTime(reader["PNTimeStamp"]),

                            Reason = reader["Reason"]?.ToString() ?? ""
                        });
                    }
                }
                //Hvis der findes shopping
                if (reader["ShoppingID"] != DBNull.Value)
                {
                    var shoppingId = Convert.ToInt32(reader["ShoppingID"]);
                    if (!residentDict[residentId].Shopping
                        .Any(s => s.ShoppingID == shoppingId))
                    {
                        residentDict[residentId].Shopping.Add(new ShoppingModel
                        {
                            ShoppingID = shoppingId,
                            ResidentID = residentId,
                            Day = Enum.TryParse<Day>(reader["ShoppingDay"]?.ToString(), out var day)
                                ? day
                                : Day.Monday,
                            Time = reader["ShoppingTime"] == DBNull.Value ? (TimeSpan?)null : (TimeSpan)reader["ShoppingTime"],
                            PaymentMethod = reader["PaymentMethod"]?.ToString() ?? ""
                        });
                    }
                }
                //Hvis der findes specialEvent
                if (reader["SpecialEventID"] != DBNull.Value)
                {
                    var specialEventID = Convert.ToInt32(reader["SpecialEventID"]);
                    if (!residentDict[residentId].SpecialEvent
                        .Any(se => se.SpecialEventID == specialEventID))
                    {
                        residentDict[residentId].SpecialEvent.Add(new SpecialEventModel
                        {
                            SpecialEventID = specialEventID,
                            ResidentID = residentId,
                            SpecialEventNote = reader["SpecialEventNote"]?.ToString() ?? "",
                            SpecialEventDateTime = reader["SpecialEventDateTime"] == DBNull.Value ? null : Convert.ToDateTime(reader["SpecialEventDateTime"])
                        });
                    }
                }
            }
        

            return residentDict.Values.ToList();
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

            var residentDict = new Dictionary<int, ResidentModel>(); //Tjek dictionaries ud!!!!

            while (await reader.ReadAsync())
            {
                int residentId = Convert.ToInt32(reader["ResidentID"]);

                if (!residentDict.ContainsKey(residentId))
                {
                    residentDict[residentId] = new ResidentModel
                    {
                        ResidentId = residentId,
                        DepartmentId = reader["DepartmentID"] == DBNull.Value ? null : Convert.ToInt32(reader["DepartmentID"]),
                        Name = reader["ResidentName"]?.ToString() ?? "",
                        Status = reader["ResidentStatus"]?.ToString() ?? "",
                        Activity = reader["Activity"]?.ToString() ?? "",
                        Family = reader["FamilyNote"]?.ToString() ?? "",
                        ResidentEmployee = reader["ResidentEmployee"]?.ToString() ?? "",
                        Risiko = Enum.TryParse<Risiko>(reader["Risk"]?.ToString(), out var risiko)
                            ? risiko
                            : Risiko.Green,
                        Mood = Enum.TryParse<Mood>(reader["Mood"]?.ToString(), out var mood)
                            ? mood
                            : Mood.Neutral,
                        MedicinTimes = new List<MedicinModel>(),
                        SpecialEvent = new List<SpecialEventModel>(),
                        PNMedicin = new List<PNMedicinModel>(),
                        Shopping = new List<ShoppingModel>()
                    };
                }

                // Hvis der findes medicintid
                if (reader["MedicinTimeID"] != DBNull.Value)
                {
                    var medicinId = Convert.ToInt32(reader["MedicinTimeID"]);

                    if (!residentDict[residentId].MedicinTimes
                        .Any(m => m.MedicinTimeID == medicinId))
                    {
                        residentDict[residentId].MedicinTimes.Add(new MedicinModel
                        {
                            MedicinTimeID = medicinId,
                            ResidentID = residentId,
                            MedicinTime = reader["MedicinTime"] == DBNull.Value ? (TimeSpan?)null : (TimeSpan)reader["MedicinTime"],
                            IsChecked = Convert.ToBoolean(reader["IsChecked"]),
                            MedicinCheckTimeStamp = reader["MedicinCheckTimeStamp"] == DBNull.Value
                                ? null
                                : Convert.ToDateTime(reader["MedicinCheckTimeStamp"])
                        });
                    }
                }

                // Hvis der findes PN medicin
                if (reader["PNID"] != DBNull.Value)
                {
                    var pnId = Convert.ToInt32(reader["PNID"]);

                    if (!residentDict[residentId].PNMedicin
                        .Any(p => p.PNMedicinID == pnId))
                    {
                        residentDict[residentId].PNMedicin.Add(new PNMedicinModel
                        {
                            PNMedicinID = pnId,
                            ResidentID = residentId,
                            PNTime = reader["PNTime"] == DBNull.Value
                                ? (DateTime?)null
                                : Convert.ToDateTime(reader["PNTime"]),

                            PNTimeStamp = reader["PNTimeStamp"] == DBNull.Value
                                ? null
                                : Convert.ToDateTime(reader["PNTimeStamp"]),

                            Reason = reader["Reason"]?.ToString() ?? ""
                        });
                    }
                }
                // Hvis der findes shopping
                if (reader["ShoppingID"] != DBNull.Value)
                {
                    var shoppingId = Convert.ToInt32(reader["ShoppingID"]);
                    if (!residentDict[residentId].Shopping
                        .Any(s => s.ShoppingID == shoppingId))
                    {
                        residentDict[residentId].Shopping.Add(new ShoppingModel
                        {
                            ShoppingID = shoppingId,
                            ResidentID = residentId,
                            Day = Enum.TryParse<Day>(reader["ShoppingDay"]?.ToString(), out var day)
                                ? day
                                : Day.Monday,
                            Time = reader["ShoppingTime"] == DBNull.Value ? (TimeSpan?)null : (TimeSpan)reader["ShoppingTime"],
                            PaymentMethod = reader["PaymentMethod"]?.ToString() ?? ""
                        });
                    }
                }

                //Hvis der findes specialEvent
                if (reader["SpecialEventID"] != DBNull.Value)
                {
                    var specialEventID = Convert.ToInt32(reader["SpecialEventID"]);
                    if (!residentDict[residentId].SpecialEvent
                        .Any(se => se.SpecialEventID == specialEventID)){
                        residentDict[residentId].SpecialEvent.Add(new SpecialEventModel
                        {
                            SpecialEventID = specialEventID,
                            ResidentID = residentId,
                            SpecialEventNote = reader["SpecialEventNote"]?.ToString() ?? "",
                            SpecialEventDateTime = reader["SpecialEventDateTime"] == DBNull.Value ? null : Convert.ToDateTime(reader["SpecialEventDateTime"])
                        });
                    }
                }

            }

            return residentDict.Values.ToList();
           
        }

        public async Task UpdateResidentAsync(ResidentModel resident)
        {

            using SqlConnection connection = new SqlConnection(_connectionString);
            using SqlCommand command = new SqlCommand("dbo.uspUpdateResidentById", connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add("@ResidentName", SqlDbType.NVarChar, 100).Value = resident.Name;
            command.Parameters.Add("@DepartmentID", SqlDbType.Int).Value =
                resident.DepartmentId.HasValue ? resident.DepartmentId.Value : DBNull.Value;
            command.Parameters.Add("@ResidentStatus", SqlDbType.NVarChar, 250).Value = resident.Status;
            command.Parameters.Add("@Activity", SqlDbType.NVarChar, 250).Value = resident.Activity; //Tjek her!!
            command.Parameters.Add("@FamilyNote", SqlDbType.NVarChar, 250).Value = resident.Family; //Tjek her!! 
            command.Parameters.Add("@ResidentEmployee", SqlDbType.NVarChar, 250).Value = resident.ResidentEmployee; //Tjek her!!
            command.Parameters.Add("@Risk", SqlDbType.NVarChar, 100).Value = resident.Risiko.ToString();
            command.Parameters.Add("@Mood", SqlDbType.NVarChar, 100).Value = resident.Mood.ToString();
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
            command.Parameters.Add("@ResidentStatus", SqlDbType.NVarChar, 250).Value = resident.Status;
            command.Parameters.Add("@Activity", SqlDbType.NVarChar, 250).Value = resident.Activity; //Tjek her!!
            command.Parameters.Add("@FamilyNote", SqlDbType.NVarChar, 250).Value = resident.Family; //Tjek her!!
            command.Parameters.Add("@ResidentEmployee", SqlDbType.NVarChar, 250).Value = resident.ResidentEmployee; //Tjek her!!
            command.Parameters.Add("@Risk", SqlDbType.NVarChar, 100).Value = resident.Risiko.ToString();
            command.Parameters.Add("@Mood", SqlDbType.NVarChar, 100).Value = resident.Mood.ToString();

            await connection.OpenAsync();

            object? result = await command.ExecuteScalarAsync();
            return Convert.ToInt32(result);
        }
    }
}
