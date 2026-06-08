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
                _logger.LogError(ex, "Fejl ved hentning af beboere");
                return Error.Technical("Fejl ved hentning af beboere"); 
            }
        }

        // Hent på DepartmentId

        
        public async Task<Result<List<ResidentModel>>> LoadResidentsByDepartmentAsync(int departmentId)
        {
            
            if (departmentId <= 0)
                return Error.Validation("Ugyldigt afdelingsID");

            try
            {
                var data = await _residentRepository.GetResidentByDepartmentIdAsync(departmentId);

                Residents = data ?? new List<ResidentModel>();

                return Residents;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fejl ved hentning af beboere for afdeling {DepartmentId}", departmentId);
                return Error.Technical("Fejl ved hentning af beboere for afdeling"); 
            }
        }

        // Opret
        public async Task<Result<int>> CreateResidentAsync(ResidentModel resident)
        {

            _logger.LogInformation("Creating resident for department {DepartmentId}", resident.DepartmentId);

            if (resident.DepartmentId == null || resident.DepartmentId <= 0)
            {
                _logger.LogWarning("CreateResident failed: missing DepartmentId");
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
                _logger.LogError(ex, "Fejl ved oprettelse af beboer!");

                return Error.Technical("Kunne ikke oprette beboer");
            }
        }

        // Update 
        public async Task<Result> UpdateResidentAsync(ResidentModel resident)
        {


            if (resident.ResidentId <= 0)
            {
                _logger.LogWarning("UpdateResident failed: invalid ResidentId");
                return Error.Validation("Ugyldigt beboer ID");

            }

            if (string.IsNullOrWhiteSpace(resident.Name))
            {
                _logger.LogWarning("UpdateResident failed: Name is required");
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
                _logger.LogError(ex, "Beboer blev ikke fundet");
                return Error.NotFound("Kunne ikke finde beboer at opdatere");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fejl ved opdatering af beboer");
                return Error.Technical("Kunne ikke opdatere beboer");
            }
        }

        // Delete
        public async Task<Result> DeleteResidentAsync(int residentId)
        {

            if (residentId <= 0)
            {
                _logger.LogWarning("DeleteResident failed: invalid ResidentId");
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
               _logger.LogError(ex, "Beboer kan ikke findes");
                return Error.NotFound("Kunne ikke finde beboer at slette");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fejl ved sletning af beboer");
                return Error.Technical("Kunne ikke slette beboer");
            }
            
        }

        // Tildel department
        public void SetDepartment(int departmentId)
        {
            if (departmentId <= 0)
            {
                _logger.LogWarning("SetDepartment failed: invalid DepartmentId");
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

