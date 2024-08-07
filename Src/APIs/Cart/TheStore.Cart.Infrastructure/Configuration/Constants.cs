namespace TheStore.Cart.Infrastructure.Configuration
{
    public static class Constants
    {
        public static class Database
        {
            public const string DatabaseName = "CartDb";
        }

        public static class ValidationMessages
        {
            // TODO: Show friendlier messages to the end user

            public const string InvalidSubmittedData = "Submitted data are invalid";
            public const string CartNotFound = "Cart not found";
        }

        public static class Authorization
        {
            public const string DefaultPolicy = "Default Policy";
            public const string ReadPolicy = "Read";
            public const string WritePolicy = "Write";
        }
    }
}