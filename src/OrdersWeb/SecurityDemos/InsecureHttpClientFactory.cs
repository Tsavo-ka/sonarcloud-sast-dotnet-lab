namespace OrdersWeb.SecurityDemos;

public static class InsecureHttpClientFactory
{
    // HOTSPOT/VULN: certificate validation bypass
    public static HttpClient CreateInsecureClient()
    {
        var handler = new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback =
                HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
        };
        return new HttpClient(handler);
    }
}
