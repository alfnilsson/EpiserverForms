using System;
using System.Collections.Generic;
using System.Linq;
using EPiServer;
using EPiServer.Core;
using EPiServer.Forms.Core.Data;
using EPiServer.Forms.Core.Models;
using EPiServer.ServiceLocation;

namespace Toders.Forms.Business.Forms
{
    public class FormReader
    {
        private Injected<IFormDataRepository> formDataRepository;
        private Injected<IContentLoader> contentLoader;

        public IEnumerable<Submission> ReadForm(ContentReference contentLink)
        {
            IContent content = contentLoader.Service.Get<IContent>(contentLink);
            ILocalizable localizable = content as ILocalizable;
            if (content == null || localizable == null)
            {
                return Enumerable.Empty<Submission>();
            }

            FormIdentity formIdentity = new FormIdentity(content.ContentGuid, localizable.Language.Name);
            IEnumerable<Submission> submissions = formDataRepository.Service.GetSubmissionData(formIdentity, DateTime.MinValue, DateTime.MaxValue);
            return submissions;
        }
    }
}
