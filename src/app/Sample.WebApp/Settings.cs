namespace Sample.WebApp
{
    public class AuthToken
    {
        public string Authority { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string Scope { get; set; }
    }

    public class HttpConfigAttribs
    {
        public string Name { get; set; }
        public string BaseUrl { get; set; }
        public string MediaType { get; set; }
    }

    public class ServiceHost
    {
        public string ResourceServer { get; set; }
    }

    public static class NamedHttpClients
    {
        public const string EmployeeServiceClient = "empsrvclient";
    }

    public static class Constants
    {
        public const string MediaTypeAppJson = "application/json";
    }
}
