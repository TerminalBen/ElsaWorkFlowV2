using System.IO;
using System.Threading.Tasks;
//using DocumentManagement.Core.Models;
//using DocumentManagement.Core.Services;
using Elsa.ActivityResults;
using Elsa.Attributes;
using Elsa.Expressions;
using Elsa.Providers.WorkflowStorage;
using Elsa.Services;
using Elsa.Services.Models;
using Elsa.Attributes;
using System.ComponentModel;
using DotLiquid;
using System.Reflection.Emit;
using System.Net.Http;
using System;
using ElsaQuickstarts.Server.DashboardAndServer;

namespace ElsaQuickstarts.Server.DashboardAndServer.Activities
{

    //public record DocumentFile(Document Document, Stream FileStream);
    
    [Action(Category = "Connection", Description = "Creates a folder in the desired Desitnation")]


    public class TestFlowsterInstance : Activity
    {
        public TestFlowsterInstance()
           : base()
        {

        }
        #region Input
        [ActivityInput(Label = @"RestAPiUrl", Hint = @"Enter your RestApi Url",
            IsBrowsable = true, Category = "")]

        public string RestAPiUrl
        {
            get; set;
        } = default!;

        #endregion

        #region Output
        [ActivityOutput(Hint = @"Outputs If the connection was sucessfull or not")]
        public bool Connection
        {
            get; set;
        } 

        #endregion
        protected override async ValueTask<IActivityExecutionResult> OnExecuteAsync(ActivityExecutionContext context)
        {
            try
            {
                var httpClient = Communication.GetClient();

                var path = RestAPiUrl + "/api/FlowsterInstance/Test";

                var responseStatusCode = await httpClient.GetAsync(path);

                if (responseStatusCode.IsSuccessStatusCode)
                {
                    Connection = true;
                }

                return Done(Connection);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
