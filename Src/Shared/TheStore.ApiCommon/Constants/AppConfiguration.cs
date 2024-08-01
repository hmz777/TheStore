namespace TheStore.ApiCommon.Constants
{
    public static class AppConfiguration
    {
        public static RunningPlatform RunningPlatform { get; set; }

        public static class ConnectionStrings
        {
            // Database server name for Docker Compose or Kubernetes deployment
            public const string ConnectionStringConfigKey = "CONNECTIONSTRING";
            public const string DbUserConfigKey = "DBUSER";
            public const string DbPasswordConfigKey = "DBPASS";
        }

        public static class Deployment
        {
            // If we're running on Kubernetes
            public const string IsKubernetesConfigKey = $"ISK8S";
            public const string IsDockerComposeConfigKey = $"ISDC";
        }

        public static class Logging
        {
            public const string LoggingTemplate = "[{@t:dd/MM/yyyy - HH:mm:ss} {@l:u3}{#if CorrelationId is not null} ( - {CorrelationId}){#end}] {@m:lj}\n{@x}";
            public const string SeqConfigKey = $"SEQURL";
        }

        public static class Identity
        {
            public const string IdentityServerConfigKey = $"IDENTITYSERVER";
        }

        public static class RabbitMqConfig
        {
            public const string RabbitMqHostConfigKey = "RMQHOST";
            public const string RabbitMqPortConfigKey = "RMQPORT";
            public const string RabbitMqUsernameConfigKey = "RMQUSER";
            public const string RabbitMqPasswordConfigKey = "RMQPASS";
        }

        public static class Testing
        {
            public const string ApplyMigrationsAtRuntimeEnvVarName = "APPLYMIGRATIONSATRUNTIME";
        }

        public static class Services
        {
            public const string CorsPolicyName = "Cors";
        }
    }

    public static class ResourceFilePaths
    {
        public const string ImagesPathBase = "images";
        public const string ProductsImages = @$"{ImagesPathBase}\productsimages";
        public const string BranchesImages = @$"{ImagesPathBase}\branchesimages";
    }
}