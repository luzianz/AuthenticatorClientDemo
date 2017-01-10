using System.Threading.Tasks;
using static System.Console;
using static AuthenticatorClientDemo.ApplicationConfiguration;

namespace AuthenticatorClientDemo
{
    public class Demo
	{
		public static void Main(string[] args)
		{
			var demo = new Demo();
			demo.ExecuteAsync().Wait();
		}

		public async Task ExecuteAsync()
		{
			using (var client = HttpExtensions.CreateHttpClientAcceptingAnyCert())
			{
				var claims = await client.GetClaimsAsync(AdminEmail, AdminPassword, PresharedKey);
				WriteLine(claims);
			}
		}
	}
}
