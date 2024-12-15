﻿namespace CardOperationSerice.Middleware
{
	using System.Net;
	using System.Text.Json;

	public class GlobalExceptionMiddleware
	{
		private readonly RequestDelegate _next;
		private readonly ILogger<GlobalExceptionMiddleware> _logger;

		public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
		{
			_next = next;
			_logger = logger;
		}

		public async Task InvokeAsync(HttpContext context)
		{
			try
			{
				await _next(context);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Unhandled exception occurred."); 
				await HandleExceptionAsync(context, ex); 
			}
		}

		private static Task HandleExceptionAsync(HttpContext context, Exception exception)
		{
			// Tworzenie odpowiedzi błędu
			var response = new
			{
				Message = "An unexpected error occurred. Please try again later.",
				Error = exception.Message,
				Timestamp = DateTime.UtcNow
			};

			var jsonResponse = JsonSerializer.Serialize(response);

			// Konfiguracja odpowiedzi HTTP
			context.Response.ContentType = "application/json";
			context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

			return context.Response.WriteAsync(jsonResponse);
		}
	}

}
