using Newtonsoft.Json.Linq;
using System.Runtime.Intrinsics.X86;
using JWT;

namespace ElsaQuickstarts.Server.DashboardAndServer
{
    //public static class FlowsterRestAPI
    //{
    //    [Serializable]
    //    public class FlowsterRestAPISession
    //    {
    //        private string _baseUrl;
    //        private string _user;
    //        private string _domain;
    //        private string _fullUsername;
    //        private string _password;
    //        private string _tenantName;
    //        private string _token;
    //        private DateTime _expiration;
    //        private int _tenantId;

    //        public bool IsConnected { get { return !string.IsNullOrWhiteSpace(_token); } }

    //        /// <summary>
    //        /// set the encrypted properties of the PAM connection
    //        /// </summary>
    //        public FlowsterRestAPISession(String restApiAddress, String fullUsername, String password, string tenantName)
    //        {
    //            _baseUrl = restApiAddress;
    //            _fullUsername = fullUsername;
    //            _password = password;
    //            _tenantName = tenantName;
    //        }

    //        public async Task<JWT.jwtto> GetTokenAsync()
    //        {
    //            var username = FMS.Cryptography.Decrypt.TripleDES(_fullUsername, Globals.GetString(Globals.lsb));
    //            var password = FMS.Cryptography.Decrypt.TripleDES(_password, Globals.GetString(Globals.lsb));
    //            var tenantName = FMS.Cryptography.Decrypt.TripleDES(_tenantName, Globals.GetString(Globals.lsb));

    //            try
    //            {
    //                _tenantId = await Invoke<int>("api/Installer/GetTenantIdForTenantName", FlowsterRestAPI.FlowsterRestAPISession.MethodEnum.POST, tenantName);
    //            }
    //            catch { }

    //            var jsSer = new JavaScriptSerializer();
    //            var bodyCredentials = new { username, password };
    //            var route = @"api/jwt";
    //            var apiResponse = new object();
    //            FlowsterTokenResponse tokenResponse;

    //            try
    //            {
    //                apiResponse = await Invoke<JWTToken>(route, MethodEnum.POST, bodyCredentials);
    //            }
    //            catch (Exception ex)
    //            {
    //                var errorMsg = "Connection With Api Failed!";
    //                try
    //                {
    //                    var deserializedError = jsSer.Deserialize<ErrorClassModel>(ex.Message);
    //                    if (deserializedError != null)
    //                    {
    //                        errorMsg = deserializedError.Summarry;
    //                    }
    //                }
    //                finally
    //                {
    //                    tokenResponse = new FlowsterTokenResponse(new JWTToken(), errorMsg);
    //                }
    //                return tokenResponse;
    //            }

    //            try
    //            {
    //                var token = apiResponse as JWTToken;

    //                if (token != null)
    //                {
    //                    _token = FMS.Cryptography.Encrypt.TripleDES(token.AccessToken, Globals.GetString(Globals.lsb));
    //                    _domain = FMS.Cryptography.Encrypt.TripleDES(token.Domain, Globals.GetString(Globals.lsb));
    //                    _user = FMS.Cryptography.Encrypt.TripleDES(token.UserName, Globals.GetString(Globals.lsb));

    //                    var jwthandler = new JwtSecurityTokenHandler();
    //                    var jwttoken = jwthandler.ReadToken(token.AccessToken);
    //                    _expiration = jwttoken.ValidTo;
    //                    return new FlowsterTokenResponse(token, string.Empty);
    //                }

    //                //User has no roles
    //                return new FlowsterTokenResponse(new JWTToken(), "User has no roles!");
    //            }
    //            catch (Exception ex)
    //            {
    //                if (apiResponse is string)
    //                {
    //                    return new FlowsterTokenResponse(new JWTToken(), jsSer.Deserialize<string>(apiResponse as string));
    //                }

    //                return new FlowsterTokenResponse(new JWTToken(), "Invalid credentials");
    //            }
    //        }
    //    }
    //}
}
