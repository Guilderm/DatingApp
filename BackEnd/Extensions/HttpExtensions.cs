using BackEnd.Helpers;
using System.Text.Json;

namespace BackEnd.Extensions;

public static class HttpExtensions
{
    public static void AddPaginationHeader(this HttpResponse response, PaginationHeader header)
    {
        JsonSerializerOptions jsonOptions = new() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
        response.Headers.Add("Pagination", JsonSerializer.Serialize(header, jsonOptions));
        response.Headers.Add("Access-Control-Expose-Headers", "Pagination");
    }
}