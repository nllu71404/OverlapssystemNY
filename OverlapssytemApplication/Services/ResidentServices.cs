using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OverlapssystemDomain.Entities;
using OverlapssystemDomain.Enums;
using OverlapssystemDomain.Interfaces;
using OverlapssytemApplication.Interfaces;


namespace OverlapssytemApplication.Services
{
    public class ResidentServices : IResidentServices
    {
        private readonly IResidentRepository _residentRepository;
        private readonly IMedicinRepository _medicinRepository;

        public ResidentServices(IResidentRepository residentRepository, IMedicinRepository medicinRepository)
        {
            _residentRepository = residentRepository;
            _medicinRepository = medicinRepository;
        }


        public List<ResidentModel> Residents { get; private set; } = new();
        public int SelectedDepartmentId { get; set; } = 1;

        public ResidentModel NewResident { get; set; } = new ResidentModel
        {
            Risiko = Risiko.Green
        };


        public async Task<List<ResidentModel>> LoadResidentsAsync()
        {
            return Residents = await _residentRepository.GetAllResidentsAsync();
        }

        public async Task<List<ResidentModel>> LoadResidentsByDepartmentAsync(int departmentId)
        {
            return Residents = await _residentRepository.GetResidentByDepartmentIdAsync(departmentId);
        }

        //public async Task UpdateResidentAsync(ResidentModel resident)
        //{
        //    resident.DepartmentId = SelectedDepartmentId;
        //    await _residentRepository.UpdateResidentAsync(resident);
        //    Residents = await _residentRepository.GetResidentByDepartmentIdAsync(resident.departmentId);
        //}
        public async Task UpdateResidentAsync(ResidentModel resident)
        {
            await _residentRepository.UpdateResidentAsync(resident);
            Residents = await _residentRepository.GetResidentByDepartmentIdAsync(resident.DepartmentId ?? 1);
        }

        //public async Task DeleteResidentAsync(int residentId)
        //{
        //    await _residentRepository.DeleteResidentAsync(residentId);
        //    Residents = await _residentRepository.GetResidentByDepartmentIdAsync(SelectedDepartmentId);
        //}

        public async Task DeleteResidentAsync(int residentId)
        {
            await _residentRepository.DeleteResidentAsync(residentId);
        }

        //public async Task CreateResidentAsync(ResidentModel resident)
        //{
        //    NewResident.DepartmentId = SelectedDepartmentId;
        //    await _residentRepository.SaveNewResidentAsync(NewResident);

        //    NewResident = new ResidentModel
        //    {
        //        Risiko = Risiko.Green,
        //        DepartmentId = SelectedDepartmentId
        //    };

        //    Residents = await _residentRepository.GetResidentByDepartmentIdAsync(SelectedDepartmentId);
        //}

        public async Task CreateResidentAsync(ResidentModel resident)
        {
            await _residentRepository.SaveNewResidentAsync(resident);
            Residents = await _residentRepository.GetResidentByDepartmentIdAsync(resident.DepartmentId ?? 1);
        }



        public void SetDepartment(int departmentId)
        {
            SelectedDepartmentId = departmentId;
            NewResident = new ResidentModel
            {
                DepartmentId = departmentId
            };
        }

        public async Task LoadMedicinTimesAsync(ResidentModel resident)
        {
            resident.MedicinTimes = await _medicinRepository.GetMedicinByResidentIdAsync(resident.ResidentId);
        }
        public async Task AddMedicinTimeAsync(int residentId, DateTime datetime)
        {
            var medTime = new MedicinModel
            {
                ResidentID = residentId,
                MedicinTime = datetime,
                MedicinCheckTimeStamp = null
            };
            await _medicinRepository.SaveNewMedicinAsync(medTime);
        }
        public async Task ToggleMedicinGivenAsync(MedicinModel medTime) //Bruger vi denne metode nogen steder?:-)
        {
            medTime.MedicinCheckTimeStamp = medTime.MedicinCheckTimeStamp != null ? (DateTime?)null : DateTime.Now;
            await _medicinRepository.UpdateMedicinAsync(medTime);
        }
    }
}
