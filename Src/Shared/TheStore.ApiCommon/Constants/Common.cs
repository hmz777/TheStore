namespace TheStore.ApiCommon.Constants
{
	public class ConfigurationKeys
	{
		public static RunningPlatform RunningPlatform { get; set; }

		public class ConnectionStrings
		{
			// Database server name for Docker Compose or Kubernetes deployment
			public const string ConnectionString = "CONNECTIONSTRING";
			public const string DbUser = "DBUSER";
			public const string DbPassword = "DBPASS";
		}

		public class Deployment
		{
			// If we're running on Kubernetes
			public const string IsKubernetes = $"ISK8S";
			public const string IsDockerCompose = $"ISDC";
		}

		public class Logging
		{
			public const string Seq = $"SEQURL";
		}

		public class Identity
		{
			public const string IdentityServer = $"IDENTITYSERVER";
		}

		public class RabbitMqConfig
		{
			public const string RabbitMqHost = "RMQHOST";
			public const string RabbitMqPort = "RMQPORT";
			public const string RabbitMqUsername = "RMQUSER";
			public const string RabbitMqPassword = "RMQPASS";
		}

		public class Testing
		{
			public const string ApplyMigrationsAtRuntime = "APPLYMIGRATIONSATRUNTIME";
		}
	}

	public class ResourceFilePaths
	{
		public const string ImagesPathBase = "images";
		public const string ProductsImages = @$"{ImagesPathBase}\productsimages";
		public const string BranchesImages = @$"{ImagesPathBase}\branchesimages";
	}
}