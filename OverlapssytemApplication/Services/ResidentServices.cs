using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OverlapssystemDomain.Entities;
using OverlapssystemDomain.Enums;
using OverlapssystemDomain.Interfaces;
using OverlapssytemApplication.Interfaces;
using OverlapssytemApplication.Common;
using OverlapssytemApplication.Common.Errors;
using Microsoft.Extensions.Logging;


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

                return Residents; // implicit success
            }
            catch (Exception)
            {
                return Error.Technical("Kunne ikke hente beboere");
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
            catch (Exception)
            {
                return Error.Technical("Kunne ikke hente beboere for afdeling");
            }
        }

        // Opret
        public async Task<Result<int>> CreateResidentAsync(ResidentModel resident)
        {
            if (resident == null)
                return Error.Validation("Resident må ikke være null");

            if (string.IsNullOrWhiteSpace(resident.Name))
                return Error.Validation("Navn er påkrævet");

            if (resident.DepartmentId == null || resident.DepartmentId <= 0)
                return Error.Validation("Afdelings ID er ikke sat");

            try
            {
                var id = await _residentRepository.SaveNewResidentAsync(resident);

                Residents = await _residentRepository
                    .GetResidentByDepartmentIdAsync(resident.DepartmentId.Value)
                    ?? new List<ResidentModel>();

                return id; // implicit success
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fejl ved oprettelse af resident!");

                return Error.Technical("Kunne ikke gemme resident");
            }
        }

        // Update
        public async Task<Result> UpdateResidentAsync(ResidentModel resident)
        {
            if (resident == null)
                return Error.Validation("Resident må ikke være null");

            if (resident.ResidentId <= 0)
                return Error.Validation("Ugyldigt beboer ID");

            if (string.IsNullOrWhiteSpace(resident.Name))
                return Error.Validation("Navn er påkrævet");

            try
            {
                await _residentRepository.UpdateResidentAsync(resident);

                Residents = await _residentRepository
                    .GetResidentByDepartmentIdAsync(resident.DepartmentId ?? SelectedDepartmentId)
                    ?? new List<ResidentModel>();

                return Result.Ok();
            }
            catch (KeyNotFoundException)
            {
                return Error.NotFound("Beboer blev ikke fundet");
            }
            catch (Exception)
            {
                return Error.Technical("Kunne ikke opdatere beboer");
            }
        }

        // Delete
        public async Task<Result> DeleteResidentAsync(int residentId)
        {
            if (residentId <= 0)
                return Error.Validation("Ugyldigt ID");

            try
            {
                await _residentRepository.DeleteResidentAsync(residentId);

                Residents = await _residentRepository
                    .GetResidentByDepartmentIdAsync(SelectedDepartmentId)
                    ?? new List<ResidentModel>();

                return Result.Ok();
            }
            catch (KeyNotFoundException)
            {
                return Error.NotFound("Beboer findes ikke");
            }
            catch (Exception)
            {
                return Error.Technical("Kunne ikke slette beboer");
            }
        }

        // Tildel department
        public void SetDepartment(int departmentId)
        {
            if (departmentId <= 0)
                return;

            SelectedDepartmentId = departmentId;

            NewResident = new ResidentModel
            {
                DepartmentId = departmentId
            };
        }
    }
}

