using OverlapssystemAPI.Common;
using OverlapssytemApplication.Common.Errors;
using OverlapssytemApplication.Common.Result;
using System;
using System.Text.Json;


namespace Overlapssystem.Services.Extensions
{
    public static class HttpResponseExtensions
    {
        public static async Task<Result<T>> ReadApiResponse<T>(this HttpResponseMessage response)
        {
            var content = await response.Content.ReadAsStringAsync();

            ApiResponse<T>? result;

            try
            {
                result = JsonSerializer.Deserialize<ApiResponse<T>>(
                    content,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
            }
            catch (Exception)
            {
                return Error.Technical("Ugyldigt svar fra serveren");
            }

            if (result is null)
                return Error.Technical("Tomt svar fra serveren");

            if (!response.IsSuccessStatusCode)
            {
                return Error.Technical(
                    "Serverfejl - prøv igen senere");
            }

            if (!result.Success)
            {
                return Error.Validation(
                    result.Error ?? "Ukendt API fejl");
            }

            if (result.Data is null)
            {
                return Error.Technical("Manglende data fra serveren");
            }

            return result.Data;
        }
    }
}
