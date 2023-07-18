using DotLiquid;
using Elsa.Activities.Primitives;
using Elsa.ActivityResults;
using Elsa.Attributes;
using Elsa.Services;
using System.Runtime.Intrinsics.X86;

namespace ElsaQuickstarts.Server.DashboardAndServer.Activities
{
    [Action(Category = "Connection", Description = "Creates a folder in the desired Desitnation")]
    public class ConnectToFlowsterRestAPI : Activity
    {

        public ConnectToFlowsterRestAPI()
            : base()
        {

        }
        #region In/Out Parameters
        [ActivityInput(
                    Label = @"RestAPI Address",
                    Hint = @"Enter the Rest API Address. It can be copied from the Flowster Administrator by selecting the clientApp for the RestAPI",
                    IsBrowsable = true,
                    Category = @"Parameters")]
        public string RestAPIAdress
        {
            get; set;
        } = default!;

        //how to set required Field?
        [ActivityInput(
                    Label = @"TenantName",
                    Name = @"Tennant Name",
                    Hint = @"Enter the Tenant Name for the RestAPI user.",
                    IsBrowsable = true,
                    Category = @"Parameters")]
        public string TenantName
        {
            get; set;
        } = default!;

        [ActivityInput(
                    Label = @"UserName",
                    Name = @"User Name",
                    Hint = @"Enter the username with domain (example: ""domain\\user"")",
                    IsBrowsable = true,
                    Category = @"Parameters")]
        public string UserName
        {
            get; set;
        } = default!;

        [ActivityInput(
                    Label = @"Password",
                    Name = @"Password",
                    Hint = @"Enter the user Password",
                    IsBrowsable = true,
                    Category = @"Parameters")]
        public string Password
        {
            get; set;
        } = default!;

        [ActivityInput(
                    Label = @"PasswordBinding",
                    Name = @"Password Binding",
                    Hint = @"Bind to a variable containing the encrypted password for connection. (This parameter is used only when the user wants to send an encripted password from outside the task, for example Flowster Studio Portal)",
                    IsBrowsable = true,
                    Category = @"Parameters")]
        public string PasswordBinding
        {
            get; set;
        } = default!;

        [ActivityOutput(

                    Name = @"Connection Object",
                    Hint = @"Connection object from Flowster",
                    IsBrowsable = true)]
        public object Connection
        {
            get; set;
        } = default!;

        #endregion

        protected override IActivityExecutionResult OnExecute()
        {

            try
            {
                if (string.IsNullOrWhiteSpace(UserName))
                {
                    throw new Exception("Please specify a valid Username");
                }
                if (string.IsNullOrWhiteSpace(RestAPIAdress))
                {
                    throw new Exception("Please specify a valid Rest API Address");
                }
                if (string.IsNullOrWhiteSpace(TenantName))
                {
                    throw new Exception("Please specify a valid Tenant Name");
                }

                var userWithDomain = UserName;
                string pwd = "";

                // Criptography stuff. To check Later

                //if (PasswordBinding == null)//password binding not set
                //{
                //    pwd = FMS.Cryptography.Decrypt.TripleDES(Password, Globals.GetString(Globals.lsb));//email_password
                //}
                //else
                //{
                //    pwd = FMS.Cryptography.Decrypt.TripleDES(PasswordBinding.Get(context), Globals.GetString(Globals.lsb));
                //}

                //var flowsterSession = new FlowsterRestAPI.FlowsterRestAPISession(RestAPIAdress, userWithDomain, pwd, TenantName);

                //var accessToken = flowsterSession.GetTokenAsync().Result;

                //if (!string.IsNullOrWhiteSpace(accessToken.Exception))
                //{
                //    throw new Exception($"Connection failed: {accessToken.Exception}");
                //}

                //Connection = flowsterSession;

                ////if (TrackOutputs == YesNoEditor.YesNo.Yes.ToString())
                ////{
                ////    TrackData("Connection to Flowster RestAPI has been completed successfully", "Done", context);
                ////}
                return Done();
            }
            catch (Exception ex)
            {

                throw new Exception("Error in Connection", ex);
            }

        }
    }

}
