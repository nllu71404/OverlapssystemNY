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


namespace OverlapssytemApplication.Services
{

        public class ResidentServices : IResidentServices
        {
            private readonly IResidentRepository _residentRepository;

            public ResidentServices(IResidentRepository residentRepository)
            {
                _residentRepository = residentRepository;
            }

            public List<ResidentModel> Residents { get; private set; } = new();

            public int SelectedDepartmentId { get; set; } = 1;

            public ResidentModel NewResident { get; set; } = new ResidentModel
            {
                Risiko = Risiko.Green
            };

        //Hent
        public async Task<Result<List<ResidentModel>>> LoadResidentsAsync()
            {
                var data = await _residentRepository.GetAllResidentsAsync();

                Residents = data ?? new List<ResidentModel>();

                return Result<List<ResidentModel>>.Ok(Residents);
            }

        //Hent på DepartmentId
        public async Task<Result<List<ResidentModel>>> LoadResidentsByDepartmentAsync(int departmentId)
            {
                if (departmentId <= 0 || departmentId > 2 )
                    return Result<List<ResidentModel>>.Fail("Ugyldigt afdelingsID");

                var data = await _residentRepository.GetResidentByDepartmentIdAsync(departmentId);

                Residents = data ?? new List<ResidentModel>();

                return Result<List<ResidentModel>>.Ok(Residents);
            }
        //Opret
        public async Task<Result<int>> CreateResidentAsync(ResidentModel resident)
            {

                if (string.IsNullOrWhiteSpace(resident.Name))
                    return Result<int>.Fail("Navn er påkrævet");

                if (resident.DepartmentId == null || resident.DepartmentId <= 0)
                    return Result<int>.Fail("Afdelings ID er ikke sat");

                var id = await _residentRepository.SaveNewResidentAsync(resident);

                Residents = await _residentRepository.GetResidentByDepartmentIdAsync(resident.DepartmentId.Value);

                return Result<int>.Ok(id);
            }

        //Update
        public async Task<Result> UpdateResidentAsync(ResidentModel resident)
            {

                if (resident.ResidentId <= 0)
                    return Result.Fail("Ugyldigt beboer ID");

                if (string.IsNullOrWhiteSpace(resident.Name))
                    return Result.Fail("Navn er påkrævet");

                await _residentRepository.UpdateResidentAsync(resident);

                Residents = await _residentRepository.GetResidentByDepartmentIdAsync(resident.DepartmentId ?? SelectedDepartmentId);

                return Result.Ok();
            }
        //Delete
        public async Task<Result> DeleteResidentAsync(int residentId)
            {
                if (residentId <= 0)
                    return Result.Fail("Ugyldigt ID");

                await _residentRepository.DeleteResidentAsync(residentId);

                Residents = await _residentRepository.GetResidentByDepartmentIdAsync(SelectedDepartmentId);

                return Result.Ok();
            }

        //Tildel department
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

