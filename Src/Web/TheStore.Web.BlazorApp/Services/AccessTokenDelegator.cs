namespace TheStore.Web.BlazorApp.Services
{
    // TODO: It looks stable but integration test it for edge cases
    public class AccessTokenDelegator
    {
        private static readonly AsyncLocal<string?> accessToken = new();

        public string? AccessToken
        {
            get { return accessToken.Value; }
            set { accessToken.Value = value!; }
        }

    }
}