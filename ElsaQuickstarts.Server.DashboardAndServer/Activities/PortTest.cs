using System.IO;
using System.Threading.Tasks;
using Elsa.ActivityResults;
using Elsa.Attributes;
using Elsa.Expressions;
using Elsa.Providers.WorkflowStorage;
using Elsa.Services;
using Elsa.Services.Models;
using Elsa.Attributes;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Collections.Specialized;
using System.Net;
using System.Text;
using DotLiquid;
using System.Net.Sockets;

namespace ElsaQuickstarts.Server.DashboardAndServer.Activities
{
    [Action(Category = "Connection", Description = "Tests the availability of a port")]
    public class PortTest:Activity
    {
        public PortTest():base() { }

        #region Inputs

        [ActivityInput(Label = @"Target System", Name ="TargetSystem", Hint = @"Enter the IP Address or the Name of the target system",
            IsBrowsable = true, Category = "")]

        public string TargetSystem
        {
            get; set;
        } = default!;


        [ActivityInput(Label = @"Port", Name = "Port", Hint = @"Enter the Port to be checked on the target system",
            IsBrowsable = true, Category = "")]

        public Int32 Port
        {
            get; set;
        } = default!;

        [ActivityInput(Label = @"Timeout", Name = "Timeout", Hint = @"Enter timeout in ms for the connection test",
            IsBrowsable = true, Category = "")]

        public Int32 Timeout
        {
            get; set;
        } = default!;
        #endregion

        #region
       
        [ActivityOutput(Hint = @"Outputs True if the port is available, False if not.")]
        public bool PortAvailability
        {
            get; set;
        } = default!;

        #endregion

        #region Auxiliary Code

        // callback used to validate the certificate in an SSL conversation
        private static bool ValidateRemoteCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors policyErrors)
        {
            return true;
        }

        private static string SendHTTPRequest(NameValueCollection data, List<Tuple<string,string>> headers, string body, string method, string url, out HttpStatusCode httpStatusCode, NetworkCredential credentials)
        {
            StringBuilder postData = new StringBuilder();
            if ((body == "" && data != null && data.Count > 0) || body == null)
            {
                foreach (string key in data)
                {
                    postData.Append(key + "=" + data[key] + "&");
                }
            }
            if (body != null && body != "")
            {
                postData.Append(body);
            }
            if (method == "GET" && data.Count > 0)
            {
                url += "?" + postData.ToString();
            }

            ServicePointManager.ServerCertificateValidationCallback += new RemoteCertificateValidationCallback(ValidateRemoteCertificate);

            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.Method = method;
            httpWebRequest.Accept = "*/*";
            httpWebRequest.ContentType = "application/x-www-form-urlencoded";
            if (headers != null && headers.Count > 0)
            {
                var HeaderProperties = httpWebRequest.GetType().GetProperties().Where(a => headers.Select(g => g.Item1.ToLower()).Contains(a.Name.ToLower())).ToList();
                var headersList = headers.Select(a => a.Item1).ToList();

                foreach (var prop in HeaderProperties)
                {
                    var propFound = headers.FirstOrDefault(a => a.Item1.ToLower() == prop.Name.ToLower());
                    if (propFound != null)
                    {
                        prop.SetValue(httpWebRequest, propFound.Item2);
                        headersList.Remove(propFound.Item1);
                    }
                }

                foreach (var prop in headersList)
                {

                    var value = headers.FirstOrDefault(a => a.Item1.ToLower() == prop.ToLower());
                    if (value != null)
                    {
                        httpWebRequest.Headers.Add(prop, value.Item2);
                    }
                }
            }

            if (credentials != null)
            {
                httpWebRequest.Credentials = credentials;
                //httpWebRequest.Proxy.Credentials = credentials;
            }

            if (method == "POST")
            {
                using (Stream requestStream = httpWebRequest.GetRequestStream())
                using (MemoryStream ms = new MemoryStream())
                using (BinaryWriter bw = new BinaryWriter(ms))
                {
                    bw.Write(Encoding.GetEncoding(1252).GetBytes(postData.ToString()));
                    ms.WriteTo(requestStream);
                }
            }
            return GetWebResponse(httpWebRequest, out httpStatusCode);
        }

        public static string GetWebResponse(HttpWebRequest httpWebRequest, out HttpStatusCode httpStatusCode)
        {
            ServicePointManager.ServerCertificateValidationCallback += new RemoteCertificateValidationCallback(ValidateRemoteCertificate);

            using (HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse())
            {
                httpStatusCode = httpWebResponse.StatusCode;

                if (httpStatusCode == HttpStatusCode.OK)
                {
                    using (Stream responseStream = httpWebResponse.GetResponseStream())
                    using (StreamReader responseReader = new StreamReader(responseStream))
                    {
                        StringBuilder response = new StringBuilder();

                        char[] read = new Char[256];
                        int count = responseReader.Read(read, 0, 256);

                        while (count > 0)
                        {
                            response.Append(read, 0, count);
                            count = responseReader.Read(read, 0, 256);
                        }
                        responseReader.Close();
                        return response.ToString();
                    }
                }
                return null;
            }
        }
        #endregion

        protected override IActivityExecutionResult OnExecute()
        {
            try
            {
                Boolean returnvalue = false;
                var TSystem = TargetSystem;
                var TPort = Port;
                var TOut = Timeout;

                TcpClient portscan = new TcpClient();
                try
                {
                    portscan.ConnectAsync(TSystem, TPort);
                    Thread.Sleep(TOut);
                    returnvalue = portscan.Connected;
                }
                catch
                {
                    returnvalue = false;
                }

                PortAvailability = returnvalue;

                
                if (returnvalue)
                {
                    Console.WriteLine("Port " + TPort + " availability : True");
                }
                else
                {
                    Console.WriteLine("Port " + TPort + " availability : False");
                }
                
                return Done();
            }
            catch(Exception e)
            {
                throw e;
            }
           
        }
    }
}
