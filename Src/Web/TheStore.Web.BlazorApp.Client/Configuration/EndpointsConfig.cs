namespace TheStore.Web.BlazorApp.Client.Configuration
{
    public class EndpointsConfig
    {
        public const string Key = "Apis";
        public const string CatalogEndpointKey = "Catalog Api";
        public const string CartEndpointKey = "Cart Api";
        public List<Endpoint> Endpoints { get; set; } = [];

        public Endpoint GetCatalogEndpoint() => Endpoints.First(endpoint => endpoint.Name == CatalogEndpointKey);
        public Endpoint GetCartEndpoint() => Endpoints.First(endpoint => endpoint.Name == CartEndpointKey);
    }
}