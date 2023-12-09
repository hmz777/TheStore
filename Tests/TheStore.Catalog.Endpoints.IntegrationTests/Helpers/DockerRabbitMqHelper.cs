using Docker.DotNet;
using Docker.DotNet.Models;

namespace TheStore.Catalog.Endpoints.IntegrationTests.Helpers
{
	public class DockerRabbitMqHelper : IAsyncDisposable
	{
		private readonly DockerClient dockerClient;
		private string? containerId;
		private readonly string containerName;

		public bool DatabaseServerStarted { get; private set; }
		public int Port { get; private set; }
		public int ManagementPort { get; private set; }
		public string Username { get; private set; }
		public string Password { get; private set; }

		public DockerRabbitMqHelper()
		{
			dockerClient = new DockerClientConfiguration().CreateClient();

			Port = 5672;
			ManagementPort = 15672;
			Username = "guest";
			Password = "guest";
			containerName = "RabbitMq_Tests";
		}

		public async Task StartDatabaseServer(CancellationToken cancellationToken = default)
		{
			if (await ContainerExistsAsync(containerName, cancellationToken) == false)
			{
				await dockerClient.Containers.CreateContainerAsync(new CreateContainerParameters()
				{
					Image = "rabbitmq:3.12-management",
					HostConfig = new HostConfig
					{
						PortBindings = new Dictionary<string, IList<PortBinding>>
					{
						{
							"5672/tcp",
							new List<PortBinding>
							{
								new() {
									HostPort = $"{Port}"
								}
							}
						},
						{
							"15672/tcp",
							new List<PortBinding>
							{
								new() {
									HostPort = $"{ManagementPort}"
								}
							}
						}
					},
					},
					ExposedPorts = new Dictionary<string, EmptyStruct>
					{
						[$"{Port}/tcp"] = new EmptyStruct(),
						[$"{ManagementPort}/tcp"] = new EmptyStruct()
					},
					Name = containerName
				}, cancellationToken);
			}

			await SetContainerIdAsync(containerName, cancellationToken);

			await dockerClient.Containers.StartContainerAsync(containerId, new ContainerStartParameters(), cancellationToken);

			DatabaseServerStarted = true;

			// Wait for the container to start
			await Task.Delay(6000, cancellationToken);
		}

		public async Task StopDatabaseServer(CancellationToken cancellationToken = default)
		{
			if (DatabaseServerStarted == false)
				return;

			await dockerClient.Containers.StopContainerAsync(containerId, new ContainerStopParameters
			{
				WaitBeforeKillSeconds = 10
			}, cancellationToken);
		}

		private async Task<bool> ContainerExistsAsync(string containerName, CancellationToken cancellationToken = default)
		{
			var containers = await dockerClient.Containers.ListContainersAsync(new ContainersListParameters()
			{
				All = true
			}, cancellationToken);

			return containers.Any(c => c.Names.Contains("/" + containerName));
		}

		private async Task SetContainerIdAsync(string containerName, CancellationToken cancellationToken = default)
		{
			var containers = await dockerClient.Containers.ListContainersAsync(new ContainersListParameters()
			{
				All = true
			}, cancellationToken);

			containerId = containers.First(c => c.Names.Contains("/" + containerName)).ID;
		}

		public async ValueTask DisposeAsync()
		{
			await dockerClient.Containers.StopContainerAsync(containerName, new ContainerStopParameters());
			await dockerClient.Containers.RemoveContainerAsync(containerName,
				new ContainerRemoveParameters() { Force = true });
			dockerClient.Dispose();
		}
	}
}