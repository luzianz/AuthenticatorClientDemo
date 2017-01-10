using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using static Newtonsoft.Json.JsonConvert;
using static JwtCore.JsonWebToken;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;

namespace AuthenticatorClientDemo
{
	using Responses;

	public static class HttpExtensions
	{
		public static async Task<ClaimsResponse> GetClaimsAsync(this HttpClient client, string email, string password, string key)
		{
			var url = "https://localhost/api/authenticator/claims";
			using (var request = new HttpRequestMessage(HttpMethod.Get, url))
			{
				request.Headers.AddBasicAuthorization(email, password);
				var httpResponse = await client.SendAsync(request);
				var stringResponse = await httpResponse.Content.ReadAsStringAsync();
				var response = DeserializeObject<GetClaimsResponse>(stringResponse);
				var tokenJsonString = Decode(response.Token, key);
				var claims = DeserializeObject<ClaimsResponse>(tokenJsonString);
				return claims;
			}
		}

		public static void AddBasicAuthorization(this HttpRequestHeaders headers, string userName, string password)
		{
			var userNamePassword = $"{userName}:{password}";
			var userNamePasswordBytes = Encoding.ASCII.GetBytes(userNamePassword);
			var parameter = Convert.ToBase64String(userNamePasswordBytes);
			headers.Authorization = new AuthenticationHeaderValue("Basic", parameter);
		}

		#region Needed for self-signed certificates
		public static HttpClient CreateHttpClientAcceptingAnyCert()
		{
			return new HttpClient(new HttpClientHandler {
				ServerCertificateCustomValidationCallback = AcceptAnyCert
			});
		}

		private static bool AcceptAnyCert(
			HttpRequestMessage httpRequestMessage, 
			X509Certificate2 cert,
            X509Chain x509chain,
            SslPolicyErrors sslPolicyErrors) => true;
		#endregion
	}
}