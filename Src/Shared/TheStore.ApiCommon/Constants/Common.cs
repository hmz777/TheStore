namespace TheStore.ApiCommon.Constants
{
	public class ConfigurationKeys
	{
		public static RunningPlatform RunningPlatform { get; set; }

		public class ConnectionStrings
		{
			// Database server name for Docker Compose or Kubernetes deployment
			public const string DockerComposeData = $"{nameof(ConnectionStrings)}__DOCKER_CONNECTION_STRING";
			public const string KubernetesData = $"{nameof(ConnectionStrings)}__K8S_CONNECTION_STRING";
			public const string StandaloneData = $"{nameof(ConnectionStrings)}__SA_CONNECTION_STRING";
		}

		public class Deployment
		{
			// If we're running on Kubernetes
			public const string IsKubernetes = $"{nameof(Deployment)}__ISK8S";
			public const string IsDockerCompose = $"{nameof(Deployment)}__ISDC";
		}

		public class Logging
		{
			public const string Seq = $"{nameof(Logging)}__SEQ_URL";
		}

		public class Identity
		{
			public const string IdentityDockerCompose = $"{nameof(Identity)}__IDENTITY_DOCKER_URL";
			public const string IdentityKubernetes = $"{nameof(Identity)}__IDENTITY_K8S_URL";
			public const string IdentityStandalone = $"{nameof(Identity)}__IDENTITY_SA_URL";
		}
	}

	public class ResourceFilePaths
	{
		public const string ImagesPathBase = "images";
		public const string ProductsImages = @$"{ImagesPathBase}\productsimages";
		public const string BranchesImages = @$"{ImagesPathBase}\branchesimages";
	}
}