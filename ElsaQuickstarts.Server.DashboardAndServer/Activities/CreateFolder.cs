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

namespace ElsaQuickstarts.Server.DashboardAndServer.Activities
{

    //public record DocumentFile(Document Document, Stream FileStream);

    [Action(Category = "File Management", Description = "Creates a folder in the desired Desitnation")]


    public class CreateFolder : Activity
    {
        public CreateFolder()
           : base()
        {

        }
        #region Input
        [ActivityInput(Label = @"Path", Hint = @"Enter the path on which the folder should be created. The path should not include the folder name because that is specified in the FolderName parameter",
            IsBrowsable = true, Category = "")]

        public string Path
        {
            get; set;
        } = default!;

        [ActivityInput(Label = @"FolderName", Hint = @"Enter the name of the folder to be created",
            IsBrowsable = true, Category = "")]

        public string FolderName
        {
            get; set;
        } = default!;
        #endregion

        #region Output
        [ActivityOutput(Hint = @"Outputs the path of the created folder")]
        public string PathOfCreatedFolder
        {
            get; set;
        } = default!;

        #endregion
        protected override IActivityExecutionResult OnExecute()
        {
            try
            {
                string pathAux = System.IO.Path.Combine(Path, FolderName);
                Directory.CreateDirectory(pathAux);
                PathOfCreatedFolder = pathAux;


                //Stuff for the tracking Data
                //if (TrackOutputs == YesNoEditor.YesNo.Yes.ToString())
                //{
                //    TrackData("PathOfCreatedFolder", pathAux, context);
                //}

                //stuff for logging
                //ExecutionResultString.Set(context, "Folder Created");
                //if (TrackOutputs == YesNoEditor.YesNo.Yes.ToString())
                //{
                //    TrackData("Execution Result", "Folder Created", context);
                //}



                return Done();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
