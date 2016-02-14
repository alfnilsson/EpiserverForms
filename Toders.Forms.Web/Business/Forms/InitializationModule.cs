using System.Diagnostics;
using EPiServer.Forms.Core.Events;
using EPiServer.Framework;
using EPiServer.Framework.Initialization;

namespace Toders.Forms.Business.Forms
{
    [InitializableModule]
    [ModuleDependency(typeof(EPiServer.Web.InitializationModule))]
    public class InitializationModule : IInitializableModule
    {
        public void Initialize(InitializationEngine context)
        {
            FormsEvents.Instance.FormsSubmitting += FormsSubmitting;
            FormsEvents.Instance.FormsStepSubmitted += FormsStepSubmitted;
            FormsEvents.Instance.FormsSubmissionFinalized += FormsSubmissionFinalized;
            FormsEvents.Instance.FormsStructureChange += FormsStructureChange;
        }

        public void Uninitialize(InitializationEngine context)
        {
            //Add uninitialization logic
        }

        private void FormsSubmitting(object sender, FormsEventArgs formsEventArgs)
        {
            AddSpaceLog();
            var formsSubmittingEventArgs = formsEventArgs as FormsSubmittingEventArgs;
            Log("FormsSubmitting");
        }

        private void FormsStepSubmitted(object sender, FormsEventArgs formsEventArgs)
        {
            var formsSubmittedEventArgs = formsEventArgs as FormsSubmittedEventArgs;
            Log("FormsStepSubmitted");
        }

        private void FormsSubmissionFinalized(object sender, FormsEventArgs formsEventArgs)
        {
            var formsSubmittedEventArgs = formsEventArgs as FormsSubmittedEventArgs;
            Log("FormsSubmissionFinalized");
        }

        private void FormsStructureChange(object sender, FormsEventArgs formsEventArgs)
        {
            Log("FormsStructureChange");
        }

        private static void AddSpaceLog()
        {
            Debug.WriteLine(string.Empty);
        }

        private static void Log(string subject)
        {
            Debug.WriteLine(subject);
        }
    }
}