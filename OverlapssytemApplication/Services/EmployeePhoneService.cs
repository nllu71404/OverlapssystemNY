using Microsoft.Extensions.Logging;
using OverlapssystemDomain.Entities;
using OverlapssystemDomain.Interfaces;
using OverlapssytemApplication.Common;
using OverlapssytemApplication.Common.Errors;
using OverlapssytemApplication.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OverlapssytemApplication.Services
{
    public class EmployeePhoneService : IEmployeePhoneService
    {
        private readonly IEmployeePhoneRepository _employeePhoneRepository;
        private readonly ILogger<EmployeePhoneService> _logger;

        public EmployeePhoneService(IEmployeePhoneRepository employeePhoneRepository, ILogger<EmployeePhoneService> logger)
        {
            _employeePhoneRepository = employeePhoneRepository;
            _logger = logger;
        }

        public List<EmployeePhoneModel> EmployeePhones { get; private set; } = new();
        
        // Slet medarbejder telefon
        public async Task<Result> DeleteEmployeePhoneAsync(int employeePhoneId)
        {
            try
            {
                await _employeePhoneRepository.DeleteEmployeePhoneAsync(employeePhoneId);
                return Result.Ok();
            }
                        catch (Exception)
            {
                
                return Error.Technical("Fejl ved sletning af medarbejder telefon!");
            }
        }
        
        // Hent alle
        public async Task<Result<List<EmployeePhoneModel>>> GetAllEmployeePhoneNumbersAsync()
        {
            try
            {
                var result = await _employeePhoneRepository.GetAllEmployeePhoneNumbersAsync();
                EmployeePhones = result ?? new List<EmployeePhoneModel>();
                return EmployeePhones;
            }
            catch (Exception)
            {
                return Error.Technical("Kunne ikke hente telefonnummer");
            }

        }
        
        //Hent på ID
        public async Task<Result<EmployeePhoneModel>> GetEmployeePhoneByIdAsync(int employeePhoneId)
        {
            if (employeePhoneId <= 0)
                return Error.Validation("Ugyldigt telefonnummer med det ID");

            try
            {
                var result = await _employeePhoneRepository.GetEmployeePhoneByIdAsync(employeePhoneId);
                return result;
            }
            catch (Exception)
            {
                return Error.Technical("Kunne ikke hente medarbejdertelefon");
            }
        }

        //Hent på DepartmentID
        public async Task<Result<List<EmployeePhoneModel>>> GetEmployeePhonesByDepartmentIdAsync(int departmentId)
        {
            if (departmentId <= 0)
                return Error.Validation("Ugyldigt afdelings ID");
            try
            {
                var result = await _employeePhoneRepository.GetEmployeePhonesByDepartmentIdAsync(departmentId);
                EmployeePhones = result ?? new List<EmployeePhoneModel>();
                return EmployeePhones;
            }
            catch (Exception)
            {
                return Error.Technical("Kunne ikke hendes telefonnumre for afdeling");
            }
        }

        //Gem ny medarbejder telefon
        public async Task<Result<int>> SaveNewEmployeePhoneAsync(EmployeePhoneModel employeePhone)
        {
            if (employeePhone == null)
                return Error.Validation("Telefonnumret må ikke være nul");
            
            try
            {
                var id = await _employeePhoneRepository.SaveNewEmployeePhoneAsync(employeePhone);
                return id;
            }
            catch (Exception)
            {

                return Error.Technical("Fejl ved oprettelse af medarbejder telefon!");
            }
        }
        
        //Opdater medarbejder telefon
        public async Task<Result> UpdateEmployeePhoneAsync(EmployeePhoneModel employeePhone)
        {
            try
            {
                await _employeePhoneRepository.UpdateEmployeePhoneAsync(employeePhone);
                return Result.Ok();
            }
            catch (Exception)
            {
                return Error.Technical("Fejl ved opdatering af medarbejder telefon");
            }
        }
    }
}
