using OverlapssystemAPI.Common;
using System;
using System.Text.Json;


namespace Overlapssystem.Services.Extensions
{
    public static class HttpResponseExtensions
    {
        public static async Task<T> ReadApiResponse<T>(this HttpResponseMessage response)
        {
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(
                    $"HTTP {(int)response.StatusCode}: {content}");
            }

            var result = JsonSerializer.Deserialize<ApiResponse<T>>(content,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

            if (result == null)
                throw new Exception("Tom API response");

            if (!result.Success)
                throw new Exception(result.Error ?? "Ukendt API fejl");

            return result.Data!;
        }
    }
}
