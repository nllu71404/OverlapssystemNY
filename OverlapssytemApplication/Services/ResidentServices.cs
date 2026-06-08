using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OverlapssystemDomain.Entities;
using OverlapssystemDomain.Enums;
using OverlapssystemDomain.Interfaces;
using OverlapssytemApplication.Interfaces;
using OverlapssytemApplication.Common.Errors;
using Microsoft.Extensions.Logging;
using OverlapssytemApplication.Common.Result;


namespace OverlapssytemApplication.Services
{

    public class ResidentServices : IResidentServices
    {
        private readonly IResidentRepository _residentRepository;

        private readonly ILogger<ResidentServices> _logger;

        public ResidentServices(IResidentRepository residentRepository, ILogger<ResidentServices> logger)
        {
            _residentRepository = residentRepository;
            _logger = logger;
        }

        public List<ResidentModel> Residents { get; private set; } = new();

        public int SelectedDepartmentId { get; set; } = 1;

        public ResidentModel NewResident { get; set; } = new ResidentModel
        {
            Risiko = Risiko.Green
        };

        // Hent alle
        public async Task<Result<List<ResidentModel>>> LoadResidentsAsync()
        {
            try
            {
                var data = await _residentRepository.GetAllResidentsAsync();

                Residents = data ?? new List<ResidentModel>();

                return Residents; 
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "LoadResidents failed: unexpected error");
                return Error.Technical("Fejl ved hentning af beboere"); 
            }
        }

        // Hent på DepartmentId

        
        public async Task<Result<List<ResidentModel>>> LoadResidentsByDepartmentAsync(int departmentId)
        {

            if (departmentId <= 0)
            {
                _logger.LogError("LoadResidentsByDepartment failed: invalid DepartmentId");
                return Error.Validation("Ugyldigt afdelingsID");
            }

            try
            {
                var data = await _residentRepository.GetResidentByDepartmentIdAsync(departmentId);

                Residents = data ?? new List<ResidentModel>();

                return Residents;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "LoadResidentsByDepartment failed: unexpected error for DepartmentId {DepartmentId}", departmentId);
                return Error.Technical("Fejl ved hentning af beboere for afdeling"); 
            }
        }

        // Opret
        public async Task<Result<int>> CreateResidentAsync(ResidentModel resident)
        {

            _logger.LogInformation("Creating resident for department {DepartmentId}", resident.DepartmentId);

            if (resident.DepartmentId == null || resident.DepartmentId <= 0)
            {
                _logger.LogError("CreateResident failed: missing DepartmentId");
                return Error.Validation("Afdelings ID er ikke sat");
            }
            try
            {
                var id = await _residentRepository.SaveNewResidentAsync(resident);

                _logger.LogInformation("Resident created successfully with id {Id}", id);

                return id; 
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "CreateResident failed: unexpected error for ResidentId {ResidentId}", resident.ResidentId);

                return Error.Technical("Kunne ikke oprette beboer");
            }
        }

        // Update 
        public async Task<Result> UpdateResidentAsync(ResidentModel resident)
        {


            if (resident.ResidentId <= 0)
            {
                _logger.LogError("UpdateResident failed: invalid ResidentId");
                return Error.Validation("Ugyldigt beboer ID");

            }

            if (string.IsNullOrWhiteSpace(resident.Name))
            {
                _logger.LogError("UpdateResident {ResidentId} failed: Name is required", resident.ResidentId);
                return Error.Validation("Navn er påkrævet");
            }


            try
            {
                await _residentRepository.UpdateResidentAsync(resident);

                _logger.LogInformation("Resident {ResidentId} updated successfully", resident.ResidentId);

                return Result.Ok();
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogError(ex, "UpdateResident failed: ResidentId {ResidentId} not found", resident.ResidentId);
                return Error.NotFound("Kunne ikke finde beboer at opdatere");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "UpdateResident failed: unexpected error for ResidentId {ResidentId}", resident.ResidentId);
                return Error.Technical("Kunne ikke opdatere beboer");
            }
        }

        // Delete
        public async Task<Result> DeleteResidentAsync(int residentId)
        {

            if (residentId <= 0)
            {
                _logger.LogError("DeleteResident failed: invalid ResidentId");
                return Error.Validation("Ugyldigt beboer ID");
            }

            try
            {
                await _residentRepository.DeleteResidentAsync(residentId);

                _logger.LogInformation("Resident {ResidentId} deleted successfully", residentId);

                return Result.Ok();
            }
            catch (KeyNotFoundException ex)
            {
               _logger.LogError(ex, "DeleteResident failed: ResidentId {ResidentId} not found", residentId);
                return Error.NotFound("Kunne ikke finde beboer at slette");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "DeleteResident failed: unexpected error for ResidentId {ResidentId}", residentId);
                return Error.Technical("Kunne ikke slette beboer");
            }
            
        }

        // Tildel department
        public void SetDepartment(int departmentId)
        {
            if (departmentId <= 0)
            {
                _logger.LogError("SetDepartment failed: invalid DepartmentId");
                return;
            }

            SelectedDepartmentId = departmentId;

            NewResident = new ResidentModel
            {
                DepartmentId = departmentId
            };

        }
    }
}

