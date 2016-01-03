using System.Web.Mvc;
using EPiServer.Core;
using EPiServer.Framework.DataAnnotations;
using EPiServer.Framework.Web;
using EPiServer.Web;
using EPiServer.Web.Mvc;
using EPiServer.Forms.Implementation.Elements;

namespace Toders.Forms.Controllers
{
    [TemplateDescriptor(
        TemplateTypeCategory = TemplateTypeCategories.MvcController,
        Tags = new[] { RenderingTags.Preview, RenderingTags.Edit },
        Default = true,
        AvailableWithoutTag = false)]
    [VisitorGroupImpersonation]
    public class PreviewFormController : ActionControllerBase, IRenderTemplate<FormContainerBlock>
    {
        public ActionResult Index(IContent currentContent)
        {
            var contentArea = new ContentArea();
            contentArea.Items.Add(new ContentAreaItem
            {
                ContentLink = currentContent.ContentLink
            });

            return View(contentArea);
        }
    }
}
