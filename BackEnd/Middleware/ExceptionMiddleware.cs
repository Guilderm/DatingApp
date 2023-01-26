using API.Errors;

using System.Net;
using System.Text.Json;

namespace API.Middleware;

public class ExceptionMiddleware
{
	private readonly IHostEnvironment _env;
	private readonly ILogger<ExceptionMiddleware> _logger;
	private readonly RequestDelegate _next;

	public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger,
		IHostEnvironment env)
	{
		_env = env;
		_logger = logger;
		_next = next;
	}

	public async Task InvokeAsync(HttpContext context)
	{
		try
		{
			await _next(context);
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, ex.Message);
			context.Response.ContentType = "application/json";
			context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

			ApiException response = _env.IsDevelopment()
				? new ApiException(context.Response.StatusCode, ex.Message, ex.StackTrace)
				: new ApiException(context.Response.StatusCode, ex.Message, "Internal Server Error");

			JsonSerializerOptions options = new() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

			string json = JsonSerializer.Serialize(response, options);

			await context.Response.WriteAsync(json);
		}
	}
}