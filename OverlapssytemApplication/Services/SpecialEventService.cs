using Microsoft.Extensions.Logging;
using OverlapssystemDomain.Entities;
using OverlapssystemDomain.Interfaces;
using OverlapssytemApplication.Common;
using OverlapssytemApplication.Common.Errors;
using OverlapssytemApplication.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OverlapssytemApplication.Services
{
    public class SpecialEventService : ISpecialEventService 
    {
        private readonly ISpecialEventRepository _specialEventRepository;
        private readonly ILogger<SpecialEventService> _logger;
        public SpecialEventService(ISpecialEventRepository specialEventRepository, ILogger<SpecialEventService> logger)
        {
            _specialEventRepository = specialEventRepository;
            _logger = logger;
        }

        public async Task<Result<List<SpecialEventModel>>> GetSpecialEventByResidentIdAsync(int residentId)
        {
            if (residentId <= 0)
                return Error.Validation("Ugyldigt beboer ID");

            try
            {
                var specialEventList = await _specialEventRepository.GetSpecialEventByResidentId(residentId);

                // Filtrer for at kun få særlige hændelser inden for de sidste 48 timer
                var cutoff = DateTime.Now.AddHours(-48);

                var filtered = specialEventList
                    .Where(x => x.SpecialEventDateTime >= cutoff)
                    .OrderByDescending(x => x.SpecialEventDateTime)
                    .ToList();

                return filtered; // implicit success
                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fejl ved hentning af særlige hændelser");
                return Error.Technical("Kunne ikke hente særlige hændelser");
            }
        }

        public async Task<Result<int>> CreateSpecialEventAsync(SpecialEventModel specialEvent)
        {
            if (specialEvent.ResidentID <= 0)
                return Error.Validation("Ugyldigt beboer ID");

            try
            {
                var id = await _specialEventRepository.SaveNewSpecialEvent(specialEvent);
                return id; // implicit success
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fejl ved oprettelse af særlig hændelse");
                return Error.Technical("Kunne ikke oprette særlig hændelse");
            }
        }

        public async Task<Result> UpdateSpecialEventAsync(SpecialEventModel specialEvent)
        {
            if (specialEvent.SpecialEventID <= 0)
                return Error.Validation("Ugyldigt særlig hændelse ID");

            if (specialEvent.ResidentID <= 0)
                return Error.Validation("Ugyldigt beboer ID");

            try
            {
                await _specialEventRepository.UpdateSpecialEvent(specialEvent);
                return Result.Ok();
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "Særlig hændelse blev ikke fundet");
                return Error.NotFound("Kunne ikke finde særlig hændelse at opdatere");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fejl ved opdatering af særlig hændelse");
                return Error.Technical("Kunne ikke opdatere særlig hændelse");
            }
        }
        public async Task<Result> DeleteSpecialEventAsync(int specialEventID)
        {
            if (specialEventID <= 0)
                return Error.Validation("Ugyldigt særlig hændelse ID");

            try
            {
                await _specialEventRepository.DeleteSpecialEvent(specialEventID);
                return Result.Ok();
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "Særlig hændelse blev ikke fundet");
                return Error.NotFound("Kunne ikke finde særlig hændelse at slette");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fejl ved sletning af særlig hændelse");
                return Error.Technical("Kunne ikke slette særlig hændelse");
            }
        }
    }
}
