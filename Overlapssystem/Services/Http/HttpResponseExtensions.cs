using OverlapssystemAPI.Common;
using OverlapssytemApplication.Common.Errors;
using OverlapssytemApplication.Common.Result;
using System;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;


namespace Overlapssystem.Services.Extensions
{
    //Vi bruger denne klasse til at håndtere alle API-responser på en ensartet måde.

    // Den forsøger først at parse fejlresponser, og hvis det ikke lykkes, håndterer den det som en teknisk fejl.
    // For succesresponser forsøger den at parse dataen, og hvis det ikke lykkes, håndterer den det som en teknisk fejl.

    //Bruges også for at undgå gentagelse af kode i alle vores API-kald, da håndtering af responser ofte involverer det samme mønster af parsing og fejlhåndtering.
    public static class HttpResponseExtensions
    {
        public static async Task<Result<T>> ReadApiResponse<T>(this HttpResponseMessage response)
        {
            var content = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                NumberHandling = JsonNumberHandling.AllowReadingFromString
            };

            try
            {
                // FEJLRESPONSES
                if (!response.IsSuccessStatusCode)
                {
                    var errorResponse =
                        JsonSerializer.Deserialize<ErrorResponse>(content, options);

                    if (errorResponse?.Error != null)
                        return errorResponse.Error;

                    Console.WriteLine("ERROR RESPONSE COULD NOT BE PARSED:");
                    Console.WriteLine(content);

                    return Error.Technical("Ukendt serverfejl");
                }

                // SUCCESSRESPONSES
                var result =
                    JsonSerializer.Deserialize<ApiResponse<T>>(content, options);

                if (result is null)
                    return Error.Technical("Serveren returnerede et ugyldigt svar");


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