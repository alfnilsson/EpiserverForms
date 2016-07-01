using System;
using System.Collections.Generic;
using System.Linq;
using EPiServer;
using EPiServer.Core;
using EPiServer.Forms.Core;
using EPiServer.Forms.Core.Data;
using EPiServer.Forms.Core.Models;
using EPiServer.Forms.Core.Models.Internal;
using EPiServer.ServiceLocation;

namespace Toders.Web.Business.Forms
{
    public class FormReader
    {
        private Injected<IFormRepository> _formRepository;
        private Injected<IFormDataRepository> formDataRepository;
        private Injected<IContentLoader> contentLoader;

        public FormInformation ReadForm(ContentReference contentLink)
        {
            IContent content = contentLoader.Service.Get<IContent>(contentLink);
            ILocalizable localizable = content as ILocalizable;
            if (content == null || localizable == null)
            {
                return FormInformation.Empty;
            }

            FormIdentity formIdentity = new FormIdentity(content.ContentGuid, localizable.Language.Name);
            IEnumerable<Submission> submissions = formDataRepository.Service.GetSubmissionData(formIdentity, DateTime.MinValue, DateTime.MaxValue);
            IEnumerable<FriendlyNameInfo> friendlyNameInfos = this._formRepository.Service.GetDataFriendlyNameInfos(formIdentity);

            var info = new FormInformation
            {
                Submissions = submissions,
                FriendlyNameInfos = friendlyNameInfos
            };

            return info;
        }
    }

    public class FormInformation
    {
        public static FormInformation Empty
        {
            get
            {
                return new FormInformation
                {
                    Submissions = Enumerable.Empty<Submission>()
                };
            }
        }

        public IEnumerable<Submission> Submissions { get; set; }

        // formInformation.FriendlyNameInfos.First(x => x.ElementId == currentElementId)
        // Where currentElementId could be __field__123
        public IEnumerable<FriendlyNameInfo> FriendlyNameInfos { get; set; }
    }
}