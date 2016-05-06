using System.Linq;
using System.Web;
using EPiServer;
using EPiServer.Core;
using EPiServer.DataAccess;
using EPiServer.Find.UI;
using EPiServer.Forms.Services;
using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using EPiServer.Security;
using EPiServer.ServiceLocation;

namespace Toders.Forms.Business.Forms
{
    [InitializableModule]
    [ModuleDependency(typeof(EPiServer.Web.InitializationModule))]
    public class ChangeUploadedFilesFolder : IConfigurableModule
    {
        public void ConfigureContainer(ServiceConfigurationContext context)
        {
            // Saving the current implementation
            context.Container.EjectAllInstancesOf<DataSubmissionService>();

            // And giving it to my custom implementation as I want to reuse most of the original functionality.
            // Here I'm registering it as Transient in the same way as the original was registered.
            context.Container.Configure(x => x.For<DataSubmissionService>().Use<CustomFolderDataSubmissionService>());

        }

        public void Initialize(InitializationEngine context)
        {
        }

        public void Uninitialize(InitializationEngine context)
        {
        }

        public void Preload(string[] parameters)
        {
        }
    }

    public class CustomFolderDataSubmissionService : DataSubmissionService
    {
        protected override FileSaveItem StorePostedFile(long postId, HttpPostedFileBase postedFile, ContentReference folderLink)
        {
            folderLink = GetOrCreateFolder("Form Uploads");
            return base.StorePostedFile(postId, postedFile, folderLink);
        }

        private ContentReference GetOrCreateFolder(string folderName)
        {
            var contentRepository = ServiceLocator.Current.GetInstance<IContentRepository>();

            ContentReference parentFolder = ContentReference.GlobalBlockFolder;
            ContentFolder uploadFolder = contentRepository.GetChildren<ContentFolder>(parentFolder)
                .FirstOrDefault(contentFolder => contentFolder.Name == folderName);
            if (uploadFolder != null)
            {
                return uploadFolder.ContentLink;
            }

            var newFolder = contentRepository.GetDefault<ContentFolder>(parentFolder);
            newFolder.Name = folderName;
            return contentRepository.Save(newFolder, SaveAction.Publish, AccessLevel.NoAccess);
        }
    }
}
