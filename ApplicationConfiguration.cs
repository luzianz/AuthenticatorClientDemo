using System;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace AuthenticatorClientDemo
{
	public static class ApplicationConfiguration
	{
		private static readonly Lazy<IConfigurationRoot> configurationLazy;

		static ApplicationConfiguration()
		{
			configurationLazy = new Lazy<IConfigurationRoot>(() => {
				var currentDirPath = Directory.GetCurrentDirectory();
				var currentDir = new DirectoryInfo(currentDirPath);

				// My secrets file is located {ProjectDirectory}/../secrets/secrets.json
				return new ConfigurationBuilder()
					// Go up one directory with the Parent property
					.SetBasePath(currentDir.Parent.FullName)
					// -because we can't use '../secrets/secrets.json' here
					.AddJsonFile("secrets/secrets.json")
					.Build();
			});
		}

		public static string PresharedKey
		{
			get
			{
				return configurationLazy.Value["presharedKey"];
			}
		}

		public static string AdminEmail
		{
			get
			{
				return configurationLazy.Value["users:0:email"];
			}
		}

		public static string AdminPassword
		{
			get
			{
				return configurationLazy.Value["users:0:password"];
			}
		}
	}
}