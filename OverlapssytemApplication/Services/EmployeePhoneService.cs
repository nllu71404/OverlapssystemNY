using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OverlapssystemDomain.Entities;
using OverlapssytemApplication.Common;
using OverlapssytemApplication.Interfaces;
using OverlapssystemDomain.Interfaces;

namespace OverlapssytemApplication.Services
{
    public class EmployeePhoneService : IEmployeePhoneService
    {
        private readonly IEmployeePhoneRepository _employeePhoneRepository;

        public EmployeePhoneService(IEmployeePhoneRepository employeePhoneRepository)
        {
            _employeePhoneRepository = employeePhoneRepository;
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
                        catch (Exception ex)
            {
                return Result.Fail($"Fejl ved sletning af medarbejder telefon: {ex.Message}");
            }
        }
        
        // Hent alle
        public async Task<Result<List<EmployeePhoneModel>>> GetAllEmployeePhoneNumbersAsync()
        {
            var result = await _employeePhoneRepository.GetAllEmployeePhoneNumbersAsync();
            
            EmployeePhones = result ?? new List<EmployeePhoneModel>();
            
            return Result<List<EmployeePhoneModel>>.Ok(EmployeePhones);

        }
        
        //Hent på ID
        public async Task<Result<EmployeePhoneModel>> GetEmployeePhoneByIdAsync(int employeePhoneId)
        {
            var result = await _employeePhoneRepository.GetEmployeePhoneByIdAsync(employeePhoneId);
            
            if (result == null)
                return Result<EmployeePhoneModel>.Fail("Medarbejder telefon blev ikke fundet");
            
            return Result<EmployeePhoneModel>.Ok(result);

        }

        //Hent på DepartmentID
        public async Task<Result<List<EmployeePhoneModel>>> GetEmployeePhonesByDepartmentIdAsync(int departmentId)
        {
            var result = await _employeePhoneRepository.GetEmployeePhonesByDepartmentIdAsync(departmentId);
            
            EmployeePhones = result ?? new List<EmployeePhoneModel>();
            
            return Result<List<EmployeePhoneModel>>.Ok(EmployeePhones);
        }

        //Gem ny medarbejder telefon
        public async Task<Result<int>> SaveNewEmployeePhoneAsync(EmployeePhoneModel employeePhone)
        {
            try
            {
                var id = await _employeePhoneRepository.SaveNewEmployeePhoneAsync(employeePhone);
                return Result<int>.Ok(id);
            }
            catch (Exception ex)
            {
                return Result<int>.Fail($"Fejl ved oprettelse af medarbejder telefon: {ex.Message}");
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
            catch (Exception ex)
            {
                return Result.Fail($"Fejl ved opdatering af medarbejder telefon: {ex.Message}");
            }
        }
    }
}
