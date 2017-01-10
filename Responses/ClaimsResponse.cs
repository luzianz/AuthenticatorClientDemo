using System;
using System.Text;
using Newtonsoft.Json;

namespace AuthenticatorClientDemo.Responses
{
	public class ClaimsResponse
	{
		[JsonProperty("iss")]
		public string Issuer { get; set; }

		[JsonProperty("email")]
		public string Email { get; set; }

		[JsonProperty("iat")]
		[JsonConverter(typeof(JsonConverters.UnixTimeConverter))]
		public DateTime WhenIssued { get; set; }

		[JsonProperty("exp")]
		[JsonConverter(typeof(JsonConverters.UnixTimeConverter))]
		public DateTime WhenExpires { get; set; }

		public override string ToString()
		{
            var sb = new StringBuilder();
			sb.AppendLine($"Issuer: {Issuer}");
			sb.AppendLine($"Email: {Email}");
			sb.AppendLine($"Expires: {WhenExpires}");
			sb.AppendLine($"When Issued: {WhenIssued}");

			return sb.ToString();
		}
	}
}