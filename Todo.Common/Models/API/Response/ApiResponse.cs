using System.Net;

namespace Todo.Common.Models.API.Response
{
	public class ApiResponse
	{
		public HttpStatusCode statusCode { get; set; }
		public string message { get; set; }
		public bool success { get; set; }

		public object? data { get; set; }
		public static ApiResponse Okay(object data = null, string messsage = null)
		{
			return new ApiResponse()
			{
				statusCode = HttpStatusCode.OK,
				message = messsage == null ? "Request successfully completed!" : messsage,
				data = data,
				success = true
			};
		}
		public static ApiResponse BadRequest(string message)
		{
			return new ApiResponse()
			{
				statusCode = HttpStatusCode.BadRequest,
				message = message,
				success = false
			};
		}

		public static ApiResponse NotFound(string str)
		{
			return new ApiResponse()
			{
				statusCode = HttpStatusCode.NotFound,
				message = str,
				success = false
			};
		}

		public static ApiResponse Forbidden(string str)
		{
			return new ApiResponse()
			{
				statusCode = HttpStatusCode.Forbidden,
				message = str,
				success = false
			};
		}

		public static ApiResponse ExpectationFailed(string message)
		{
			return new ApiResponse()
			{
				statusCode = HttpStatusCode.ExpectationFailed,
				message = message,
				success = false
			};
		}
	}
}
