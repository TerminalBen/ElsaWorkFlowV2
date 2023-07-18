using Rebus.Logging;
using System.Net.Http.Headers;

namespace ElsaQuickstarts.Server.DashboardAndServer
{
    
        public class Communication
        {

            public static HttpClient GetClient()
            {

                try
                {
                    HttpClientHandler handler = new HttpClientHandler
                    {
                        ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
                    };
                    var newHttpClient = new HttpClient(handler);
                    MediaTypeWithQualityHeaderValue contentType = new MediaTypeWithQualityHeaderValue("application/json");
                    newHttpClient.DefaultRequestHeaders.Accept.Add(contentType);
                    newHttpClient.Timeout = new TimeSpan(1, 0, 0);
                   
                    return newHttpClient;

                }
                catch (Exception ex)
                {

                    
                    throw ex;
                }
            }
        }
    
}
