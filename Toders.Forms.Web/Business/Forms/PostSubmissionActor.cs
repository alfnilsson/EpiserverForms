using EPiServer.Forms.Core.PostSubmissionActor;

namespace Toders.Forms.Business.Forms
{
    public class PostSubmissionActor : PostSubmissionActorBase
    {
        public override object Run(object input)
        {
            var submissionData = this.SubmissionData;
            return null;
        }
    }
}