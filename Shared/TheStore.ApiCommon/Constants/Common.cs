using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheStore.ApiCommon.Constants
{
	public class Common
	{
		public class ConfigurationKeys
		{
			public class ConnectionStrings
			{
				// Database server name for Docker Compose or Kubernetes deployment
				public const string DockerData = $"{nameof(ConnectionStrings)}__DOCKER_CONNECTION_STRING";
				public const string KubernetesData = $"{nameof(ConnectionStrings)}__K8S_CONNECTION_STRING";
			}

			public class Deployment
			{
				// If we're running on Kubernetes
				public const string IsKubernetes = $"{nameof(Deployment)}__ISK8S";
				public const string IsCompose = $"{nameof(Deployment)}__ISDC";
			}

			public class Logging
			{
				public const string Seq = $"{nameof(Logging)}__SEQ_URL";
			}

			public class Identity
			{
				public const string IdentityDocker = $"{nameof(Identity)}__IDENTITY_DOCKER_URL";
				public const string IdentityKubernetes = $"{nameof(Identity)}__IDENTITY_K8S_URL";
			}
		}
	}
}