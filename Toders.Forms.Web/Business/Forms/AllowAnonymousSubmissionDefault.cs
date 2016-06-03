using EPiServer;
using EPiServer.Core;
using EPiServer.Forms.Implementation.Elements;
using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using EPiServer.ServiceLocation;

namespace Toders.Forms.Business.Forms
{
    [InitializableModule]
    [ModuleDependency(typeof(EPiServer.Web.InitializationModule))]
    public class AllowAnonymousSubmissionDefault : IInitializableModule
    {
        public void Initialize(InitializationEngine context)
        {
            //Add initialization logic, this method is called once after CMS has been initialized

            ServiceLocator.Current.GetInstance<IContentEvents>().CreatingContent += SetAllowAnonymousSubmission;
        }

        private void SetAllowAnonymousSubmission(object sender, ContentEventArgs e)
        {
            var formContainerBlock = e.Content as FormContainerBlock;
            if (formContainerBlock == null)
            {
                return;
            }

            formContainerBlock.AllowAnonymousSubmission = true;
        }

        public void Uninitialize(InitializationEngine context)
        {
        }
    }
}