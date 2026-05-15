using OverlapssystemAPI.Common;
using OverlapssytemApplication.Common.Errors;
using OverlapssytemApplication.Common.Result;
using System;
using System.Net;
using System.Text.Json;


namespace Overlapssystem.Services.Extensions
{
    public static class HttpResponseExtensions
    {
        public static async Task<Result<T>> ReadApiResponse<T>(this HttpResponseMessage response)
        {
            var content = await response.Content.ReadAsStringAsync();

            try
            {
                // FEJLRESPONSES
                if (!response.IsSuccessStatusCode)
                {
                    var errorResponse =
                        JsonSerializer.Deserialize<ErrorResponse>(
                            content,
                            new JsonSerializerOptions
                            {
                                PropertyNameCaseInsensitive = true
                            });

                    return errorResponse?.Error
                        ?? Error.Technical("Ukendt serverfejl");
                }

                // SUCCESSRESPONSES
                var result =
                    JsonSerializer.Deserialize<ApiResponse<T>>(
                        content,
                        new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        });

                if (result is null)
                    return Error.Technical("Serveren returnerede et ugyldigt svar");

                if (result.Data is null)
                {
                    return Error.Technical("Manglende data fra serveren");
                }

                return result.Data;
            }
            catch (Exception ex)
            {
                Console.WriteLine(content);
                Console.WriteLine(ex);

                return Error.Technical("Ugyldigt svar fra serveren");
            }
        }
    }
}
