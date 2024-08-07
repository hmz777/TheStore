namespace TheStore.SharedModels.Claims
{
    public static class DefaultAppScopes
    {
        // TODO: Later we'll seed those as default scopes but manage them dynamically via a CRUD API

        public const string CatalogUserRead = "Api.Catalog.User.Read";
        public const string CatalogUserWrite = "Api.Catalog.User.Write";
        public const string CartUserRead = "Api.Cart.User.Read";
        public const string CartUserWrite = "Api.Cart.User.Write";

        public static string[] GetDefaultUserScopes() => new string[] { CatalogUserRead, CartUserRead, CartUserWrite };
    }
}