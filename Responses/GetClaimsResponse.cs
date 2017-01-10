using Newtonsoft.Json;

namespace AuthenticatorClientDemo.Responses
{
	public class GetClaimsResponse
	{
		[JsonProperty("token")]
		public string Token { get; set; }
	}
}